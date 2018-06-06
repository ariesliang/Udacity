using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public bool coaster; // true if set coaster to major scene, false if set drop of doom as major scene

    public GameObject coasterCurves;
    public GameObject coasterpillars;
    public GameObject dropOfDoom;
    public GameObject park;

    public Material coasterVirtualWorldMaterial;
    public Material coasterRealityWorldMaterial;

    public GameObject coasterVirtualWorld;
    public GameObject dropOfDoomVirtualWorld;

    public GameObject cameraHolder;
    public GameObject cardboardIndicator;

    public GameObject coasterCameraPositionHolder;
    public GameObject dropOfDoomCameraPositionHolder;

    public GameObject breatheAudioHolder;

    private bool virtCoaster;
    private AudioSource breathe;

    void Start() {
        if (coaster)
        {
            virtCoaster = false;
            cameraHolder.transform.parent = coasterCameraPositionHolder.transform;
            cameraHolder.transform.localPosition = new Vector3(0f, 2f, 0f);
            cameraHolder.transform.Rotate(0, 235, 0);
            breathe = breatheAudioHolder.GetComponent<AudioSource>();
            breathe.time = 56.5f;
            breathe.Play();
            breathe.mute = true; 
        }
        else
        {
            cameraHolder.transform.parent = dropOfDoomCameraPositionHolder.transform;
            cameraHolder.transform.localPosition = new Vector3(0f, 0.8f, 0f);
        }
    }

    void Update()
    {
        /* If the player pressed the cardboard button (or touched the screen), 
         * set the trigger parameter to active (until it has been used in a transition)
         */ 
        if (coaster && Input.GetMouseButtonDown(0))
        {
            SwitchCoasterReality();
        }
    }

    public void Log()
    {
        Debug.Log("touched");
    }

    void SwitchCoasterReality() {
        if (virtCoaster)
        { // switching back to reality
            park.SetActive(true);
            coasterpillars.SetActive(true);
            dropOfDoom.SetActive(true);
            breathe.mute = true;

            coasterVirtualWorld.SetActive(false);
            ChangeMaterial(coasterRealityWorldMaterial, coasterCurves);
            cardboardIndicator.SetActive(false);
            virtCoaster = false;
        }
        else
        { // switching back to virtual 
            park.SetActive(false);
            coasterpillars.SetActive(false);
            dropOfDoom.SetActive(false);
            breathe.mute = false;

            coasterVirtualWorld.SetActive(true);
            ChangeMaterial(coasterVirtualWorldMaterial, coasterCurves);
            cardboardIndicator.SetActive(true);
            virtCoaster = true;
        }
    } 

    void ChangeMaterial(Material space, GameObject targetObject)
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
                ChangeMaterial(space, child.gameObject);
            }
        }
    } 
}
