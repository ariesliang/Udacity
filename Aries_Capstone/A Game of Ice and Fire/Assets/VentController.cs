using UnityEngine;

public class VentController : MonoBehaviour {

    public GameObject ventBallPrefab;
    public GameObject ventBallHolder;

    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnVentBall", 0.0f, 1f);
    } 

    private void SpawnVentBall()
    {
        GameObject o = Instantiate(ventBallPrefab, ventBallHolder.transform.position, Quaternion.identity);
        o.SendMessage("StartMoving"); 
    }
 }
