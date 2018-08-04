using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraterDasher : MonoBehaviour {

    private Transform grater_end;
    private Vector3 graterEndPos;
    private Vector3 initialPos; 

    void Start()
    {
        initialPos = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grater got entered by:" + other.gameObject.name);
        if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("Entered : " + other.gameObject.name);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true; 
            grater_end = gameObject.transform.Find("Grater_End");
            Debug.Log("Grater_End:" + grater_end); 
            graterEndPos = grater_end.transform.position;
            Debug.Log("Grater End pos:" + graterEndPos);
           // gameObject.transform.position = new Vector3(100, 100, 100);

            iTween.MoveTo(other.gameObject,
                iTween.Hash(
                    "position", graterEndPos,
                    "time", 1F,
                    "easetype", "linear",
                    "oncomplete", "AfterGothroughGrater",
                    "oncompleteparams", other.gameObject,
                    "onCompleteTarget", this.gameObject
                )
            );
        }
    }

    void AfterGothroughGrater(GameObject ball)
    {
      //  gameObject.transform.position = initialPos;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("After go throughgrater:" + ball.GetComponent<Rigidbody>().isKinematic);
    }
}
