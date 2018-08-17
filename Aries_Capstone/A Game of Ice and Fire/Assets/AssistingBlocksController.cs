using UnityEngine;

public class AssistingBlocksController : MonoBehaviour {

    private Rigidbody rb;
    public static int movingSpeed;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(-transform.forward * movingSpeed * 3 * Time.deltaTime);
    } 
}
