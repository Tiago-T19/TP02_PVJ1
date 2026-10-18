using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
  

        rb.linearVelocity = new Vector3(1f*speed, rb.linearVelocity.y, rb.linearVelocity.z);

    }
}
