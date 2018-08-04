using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CrossPlatformUtil : MonoBehaviour {

    private bool oculus;

    public CrossPlatformUtil ()
    {
        string model = XRDevice.model;
        if (model == "vive")
        {
            oculus = false;
        }
        else if (model == "oculus")
        {
            oculus = true;
        }
    } 

    public bool pressIndexTrigger(SteamVR_Controller.Device device)
    {
        if (!oculus)
        {
            return device.GetPress(SteamVR_Controller.ButtonMask.Trigger);
        }
        else
        {
            return OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger);
        }
    }

    public bool releaseIndexTrigger(SteamVR_Controller.Device device)
    {
        if (!oculus)
        {
            return device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
        }
        else
        {
            return OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);
        }
    }
}
