using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatController : MonoBehaviour {

    public GameObject player;               // Reference to the player's position.
    /*
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    */
    NavMeshAgent nav;               // Reference to the nav mesh agent.

    Vector3 diff;

    void Start()
    {
        // Set up the references.
        diff = gameObject.transform.position - player.transform.position;
        Debug.Log(diff);
        /*
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        */

    }
 
}
