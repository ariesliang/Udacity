using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSphereController : MonoBehaviour {

    private bool startMoving;
    private Rigidbody rb;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (startMoving)
        {
            rb.AddForce(transform.right * 500 * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        startMoving = true;
    }
}
