using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


// VR gun controller input
public class GunController : MonoBehaviour {

    /* The class that receives and processes VR input needs to check only two functions of VRInput */
    // Only two functions, GetGripButton and GetTriggerButton

    public Gun gun;



    void Update()
	{
		if(VRInput.GetButton(VRInput.Button.RightIndexTrigger))
		{
			gun.Fire();
		}

		if(VRInput.GetButtonDown(VRInput.Button.RightGripTrigger))
		{
			gun.Reload();
		}

        
    }
}