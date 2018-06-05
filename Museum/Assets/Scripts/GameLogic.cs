using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public GameObject rollercoaster;
    public GameObject curves;
    public GameObject pillars;
    public Material coasterVirtualWorldMaterial;
    public Material coasterRealityWorldMaterial;
    public GameObject realityWorld;
    public GameObject virtualWorld;
    public GameObject cardboardIndicator;
    public GameObject breatheAudioHolder;

    private bool virt;
    private AudioSource breathe;

    void Start() {
        virt = false;
        breathe = breatheAudioHolder.GetComponent<AudioSource>();
        breathe.time = 56.5f;
        breathe.Play();
        breathe.mute = true;
    }

    void Update()
    {
        // If the player pressed the cardboard button (or touched the screen), set the trigger parameter to active (until it has been used in a transition)
        if (Input.GetMouseButtonDown(0))
        {
            SwitchReality();
        }
    }

    void SwitchReality() {
        if (virt)
        { // switching back to reality
            virt = false;
            realityWorld.SetActive(true);
            virtualWorld.SetActive(false);
            pillars.SetActive(true);
            ChangeRollerCoasterMaterial(coasterRealityWorldMaterial, curves);
            cardboardIndicator.SetActive(false);
            breathe.mute = true;
        }
        else
        { // switching back to virtual 
            virt = true;
            virtualWorld.SetActive(true);
            realityWorld.SetActive(false);
            pillars.SetActive(false);
            ChangeRollerCoasterMaterial(coasterVirtualWorldMaterial, curves);
            cardboardIndicator.SetActive(true);
            breathe.mute = false;
        }
    } 

    void ChangeRollerCoasterMaterial(Material space, GameObject targetObject)
    {
        if (targetObject != null)
        {
            if (targetObject.GetComponent<Renderer>() != null)
            {
                int length = targetObject.GetComponent<Renderer>().materials.Length;
                Material[] spaceMultiMaterials = new Material[length];
                for (int i = 0; i < length; i++) spaceMultiMaterials[i] = space;
                targetObject.GetComponent<Renderer>().materials = spaceMultiMaterials;
            }
            foreach (Transform child in targetObject.transform)
            {
                ChangeRollerCoasterMaterial(space, child.gameObject);
            }
        }
    } 
}
