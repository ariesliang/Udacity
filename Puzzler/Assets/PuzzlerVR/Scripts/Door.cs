using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Door : MonoBehaviour {

    public GameObject player;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	} 

    public void OpenDoor()
    {
        animator.SetBool("player_nearby", true);
        this.GetComponent<AudioSource>().Play();  
    }

    public void CloseDoor()
    {
        animator.SetBool("player_nearby", false);
    }
}
