using UnityEngine;

public class SafeNetController : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    { 
        Destroy(collision.gameObject);
    }
}
