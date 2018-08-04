using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
 
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private bool oculus;

    // Teleporter
    private LineRenderer laser;
    private GameObject teleportAimerObject;
    private Vector3 teleportLocation;
    private GameObject player; 
    private static float yNudgeAmount = 0f; // specific to teleportAimerObject height
    private static readonly Vector3 yNudgeVector = new Vector3(0f, yNudgeAmount, 0f);

    // Audio
    public GameObject teleportAudio;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        laser = GetComponentInChildren<LineRenderer>();
        laser.gameObject.SetActive(false);
        teleportAimerObject = GameObject.FindGameObjectWithTag("TeleportAimer");
        Debug.Log("TeleportAimer:" + teleportAimerObject);
        teleportAimerObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Player:" + player); 
        oculus = HeadSetManager.oculus;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Oculus:" + oculus);
       // if (!oculus) device = SteamVR_Controller.Input((int)trackedObject.index);
        if (oculus && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Trigger pressed");
            laser.gameObject.SetActive(true);
            teleportAimerObject.SetActive(true);

            setLaserStart(gameObject.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1, 0))
            {
                teleportLocation = hit.point;
            }
            else
            {
                teleportLocation = transform.position + 1 * transform.forward;
                RaycastHit groundRay;
                if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 1.5f, 0))
                {
                    teleportLocation.y = groundRay.point.y;
                }
            }
            setLaserEnd(teleportLocation);
            // aimer
            teleportAimerObject.transform.position = teleportLocation + yNudgeVector;
        }

        if ((oculus && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) || (!oculus && device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)))
        {
            laser.gameObject.SetActive(false);
            teleportAimerObject.SetActive(false);
            player.transform.position = teleportLocation;
            teleportAudio.GetComponent<AudioSource>().Play();
        }
    }

    void setLaserStart(Vector3 startPos)
    {
        laser.SetPosition(0, startPos);
    }

    void setLaserEnd(Vector3 endPos)
    {
        laser.SetPosition(1, endPos);
    }

}
