using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropOfDoomCartController : MonoBehaviour {

    public GameObject cameraHolder;
    public GameObject dropOfDoomCameraPositionHolder;

    // Use this for initialization
    void Start () {
        MoveUp();
    }
	
    void MoveUp()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 110,
                                            "time", 10f,
                                            "loopType", "none",
                                            "easeType", iTween.EaseType.easeInOutSine,
                                            "oncomplete", "MoveDown"
                                            ));
    }

    void MoveDown()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 2,
                                            "time", 3f,
                                            "loopType", "none",
                                            "easeType", iTween.EaseType.easeInOutSine,
                                            "oncomplete", "ChangeScene"
                                            ));
    }

    void ChangeScene()
    {
        if (!GameLogic.coaster)
        { 
            Debug.Log("Entered");
            GameLogicMuseum.playerPosition = new Vector3(8, 2.1f, 9);
            SceneManager.LoadSceneAsync("Showroom");
        }
    }
}
