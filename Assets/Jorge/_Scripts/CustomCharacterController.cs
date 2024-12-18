using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CustomCharacterController : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float radius = 0.25f;
    public float skinWidth = 0.1f;

    private Vector3 velocity;
    private Vector3 moveDirection;
    private Rigidbody rb;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public bool isThirdPerson = true;
    public Vector3 thirdPersonOffset = new Vector3(0, 2, -4);
    public Vector3 firstPersonOffset = new Vector3(0, 1.8f, 0);

    [Header("Ground Detection")]
    public float groundCheckDistance = 0.2f; // Raycast distance to check the ground
    public LayerMask groundLayer; // Define which layer is ground
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        // collider.radius = radius;
        // collider.height = 2.0f;
        // collider.center = Vector3.up;

        SetupCamera();
    }

    private void Update()
    {
        HandleInput();
        CheckGround();
        ApplyGravity();
        MoveWithCollisionAndSlide();
        UpdateCameraPosition();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
    }

    void CheckGround()
    {
        // Raycast to detect ground
        Ray groundRay = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(groundRay, out RaycastHit hit, groundCheckDistance + radius, groundLayer);

        // Debugging the ground ray
        Color rayColor = isGrounded ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector3.down * (groundCheckDistance + radius), rayColor);

        if (isGrounded)
        {
            velocity.y = -2f; // Reset downward velocity to avoid accumulating gravity
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    void MoveWithCollisionAndSlide()
    {
        Vector3 totalMovement = moveDirection * speed * Time.deltaTime + velocity * Time.deltaTime;
        Vector3 finalPosition = transform.position + totalMovement;

        // SphereCast to detect collisions
        if (Physics.SphereCast(transform.position, radius, totalMovement.normalized, out RaycastHit hit, totalMovement.magnitude + skinWidth))
        {
            Vector3 slideDirection = Vector3.ProjectOnPlane(totalMovement, hit.normal);
            finalPosition = transform.position + slideDirection;

            // Debug collision normal
            Debug.DrawLine(transform.position, transform.position + hit.normal, Color.blue);
        }

        transform.position = finalPosition;
    }

    void SetupCamera()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            if (isThirdPerson)
            {
                cameraTransform.position = transform.position + thirdPersonOffset;
                cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
            }
            else
            {
                cameraTransform.position = transform.position + firstPersonOffset;
                cameraTransform.rotation = transform.rotation;
            }
        }
    }
}