using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerHeightTracking : MonoBehaviour {

    // this script tracks the location of the player's headset and look direction
	// when stationary
    

    // Player location tracking
    public float playerHeight = 1.7f;
	// Update is called once per frame
	void Start () {

		if(XRSettings.enabled == false)
		{
			enabled = false;
		}

		if(XRDevice.GetTrackingSpaceType() == TrackingSpaceType.RoomScale)
		{
			Debug.Log("Room Scale Tracking");
		}
		else
		{
			transform.localPosition += Vector3.up * playerHeight;
		}	


	}
}
