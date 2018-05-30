using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class CollectableController : MonoBehaviour {

	public GameObject poof_collectables;

	private float x = 0;
	private float y = 0;
	private float z = 0;
	
	// Update is called once per frame
	void Update () 
	{
//		transform.Rotate (new Vector3 (10, 90, 0) * Time.deltaTime);
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
	}

	// When mouse is clicked 
	public void Collect ()
	{
		Object.Instantiate(poof_collectables, new Vector3(x,y,z), Quaternion.identity);
//		Destroy(gameObject);
		gameObject.SetActive(false);
	}	
}
