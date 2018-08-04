using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    // Heaset setups
    private bool oculus;
    // vive
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    // oculus 
    private OVRInput.Controller thisController;

    // Swipe
    public ObjectMenuManager objectMenuManager;
    private bool menuIsSwipable;
    private float menuStickX;

    // Use this for initialization
    void Start () {
        oculus = HeadSetManager.oculus; 
        if (!oculus) trackedObject = GetComponent<SteamVR_TrackedObject>(); // vive
        else thisController = OVRInput.Controller.RTouch; // oculus
        Debug.Log("thisController:" + thisController);
    }

    // Update is called once per frame
    void Update()
    {
        if (!oculus)
        {
          //  device = SteamVR_Controller.Input((int)trackedObject.index);
        } 
        else
        {
          //  Debug.Log("Primary Thumbstick touch: " + OVRInput.Get(OVRInput.Touch.PrimaryThumbstick));
          //  Debug.Log("Secondary Thumbstick touch: " + OVRInput.Get(OVRInput.Touch.SecondaryThumbstick));

            if (OVRInput.Get(OVRInput.Touch.SecondaryThumbstick))
            {
               // Debug.Log("Touched secondary thumbstick");
               // Debug.Log("OVRInput.Axis2D.PrimaryThumbstick, thisController: " + OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, thisController));
                menuStickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, thisController).x;
              //  Debug.Log("MenuStickX: " + menuStickX);
                if (menuStickX < 0.45f && menuStickX > -0.45f)
                {
                    menuIsSwipable = true;
                }
                if (menuIsSwipable)
                {
                    if (menuStickX >= 0.45f)
                    {
                        objectMenuManager.MenuRight();
                        menuIsSwipable = false;
                    }
                    else if (menuStickX <= -0.45f)
                    {
                        objectMenuManager.MenuLeft();
                        menuIsSwipable = false;
                    }
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, thisController))
                {
                    SpawnCurrentObject();
                }
            }
            else
            {
              //  Debug.Log("Detouch secondary thumbstick");
                objectMenuManager.DisableMenu();
            }  
        } 
    }

    void SpawnCurrentObject()
    {
        objectMenuManager.SpawnCurrentObject();
    }

    void SwipedLeft()
    {
        objectMenuManager.MenuLeft();
       // Debug.Log("Swiped Left");
    }

    void SwipedRight()
    {
        objectMenuManager.MenuRight();
       // Debug.Log("Swiped Right");
    }
}
