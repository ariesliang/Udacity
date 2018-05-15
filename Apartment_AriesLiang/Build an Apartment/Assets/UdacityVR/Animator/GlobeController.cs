using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeController : MonoBehaviour {

	public bool rotate = false; 

	void Start () 
	{
		rotate = false; 
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Fire1"))
			rotate = !rotate;
		if (rotate) transform.Rotate (new Vector3 (45, 45, 45) * Time.deltaTime);
	}
}
