using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour {

    // Heaset setups
    private bool oculus;
    
    // vive
    public SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    // oculus
    public bool leftHand;
    private OVRInput.Controller thisController;

    // Throw
    public float throwForce = 1.5f;
    public GameObject releasingPlatform;
    private GameObject[] collectibles;
    private bool handEmpty;

    // Audio 
    public GameObject throwAudio; 
    public GameObject cheatAudio;

    // Cheat
    public Material ballCheatedMat;

    // Use this for initialization
    void Start()
    {
        oculus = HeadSetManager.oculus;
        if (!oculus) trackedObject = GetComponent<SteamVR_TrackedObject>(); // vive
        else // oculus 
        {
            if (leftHand) thisController = OVRInput.Controller.LTouch;
            else thisController = OVRInput.Controller.RTouch;
        } 
        releasingPlatform = GameObject.FindGameObjectWithTag("ReleasingPlatform");
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        // Debug.Log("Collectibles from start:" + collectibles);
        handEmpty = true;
    }

    // will fire every physics frame and object is touching the controllers collider
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            // Debug.Log("OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, thisController): " + OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, thisController));
            if (oculus && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, thisController) < 0.1f || 
                (!oculus && device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)))
            { 
                ThrowObject(other); 
            }
            else if (oculus && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, thisController) > 0.9f|| (!oculus && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)))
            {
                 GrabObject(other); 
            }
        }
        else if (other.gameObject.CompareTag("Structure"))
        {
            if (oculus && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, thisController) < 0.1f || (!oculus && device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)))
            {
                Putdown(other);
            }
            else if (oculus && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, thisController) > 0.9f || (!oculus && device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)))
            {
                GrabObject(other);
            }
        }
    }

    private void ThrowObject(Collider col)
    {
        //  Debug.Log("ReleasingPlatformPos:" + releasingPlatform.transform.position);
        // Debug.Log("BallPos:" + col.gameObject.transform.position);
        // Debug.Log("ReleasingPlatformAndBallDiff:  " + Vector3.Distance(releasingPlatform.transform.position, col.gameObject.transform.position));
        if (!handEmpty)
        {
            if (Vector3.Distance(releasingPlatform.transform.position, col.gameObject.transform.position) > 2.5f) // cheated 
            {
                DeactivateCollectibles();
                cheatAudio.GetComponent<AudioSource>().Play();
                col.gameObject.GetComponent<Renderer>().material = ballCheatedMat;
            }
            col.transform.SetParent(null);
            Rigidbody rg = col.GetComponent<Rigidbody>();
            
            col.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            rg.velocity = OVRInput.GetLocalControllerVelocity(thisController)  * throwForce;
            Debug.Log("RG velocity: " + rg.velocity);
            rg.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(thisController);
            Debug.Log("RG angular velocity: " + rg.angularVelocity);
            // Debug.Log("Throw method called");
            rg.isKinematic = false; // turn off physics
            throwAudio.GetComponent<AudioSource>().Play();
            handEmpty = true;
        }
    }

    private void Putdown(Collider col)
    {
        if (!handEmpty)
        { 
            col.transform.SetParent(null);
            Rigidbody rg = col.GetComponent<Rigidbody>();
            //Debug.Log("Putdown method called");
            handEmpty = true;
        }
       // putdownAudio.GetComponent<AudioSource>().Play();
    }

    private void GrabObject(Collider col)
    {
        if (handEmpty)
        {
            col.transform.SetParent(gameObject.transform);
            Rigidbody rg = col.GetComponent<Rigidbody>();
            rg.isKinematic = true;
            col.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
            // Debug.Log("Grab method called");
            handEmpty = false;
        }
    }

    void DeactivateCollectibles()
    {
        // Debug.Log("Deactivating collectables...");
        foreach (GameObject obj in collectibles)
        {
            obj.SetActive(true);
        } 
    }
}
