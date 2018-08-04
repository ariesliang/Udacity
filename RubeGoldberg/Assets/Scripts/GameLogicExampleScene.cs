using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class GameLogicExampleScene: MonoBehaviour {
     
    public GameObject witchLaugh;
    public GameObject intro;

    public GameObject panel; 

    void Start()
    { 
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    { 
        witchLaugh.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(witchLaugh.GetComponent<AudioSource>().clip.length); 
        intro.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(intro.GetComponent<AudioSource>().clip.length);
        panel.SetActive(true);
    } 
} 
