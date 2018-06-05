using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject poof;
    private float x, y, z;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
	}

    public void Hit()
    {
        Object.Instantiate(poof, new Vector3(x, y, z), Quaternion.identity);
        gameObject.SetActive(false);
    }
}
