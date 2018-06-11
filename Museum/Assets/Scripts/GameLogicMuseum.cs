using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicMuseum : MonoBehaviour {

    public static GameObject player;
    public static Vector3 playerPosition;

    public GameObject portal;
    public GameObject rope;

    public Material alisonReal;
    public Material ericReal;
    public Material hologram;

    public GameObject humanStage;
    public GameObject alisonLeft;
    public GameObject alisonRight;
    public GameObject eric;
    public GameObject gun;
    public GameObject virtualWorld;
    public GameObject realWorld;
    public GameObject battleAudioHolder;

    private bool virt;

	// Use this for initialization
	void Start () {
        virt = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (playerPosition != new Vector3(0, 0, 0))
        {
            player.transform.position = playerPosition;
            Debug.Log(playerPosition);
        }
        else
        {
            Debug.Log("enterhere");
            player.transform.position = new Vector3(0, 2.1f, 28);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchReality ()
    {
        if (virt) // need to switch to real
        {
            alisonLeft.GetComponent<Renderer>().material = alisonReal;
            alisonRight.GetComponent<Renderer>().material = alisonReal;
            eric.GetComponent<Renderer>().material = ericReal;
            virtualWorld.SetActive(false);
            gun.SetActive(false);
            realWorld.SetActive(true);
            battleAudioHolder.GetComponent<AudioSource>().Stop();
        } else // need to switch to virt
        {
            alisonLeft.GetComponent<Renderer>().material = hologram;
            alisonRight.GetComponent<Renderer>().material = hologram;
            eric.GetComponent<Renderer>().material = hologram;
            realWorld.SetActive(false);
            virtualWorld.SetActive(true);
            gun.SetActive(true);
            battleAudioHolder.GetComponent<AudioSource>().Play();
        }
        virt = !virt;
    } 

    public void SwitchToCoasterSceneWrapper()
    {
        MoveTowardsAndSwitchScene(portal, true, portal.transform.position.y);
    }

    public void SwitchToDoDSceneWrapper()
    {
        MoveTowardsAndSwitchScene(rope, false, 2.9f);
    }

    void MoveTowardsAndSwitchScene(GameObject target, bool coaster, float height)
    { 
        iTween.MoveTo(player,
             iTween.Hash(
                 "position", new Vector3(target.transform.position.x, height, target.transform.position.z),
                 "time", 3f,
                 "easetype", iTween.EaseType.linear,
                 "oncomplete", "SwitchToScene",
                 "oncompleteparams", coaster,
                 "oncompletetarget", gameObject
             )
         );
    }

    void SwitchToScene(bool coaster)
    {
        GameLogic.coaster = coaster;
        SceneManager.LoadSceneAsync("rollercoaster", LoadSceneMode.Single); 
    } 
}

