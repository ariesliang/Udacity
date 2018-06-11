using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Entered");
            GameLogicMuseum.playerPosition = new Vector3(0, 2.1f, -2);
            SceneManager.LoadSceneAsync("Showroom");
        }
    }
}
