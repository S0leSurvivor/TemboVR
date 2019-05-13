using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	// Ammunition UI
	public Text ammoText;

	// remaining bullets in magazine
    	public int magAmmo;

	// rounds per magazine
	public int magAmmoMax = 30;

    // Possible state of gun - lock, ready, magazine beam, reloading
    public enum GUN_STATE { LOCK, READY, EMPTY, RELOADING };

	// current gun status
	private GUN_STATE state = GUN_STATE.LOCK;

	// fire/burst spacing
	public float timeBetFire = 0.1f;

    // The last time the bullet was fired
    private float lastTimeFire;

	// gun animation
	public Animator gunAnimator;

	// gun sound fx
	private AudioSource fireAudio;

	// muzzle blast
	public ParticleSystem muzzleFlash;

	// round ejection
	public ParticleSystem caseEjectEffect;

	// bullet tracer
	public LineRenderer shotLineRenderer;

	// bullet firing position
	public Transform firePos;

	// tracer/bullet visible time
	private WaitForSeconds effectDuration = new WaitForSeconds(0.07f);

	// bullet range
	public float fireDistance = 100f;

	// Reload time (auto)
	public float reloadTime = 1.7f;

	// damage value
	public float damage = 25;

	// Shooting effects and animation
	public GameObject impactPrefab;

	private void Start()
	{
		// idle gun state
		state = GUN_STATE.READY;

		// set maximum ammo capicty to magazine (30 rounds)
		magAmmo = magAmmoMax;

		// check time of fire
		lastTimeFire = 0;

        // import audio sources for gunshot playback
        
                fireAudio = GetComponent<AudioSource>();

		// bullet trajectory set with two vertices
		shotLineRenderer.positionCount = 2;
		// turn off tracer
		shotLineRenderer.enabled = false;

		// update ammo UI
		UpdateGunUI();
	}

	public void Fire()
	{
        //  current time> = past time of last gun shooting + continuous shutter interval
        //  State of the Gun == Ready
        if (Time.time >= lastTimeFire + timeBetFire &&
			state == GUN_STATE.READY)
		{
			// update time of last shot fired to now
			lastTimeFire = Time.time;

			/*check shot state/shot's remaining*/
			Shot();
			// UI 갱신
			UpdateGunUI();
		}
	}
	// Shooting function (state)
	private void Shot()
	{
		// Raycasting collision
		RaycastHit hit;
        // hitPosition / arrival point of bullet projectory
        // The bullet must be assigned to the right place, but once it is set to the far side of the muzzle
        // (muzzle position + muzzle front x distance)
        Vector3 hitPosition = firePos.position + firePos.forward * fireDistance;

        // If - beaming(launch position, direction, vessel to contain collision information, range)
        // check for conflict
        if (Physics.Raycast(firePos.position, firePos.forward, out hit, fireDistance))
		{
            // The arrival point of the bullet trajectory = the point of collision of the actual ray

            hitPosition = hit.point;

            // Visible damage / bullet-hole
            // damage (original shot, position, rotation)
            // Quaternion.LookRotation(Direction) => rotate with direction of viewing
            // hit.normal = The collision angle (exactly the direction in which the collided surface is facing)
            Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));

			// instance of IDamageable
			// and check OnDamage function
			IDamageable target = hit.collider.GetComponent<IDamageable>();
			if (target != null)
			{
				target.OnDamage(damage);
			}
		}

		// playback ray collision position
		StartCoroutine(ShotEffect(hitPosition));

		//reduce ammo used by 1 per shot
		magAmmo--;

		// magazine is out ammo state
		if (magAmmo <= 0)
		{
			magAmmo = 0;
			state = GUN_STATE.EMPTY;
		}
	}

    // Play gun annimations
    // Use the "wait time" function of the coroutine to wait for a moment while turning on the bullet trail.

    private IEnumerator ShotEffect(Vector3 hitPosition)
	{
		//play firing animations and audio
		muzzleFlash.Play();
		caseEjectEffect.Play();
		fireAudio.Play();

        // Trigger Fire on gun(from animator)

        gunAnimator.SetTrigger("Fire");

		// turn on bullet linear ray effect
		shotLineRenderer.enabled = true;
        // set vertex start position to 0
        shotLineRenderer.SetPosition(0, firePos.position);
        // 1st point -point of collision(ray cast)

        shotLineRenderer.SetPosition(1, hitPosition);

		// wait time / lapse
		yield return effectDuration;

		// turn off bullet's ray effect (on/off for flash effect)
		shotLineRenderer.enabled = false;
	}

	// updated UI to a single function
	private void UpdateGunUI()
	{
		ammoText.text = magAmmo + "/" + magAmmoMax;
	}

	// reload function
	public void Reload()
	{
		if (state == GUN_STATE.RELOADING)
		{
			return;
		}

		// start reloading process 
		StartCoroutine("Reloading");
	}

	// reloading process
	// ...written as a coroutine 
	private IEnumerator Reloading()
	{
		// Assign current state to reload
		// and prevent gun from firing within reloading state
		// turn off stop fire state after gun is loaded
		state = GUN_STATE.RELOADING;
		
		gunAnimator.SetTrigger("Reload");

		// wait time
		yield return new WaitForSeconds(reloadTime);

		// current number of magazines = the maximum number of magazines
		magAmmo = magAmmoMax;

        // change to REALODING => READY to unlock the gun
                state = GUN_STATE.READY;

		// UI update
		UpdateGunUI();
	}
}