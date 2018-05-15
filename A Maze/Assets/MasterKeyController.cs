using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterKeyController : MonoBehaviour {
 
	public GameObject poofCollectables;

	private float x = 0;
	private float y = 0;
	private float z = 0;

 	void Update () 
	{
		transform.Rotate (new Vector3 (10, 90, 0) * Time.deltaTime);
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
	}

	// When mouse is clicked 
	public void Collect ()
	{
		Object.Instantiate(poofCollectables, new Vector3(x,y,z), Quaternion.identity);
		gameObject.SetActive(false);
	}	
}
