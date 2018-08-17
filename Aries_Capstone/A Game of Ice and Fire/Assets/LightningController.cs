using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour {

    int count;

    public GameObject child;

    private void Start()
    {
        InvokeRepeating("LightningOn", 0.0f, 20.0f);
        InvokeRepeating("LightningOff", 4.0f, 20.0f);
    }

    private void LightningOn()
    {
        Debug.Log("LIGHT on");
        child.gameObject.SetActive(true);
    }

    private void LightningOff()
    {
        Debug.Log("LIGHT off");
        child.gameObject.SetActive(false);
    }

}
