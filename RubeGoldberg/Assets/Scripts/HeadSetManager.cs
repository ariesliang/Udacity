using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadSetManager : MonoBehaviour {

    public GameObject viveRig;
    public GameObject oculusRig;
    private bool hmdChosen;
    public static bool oculus;
    
	// Use this for initialization
	void Start ()
    {
        Debug.Log("Active headset....");
        ActiveHeadset();
	}

    // Update is called once per frame
    void Update()
    {
        if (!hmdChosen)
        {
            ActiveHeadset();
        } 
        if (!XRDevice.isPresent)
        {
            hmdChosen = false;
        }
    }

    void ActiveHeadset ()
    { 
        string model = XRDevice.model;
        Debug.Log("model:"+model);

        /*if (model.ToLower().Contains("vive"))
        {
            oculusRig.SetActive(false);
          //  viveRig.SetActive(true);
            hmdChosen = true;
            oculus = false;
        }
        */
        if (model.ToLower().Contains("oculus"))
        {
           // viveRig.SetActive(false);
            oculusRig.SetActive(true);
            hmdChosen = true;
            oculus = true;
        }
        Debug.Log("Activing headset: oculus " + oculus);
    }
}
