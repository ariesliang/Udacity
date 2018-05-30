using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
	Animator animator;
	AudioSource audioSource;

	public AudioClip doorLocked;
	public AudioClip doorOpen;
	public GameObject masterKey;

	void Start () { 
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

    public void OnDoorClicked() { 
		if (!masterKey.activeSelf) { // Unlock the door
			animator.SetBool("Lock", false); 
			audioSource.clip = doorOpen;
			audioSource.Play();  
		} else { // Play a sound to indicate the door is locked
			audioSource.clip = doorLocked;
			audioSource.Play();  
		} 
    } 
}
