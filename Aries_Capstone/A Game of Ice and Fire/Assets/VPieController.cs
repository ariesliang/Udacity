using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPieController : MonoBehaviour {

    private bool called;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!called & transform.position.z < -14)
        {
            transform.parent = null;
            // gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            called = true;
        }
	}
}
