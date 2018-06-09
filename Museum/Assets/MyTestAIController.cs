using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MyTestAIController : MonoBehaviour {

    public GameObject player;               // Reference to the player's position.
    public GameObject coasterSection;
    public Text panel;


    NavMeshAgent nav;

    void Start () {
        nav = GetComponent<NavMeshAgent>();
    } 

    void Update()
    {
        nav.SetDestination(player.transform.position);
    }

    public void ShowText ()
    {
        if (Vector3.Distance(transform.position, coasterSection.transform.position) <= 2) {
            panel.text = "This is a test";
        }
    }
    
}
