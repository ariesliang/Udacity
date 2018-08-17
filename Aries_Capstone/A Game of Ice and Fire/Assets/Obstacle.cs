using UnityEngine;

public class Obstacle: MonoBehaviour {

    public static int movingSpeed = 100;
    private bool startMoving;
    private Rigidbody rb;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        if (startMoving)
        { 
            rb.AddForce(-transform.forward * movingSpeed * Time.deltaTime); 
        }
    }

    public void StartMoving ()
    {
        startMoving = true; 
    }
}
