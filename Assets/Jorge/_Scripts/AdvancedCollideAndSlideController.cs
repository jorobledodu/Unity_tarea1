using UnityEngine;

public class AdvancedCollideAndSlideController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _maxSlopeAngle = 45f;
    [SerializeField] private int _maxBounces = 5;
    [SerializeField] private float _skinWidth = 0.05f;
    [SerializeField] private float _groundCheckDistance = 0.3f; // Fixed raycast distance for ground detection
    [SerializeField] private LayerMask _collisionMask;

    private CapsuleCollider _capsuleCollider; // Reference to the CapsuleCollider
    private Vector3 _velocity;

    private void Start()
    {
        // Automatically get the CapsuleCollider component
        _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider not found! Please attach a CapsuleCollider to this GameObject.");
        }
    }

    private void Update()
    {
        HandleInput();
        ResolveMovement();
        SnapToGround();
        DrawDebugVisuals();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        inputDirection = Camera.main.transform.TransformDirection(inputDirection);
        inputDirection.y = 0;

        _velocity = inputDirection * _speed;
    }

    private void ResolveMovement()
    {
        Vector3 totalMovement = CollideAndSlide(_velocity * Time.deltaTime, transform.position, 0, false);
        totalMovement += CollideAndSlide(Vector3.up * _gravity * Time.deltaTime, transform.position + totalMovement, 0, true);

        transform.position += totalMovement;
    }

    private Vector3 CollideAndSlide(Vector3 velocity, Vector3 position, int depth, bool isGravityPass)
    {
        if (depth > _maxBounces)
            return Vector3.zero;

        float distance = velocity.magnitude + _skinWidth;
        RaycastHit hit;

        if (Physics.SphereCast(position, _capsuleCollider.radius, velocity.normalized, out hit, distance, _collisionMask))
        {
            Vector3 snapToSurface = velocity.normalized * (hit.distance - _skinWidth);
            Vector3 remainingMovement = velocity - snapToSurface;

            float slopeAngle = Vector3.Angle(Vector3.up, hit.normal);

            // Stop movement on steep slopes
            if (slopeAngle > _maxSlopeAngle)
            {
                Debug.Log("Steep slope detected. Movement stopped.");
                return Vector3.zero;
            }

            // Project remaining velocity onto the surface
            remainingMovement = ProjectAndScale(remainingMovement, hit.normal);

            // Debug: Show collision point and normal
            DebugDrawSphere(hit.point, 0.1f, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.red);

            return snapToSurface + CollideAndSlide(remainingMovement, position + snapToSurface, depth + 1, isGravityPass);
        }

        return velocity;
    }

    private Vector3 ProjectAndScale(Vector3 vector, Vector3 normal)
    {
        float magnitude = vector.magnitude;
        vector = Vector3.ProjectOnPlane(vector, normal).normalized;
        return vector * magnitude;
    }

    private void SnapToGround()
    {
        // Ensure we have the capsule collider data
        if (_capsuleCollider == null) return;

        // Calculate ray origin: bottom of the capsule
        float capsuleBottom = transform.position.y - (_capsuleCollider.height / 2) + _capsuleCollider.radius;
        Vector3 rayOrigin = new Vector3(transform.position.x, capsuleBottom + _groundCheckDistance, transform.position.z);

        // Perform the raycast
        Ray groundRay = new Ray(rayOrigin, Vector3.down);
        if (Physics.Raycast(groundRay, out RaycastHit hit, _groundCheckDistance + _capsuleCollider.radius, _collisionMask))
        {
            // Adjust the character position to align the capsule bottom with the ground
            float groundOffset = _capsuleCollider.height / 2 - _capsuleCollider.radius;
            transform.position = new Vector3(transform.position.x, hit.point.y + groundOffset, transform.position.z);

            // Debug: Show the ground ray and hit point
            Debug.DrawRay(rayOrigin, Vector3.down * (_groundCheckDistance + _capsuleCollider.radius), Color.green);
            DebugDrawSphere(hit.point, 0.1f, Color.green);
        }
    }

    private void DrawDebugVisuals()
    {
        if (_capsuleCollider == null) return;

        Vector3 capsuleTop = transform.position + Vector3.up * (_capsuleCollider.height / 2);
        Vector3 capsuleBottom = transform.position - Vector3.up * (_capsuleCollider.height / 2);

        Debug.DrawLine(capsuleTop, capsuleBottom, Color.yellow);
        Debug.DrawRay(transform.position, transform.forward, Color.cyan);
    }

    private void DebugDrawSphere(Vector3 center, float radius, Color color, int segments = 12)
    {
        float step = Mathf.PI * 2 / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = step * i;
            float nextAngle = step * (i + 1);

            // XY Plane
            Vector3 point1XY = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Vector3 point2XY = center + new Vector3(Mathf.Cos(nextAngle), Mathf.Sin(nextAngle), 0) * radius;
            Debug.DrawLine(point1XY, point2XY, color);

            // XZ Plane
            Vector3 point1XZ = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 point2XZ = center + new Vector3(Mathf.Cos(nextAngle), 0, Mathf.Sin(nextAngle)) * radius;
            Debug.DrawLine(point1XZ, point2XZ, color);

            // YZ Plane
            Vector3 point1YZ = center + new Vector3(0, Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Vector3 point2YZ = center + new Vector3(0, Mathf.Cos(nextAngle), Mathf.Sin(nextAngle)) * radius;
            Debug.DrawLine(point1YZ, point2YZ, color);
        }
    }
}