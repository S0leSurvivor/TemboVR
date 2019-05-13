using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Script to track realistic devices
public class VRTrackingObject : MonoBehaviour {

    // Tracking site identifier
    public XRNode trackingNode;

    void Start()
	{
        // If the XR (VR) option is not on when the game starts, turn it off
        if (XRSettings.enabled == false)
		{
			Debug.LogWarning("No XR device connected. Or setting is off in project settings. ");
			enabled = false;
		}

    }

	void Update()
	{

        // InputTracking has a function that automatically tracks device identifiers
        // Yes) InputTracking.GetLocalPosition (XRNode.LeftEye) tracks the left eye
        // Set the position and rotation of the part (device) that tracks my position and rotation
        transform.localPosition = InputTracking.GetLocalPosition(trackingNode);
		transform.localRotation = InputTracking.GetLocalRotation(trackingNode);

       
    }
}
