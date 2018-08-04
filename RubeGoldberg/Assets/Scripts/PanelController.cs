using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

    public GameObject panel;
    private bool panelActive;

    void Start()
    {
        panelActive = false;
    }

    // Update is called once per frame
    void Update () {
        if (OVRInput.Get(OVRInput.RawButton.Y))
        {
            panel.SetActive(panelActive);
            panelActive = !panelActive;
        } 
    }
}
