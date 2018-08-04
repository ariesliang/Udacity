using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    
    private int goalScore; 

    private Vector3 initialPosition;
    private GameObject[] collectibles;
    private GameObject goal;
    private int score;
    private Transform grater_end;

    // Audio 
    public GameObject ballHitWaterAudio;
    public GameObject changeSceneAudio;
    public GameObject collectiblePopAudio;

    // Material
    public Material ballOriginalMat;

    // Others
    public SteamVR_LoadLevel levelLoader; 

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
        Debug.Log("initialPosition:" + initialPosition);
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        goal = GameObject.FindGameObjectWithTag("Goal");
        goalScore = collectibles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -10 || transform.position.x >= 20 ||
            transform.position.y >= 20f || transform.position.y <= 0 ||
            transform.position.z <= -20 || transform.position.z >= 20)
        {
            ResetBall();
        }

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Ball hit : " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            ResetBall();
        }
        else if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            collectiblePopAudio.GetComponent<AudioSource>().Play();
            score++;

        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            other.gameObject.SetActive(false);
            if (score == goalScore) LoadNextLevel();
        } 
    }

    void ActivateCollectibles()
    {
        foreach (GameObject obj in collectibles)
        {
            obj.SetActive(true);
        }
        goal.SetActive(true);
        score = 0;
    }

    void ResetBall()
    {
        Debug.Log("Ball hit the ground");
        transform.position = initialPosition;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        ActivateCollectibles();
        ballHitWaterAudio.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Renderer>().material = ballOriginalMat;
    }

    void LoadNextLevel()
    {
        levelLoader.Trigger();
    }  
}
