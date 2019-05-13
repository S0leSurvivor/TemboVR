using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInputUpdator : MonoBehaviour {


    //Vector3 pos;
    //public float speedMov = 10;
    //public float speedTurn = 20;

    //private void Start()
    //{
    //    pos = transform.position;
    //}

    // Update is called once per frame
    void Update ()
    {
		VRInput.Update();

        ////handle position and rotation of player
        //Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        ////left controller for player movement
        //if (VRInput.GetButton(VRInput.Button.LeftThumbstick))
        //{
        //    if (primaryAxis.y > 0f)
        //    {
        //        pos += (primaryAxis.y * transform.forward * Time.deltaTime * speedMov);
        //    }
        //    if (primaryAxis.y < 0f)
        //    {
        //        pos += (Mathf.Abs(primaryAxis.y) * -transform.forward * Time.deltaTime * speedMov);
        //    }
        //}

        ////right controller for player movement
        //if (VRInput.GetButton(VRInput.Button.RightThumbstick))
        //{
        //    if (primaryAxis.x > 0f)
        //    {
        //        pos += (primaryAxis.x * transform.right * Time.deltaTime * speedTurn);
        //    }
        //    if (primaryAxis.x < 0f)
        //    {
        //        pos += (Mathf.Abs(primaryAxis.x) * -transform.right * Time.deltaTime * speedTurn);
        //    }
        //}

        //transform.position = pos;

        //Vector3 euler = transform.rotation.eulerAngles;
        //Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //euler.y += secondaryAxis.x;
        //transform.rotation = Quaternion.Euler(euler);
        ////may need to set local rotation...
        //transform.localRotation = Quaternion.Euler(euler);
    }
}
