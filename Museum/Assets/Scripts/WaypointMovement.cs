using UnityEngine;
using System.Collections;

public class WaypointMovement : MonoBehaviour {
	
	public GameObject player;

    public float height = 2f;
	public bool teleport = false;

	public float maxMoveDistance = 10;
	private bool moving = false;
    private bool small;

	// Use this for initialization
	void Start () {
        small = false;
        height = player.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        float scale = Mathf.Lerp(0.5f, 1f, Mathf.Abs(Mathf.Cos(Time.time)));
        transform.localScale = Vector3.one * scale;
        /*
        if (player.transform.position.x == gameObject.transform.position.x && player.transform.position.z == gameObject.transform.position.z)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }*/
    }

	public void Move(GameObject waypoint) {
		if (!teleport) {
			iTween.MoveTo (player, 
				iTween.Hash (
					"position", new Vector3 (waypoint.transform.position.x, height, waypoint.transform.position.z), 
					"time", .2F, 
					"easetype", "linear"
				)
			);
		} else {
			player.transform.position = new Vector3 (waypoint.transform.position.x, 
                height, 
                waypoint.transform.position.z);
		}
	}

}

