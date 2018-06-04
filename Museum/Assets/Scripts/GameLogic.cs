using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public GameObject rollercoaster;
    public GameObject curves;
    public GameObject pillars;
    public Material space;

	// Use this for initialization
	void Start () {
        //MakeInvisible(rollercoaster);
        ChangeRollerCoasterMaterial(space, curves);
        MakeInvisible(pillars);
    }

    void MakeInvisible (GameObject targetObject)
    {
        if (targetObject != null && !targetObject.CompareTag("Visible") && !targetObject.CompareTag("MainCamera")) {
            if (targetObject.GetComponent<Renderer>() != null)
            {
                targetObject.GetComponent<Renderer>().enabled = false;
            }
            foreach (Transform child in targetObject.transform)
            {
                MakeInvisible(child.gameObject);
            }
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
