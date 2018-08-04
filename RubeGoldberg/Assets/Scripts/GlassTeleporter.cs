using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassTeleporter : MonoBehaviour
{
    public GameObject teleportTo; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Glasses got entered by:" + other.gameObject.name);
        if (other.gameObject.CompareTag("Throwable") && !other.gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            other.gameObject.transform.position = teleportTo.transform.position;
        }
    }
     
}