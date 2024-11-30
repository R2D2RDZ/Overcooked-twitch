using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        Vector3 moveVelocity = move * moveSpeed;

        // Apply movement velocity while keeping current vertical velocity
        Vector3 velocity = rb.velocity;
        velocity.x = moveVelocity.x;
        velocity.z = moveVelocity.z;
        rb.velocity = velocity;
    }

    void Update()
    {
        // Ground check using a sphere cast
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, groundDistance, groundMask);

        // Visualize the ray in the Scene view
        Debug.DrawRay(transform.position, Vector3.down * groundDistance, isGrounded ? Color.green : Color.red);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
