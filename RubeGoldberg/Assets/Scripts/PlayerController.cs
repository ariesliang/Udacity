using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Audio
    public GameObject edgeWarningAudio;
    public GameObject edgeWarningVoiceoverAudio;

    public Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update () {
		if (transform.position.x <= -10 || transform.position.x >= 20 || 
            transform.position.y >= 20f || transform.position.y <= 0 ||
            transform.position.z <= -20 || transform.position.z >= 20)
        {
            // audio for warning
            edgeWarningAudio.GetComponent<AudioSource>().Play();
            transform.position = initialPos;
            edgeWarningVoiceoverAudio.GetComponent<AudioSource>().Play();
        }

    }
}
