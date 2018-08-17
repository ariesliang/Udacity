using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedLineController : MonoBehaviour {

    public GameObject saveAndLoad;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        saveAndLoad.gameObject.SendMessage("GameOver", false);
    }
}
