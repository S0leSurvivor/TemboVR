using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/* DEV NOTES:
	
    Unity has an OpenVR API built in.
    This allows the VR input to be received as a joystick input in the InputManager.

	1.  Refer to the VR input table.
	    Go to the link to see the VR input table (https://docs.unity3d.com/Manual/OpenVRControllers.html). 
        In the InputManager, you can check which joystick input number corresponds to the button of the VR controller.

	2.  In InputManager, make the input setting for VR.
	    Go to Project Settings > InputManager
	    (You can check pre-created InputManager values.)
        The name is arbitrary. In this example, the left input trigger (trigger) button on the HTC Vive controller and the left input trigger (trigger) button on the Oculus Rift touch controller have the input setting name "LeftIndexTrigger".
        The Vive left trigger and the Ocluls Rift left trigger on the input table are labeled Joystick 9th Axis.
	    Thus, the Joystick Axis corresponding to LeftIndexTrigger is specified as 9 th Axis.
	    The above contents can be confirmed from the preset value.
	
    3.  You can use Input.GetAxis as you would in a normal game.
	    If the trigger is pressed more than 0.1f (10%), the corresponding button is pressed.
	    GetAxis smoothly breaks the input value. But GetAxisRaw is used here without interpolation to get the input immediately.
*/

// Class that provides input detection of VR controller as GetButton function
public static class VRInput
{
    public enum Button { LeftIndexTrigger, RightIndexTrigger, LeftGripTrigger, RightGripTrigger, LeftThumbstick, RightThumbstick };

    

    //Left Hand
    // Input setting name corresponding to index finger trigger
    public const string leftIndexTriggerName = "LeftIndexTrigger";
    // The input setting name corresponding to the mouse trigger
    public const string leftGripTriggerName = "LeftGripTrigger";
    //The input setting name corresponding to the Left Thumbstick
    public const string leftThumbstickName = "LeftThumbstick";


    // Right Hand
    // Input setting name corresponding to index finger trigger
    public const string rightIndexTriggerName = "RightIndexTrigger";
    // The input setting name corresponding to the mouse trigger
    public const string rightGripTriggerName = "RightGripTrigger";
    //The input setting name corresponding to the Right Thumbstick
    public const string rightThumbstickName = "RightThumbstick";


    // variables for current state of button state
    // Input in the previous frame 'degree'

    private static float lastLeftIndexTriggerInput;
    private static float lastRightIndexTriggerInput;

    private static float lastLeftGripTriggerInput;
    private static float lastRightGripTriggerInput;

    private static float lastLeftThumbstickInput;
    private static float lastRightThumbstickInput;



    public static bool GetButtonUp(Button button)
    {
        switch (button)
        {
            case Button.LeftIndexTrigger:
                if (lastLeftIndexTriggerInput >= 1.0f && Input.GetAxisRaw(leftIndexTriggerName) <= 0.9f)
                {
                    return true;
                }
                break;

            case Button.RightIndexTrigger:
                if (lastRightIndexTriggerInput >= 1.0f && Input.GetAxisRaw(rightIndexTriggerName) <= 0.9f)
                {
                    return true;
                }
                break;

            case Button.LeftGripTrigger:
                if (lastLeftGripTriggerInput >= 1.0f && Input.GetAxisRaw(leftGripTriggerName) <= 0.9f)
                {
                    return true;
                }

                break;

            case Button.RightGripTrigger:
                if (lastRightGripTriggerInput >= 1.0f && Input.GetAxisRaw(rightGripTriggerName) <= 0.9f)
                {
                    return true;
                }

                break;

            case Button.LeftThumbstick:
                if (lastLeftThumbstickInput >= 1.0f && Input.GetAxisRaw(leftThumbstickName) <= 0.9f)
                {
                    return true;
                }

                break;

            case Button.RightThumbstick:
                if (lastRightThumbstickInput >= 1.0f && Input.GetAxisRaw(rightThumbstickName) <= 0.9f)
                {
                    return true;
                }

                break;
        }

        return false;
    }

    public static bool GetButtonDown(Button button)
    {
        switch (button)
        {
            case Button.LeftIndexTrigger:
                if (lastLeftIndexTriggerInput <= 0f && Input.GetAxisRaw(leftIndexTriggerName) >= 0.2f)
                {
                    return true;
                }

                break;

            case Button.RightIndexTrigger:
                if (lastRightIndexTriggerInput <= 0f && Input.GetAxisRaw(rightIndexTriggerName) >= 0.2f)
                {
                    return true;
                }

                break;

            case Button.LeftGripTrigger:
                if (lastLeftGripTriggerInput <= 0f && Input.GetAxisRaw(leftGripTriggerName) >= 0.2f)
                {
                    return true;
                }

                break;

            case Button.RightGripTrigger:
                if (lastRightGripTriggerInput <= 0f && Input.GetAxisRaw(rightGripTriggerName) >= 0.2f)
                {
                    return true;
                }

                break;

            case Button.LeftThumbstick:
                if (lastLeftThumbstickInput <= 0f && Input.GetAxisRaw(leftThumbstickName) >= 0.2f)
                {
                    return true;
                }

                break;

            case Button.RightThumbstick:
                if (lastRightThumbstickInput <= 0f && Input.GetAxisRaw(rightThumbstickName) >= 0.2f)
                {
                    return true;
                }

                break;
        }
        return false;
    }

    public static bool GetButton(Button button)
    {
        switch (button)
        {
            case Button.LeftIndexTrigger:
                if (Input.GetAxisRaw(leftIndexTriggerName) > 0.1f)
                {
                    return true;
                }

                break;
            case Button.RightIndexTrigger:
                if (Input.GetAxisRaw(rightIndexTriggerName) > 0.1f)
                {
                    return true;
                }

                break;
            case Button.LeftGripTrigger:
                if (Input.GetAxisRaw(leftGripTriggerName) > 0.1f)
                {
                    return true;
                }

                break;
            case Button.RightGripTrigger:
                if (Input.GetAxisRaw(rightGripTriggerName) > 0.1f)
                {
                    return true;
                }
                break;
            case Button.LeftThumbstick:
                if (Input.GetAxisRaw(leftThumbstickName) > 0.1f)
                {
                    return true;
                }

                break;
            case Button.RightThumbstick:
                if (Input.GetAxisRaw(rightThumbstickName) > 0.1f)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    // Need to call in other Unity Component Update respectly.
    public static void Update()
    {
        lastLeftIndexTriggerInput = Input.GetAxisRaw(leftIndexTriggerName);

        lastRightIndexTriggerInput = Input.GetAxisRaw(rightIndexTriggerName);

        lastLeftGripTriggerInput = Input.GetAxisRaw(leftGripTriggerName);

        lastRightGripTriggerInput = Input.GetAxisRaw(rightGripTriggerName);

        lastLeftThumbstickInput = Input.GetAxisRaw(leftThumbstickName);

        lastRightThumbstickInput = Input.GetAxisRaw(rightThumbstickName);
    }
}
