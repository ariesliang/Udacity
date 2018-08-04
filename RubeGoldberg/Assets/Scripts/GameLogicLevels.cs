using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class GameLogicLevels: MonoBehaviour {
     
    public GameObject changeSceneAudio;
    public GameObject intro;

    void Start()
    {
        changeSceneAudio.GetComponent<AudioSource>().Play();
        intro.GetComponent<AudioSource>().Play();
    }
} 
