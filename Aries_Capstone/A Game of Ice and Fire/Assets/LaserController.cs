using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    public GameObject blocker;
    public float blockerYOffset; 

    private int layerMask; 
    private Vector3 endPoint;
    private LineRenderer laser;
    private float speed;
    private float lastHitDistance;

    // Use this for initialization
    void Start () {
        laser = GetComponentInChildren<LineRenderer>(); 
        layerMask = LayerMask.NameToLayer("Ground"); 
        // Bit shift the index of the layer (8) to get a bit mask
        layerMask = 1 << layerMask; 
        speed = 1; 
    }

    void Update()
    {
        RaycastHit hit; 
        float step = 10 * Time.deltaTime; 
        // Does the ray intersect any objects excluding the player layer
        SetLaserStart(gameObject.transform.position);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            endPoint = hit.point + new Vector3(0, blockerYOffset, 0);
            lastHitDistance = hit.distance;
        } 
        else
        {
            endPoint = transform.position + transform.TransformDirection(Vector3.forward) * lastHitDistance;
        }
        blocker.transform.position = endPoint;
        SetLaserEnd(endPoint); 
    } 

    void SetLaserStart(Vector3 startPos)
    {
        laser.SetPosition(0, startPos);
    }

    void SetLaserEnd(Vector3 endPos)
    {
        laser.SetPosition(1, endPos);
    }

    public void DeactivateLaserAndBlock ()
    {
        laser.gameObject.SetActive(false);
        blocker.gameObject.SetActive(false);
    }

    public void ActivateLaserAndBlock()
    {
        laser.gameObject.SetActive(true);
        blocker.gameObject.SetActive(true);
    }
}
