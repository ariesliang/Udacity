using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGlassController : MonoBehaviour
{ 
    public GameObject teleportTo;
    /*
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Glasses hit : " + other);
        Debug.Log("Teleport To: " + teleportTo.transform.position);
        if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("Ball hit the glasses");
            other.transform.position = teleportTo.transform.position;
        }
    }*/
}