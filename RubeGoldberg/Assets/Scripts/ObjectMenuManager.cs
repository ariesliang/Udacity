using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;

    // inventory 
    public int[] inventory;
    public GameObject inventoryRunoutAudio;
    public Text[] inventoriesInfo;

    public int currentObject = 0;

    // Audio
    public GameObject swipeAudio;
    public GameObject spawnAudio;

    // Use this for initialization
    void Start () {
		foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
        for (int i = 0; i < inventory.Length; ++i)
        {
            inventoriesInfo[i].text = "Inventory: " + inventory[i];
        } 
    }

    public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
        swipeAudio.GetComponent<AudioSource>().Play();
    }

    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject == objectList.Count)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
        swipeAudio.GetComponent<AudioSource>().Play();
    }

    public void DisableMenu()
    {
        objectList[currentObject].SetActive(false);
    }

    public void SpawnCurrentObject()
    {
        if (inventory[currentObject] == 0)
        {
            // inventoryRunoutAudio.GetComponent<AudioSource>().Play();
        }
        else
        {
            inventory[currentObject]--;
            inventoriesInfo[currentObject].text = "Inventory: " + inventory[currentObject];
            Instantiate(objectPrefabList[currentObject],
            objectList[currentObject].transform.position,
            objectList[currentObject].transform.rotation);
            spawnAudio.GetComponent<AudioSource>().Play();
        } 
    }
}
