using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOfDoomCartController : MonoBehaviour {

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
                                            "oncomplete", "MoveUp"
                                            ));
    }
}
