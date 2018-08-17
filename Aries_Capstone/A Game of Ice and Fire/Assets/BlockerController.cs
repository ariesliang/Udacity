using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour {

    public GameObject hit;
    public GameObject die;
    public GameObject poof;
    public bool left;

    private AudioSource hitAudio;
    private AudioSource dieAudio; 

    private OVRInput.Controller thisController;

    private void Start()
    {
        hitAudio = hit.GetComponent<AudioSource>();
        dieAudio = die.GetComponent<AudioSource>();
        if (left) thisController = OVRInput.Controller.LTouch;
        else thisController = OVRInput.Controller.RTouch;
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitAudio.Play();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, thisController))
        { 
            Instantiate(poof, collision.transform.position, Quaternion.identity);  // spawn some particles 
            dieAudio.Play(); // sound 
            Destroy(collision.gameObject);
        }
    }
}
