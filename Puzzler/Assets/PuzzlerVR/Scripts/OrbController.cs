using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    public GameObject s1;
    public GameObject o;
    public GameObject s2;

    private void Start()
    {
        iTween.MoveTo(s1, iTween.Hash("path", iTweenPath.GetPath("s_shape_1"), 
                                            "time", 10f, 
                                            "loopType", "pingPong", 
                                            "easeType", iTween.EaseType.easeInOutSine));
        iTween.MoveTo(o, iTween.Hash("path", iTweenPath.GetPath("o_shape"),
                                            "time", 10f,
                                            "loopType", "pingPong",
                                            "easeType", iTween.EaseType.easeInOutSine));
        iTween.MoveTo(s2, iTween.Hash("path", iTweenPath.GetPath("s_shape_2"),
                                           "time", 10f,
                                           "loopType", "pingPong",
                                           "easeType", iTween.EaseType.easeInOutSine));
    }
}


