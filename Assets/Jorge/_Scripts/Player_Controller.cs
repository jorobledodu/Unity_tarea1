using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Player_Controller : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;

    [Header("Base Movement")]
    [SerializeField] private float _runAcceleration = 35f;
    [SerializeField] private float _runSpeed = 4f;
    [SerializeField] private float _sprintAcceleration = 50f;
    [SerializeField] private float _sprintSpeed = 7f;
    [SerializeField] private float _drag = 20f;
    [SerializeField] private float _gravity = 25f;
    [SerializeField] private float _jumpSpeed = 1.0f;
    [SerializeField] private float _movingThreshold = 0.01f;

    [Header("Camera Settings")]
    [SerializeField] private float _lookSensitivityH = 0.1f;
    [SerializeField] private float _lookSensitivityV = 0.1f;
    [SerializeField] private float _lookLimitV = 89f;

    private PlayerLocomotionInput_Reader _playerInputReader;
    private Player_State _playerState;

    private Vector2 _cameraRotation = Vector2.zero;
    private Vector2 _playerTargetRotation = Vector2.zero;

    private float _verticalVelocity = 0f;

    private void Awake()
    {
        _playerInputReader = GetComponent<PlayerLocomotionInput_Reader>();
        _playerState = GetComponent<Player_State>();
    }

    private void Update()
    {
        UpdateMovementState();
        HandleVerticalMovement();
        HandleLateralMovement();
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
    }

    private void UpdateMovementState()
    {
        bool isMovementInput = _playerInputReader.MovementInput != Vector2.zero;
        bool isMovingLaterally = IsMovingLaterally();
        bool isSprinting = _playerInputReader.SprintToggledOn && isMovingLaterally;
        bool isGrounded = IsGrounded();

        PlayerMovementState movementState = isSprinting ? PlayerMovementState.Sprinting :
                                           isMovingLaterally || isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;

        _playerState.SetPlayerMovementState(movementState);

        // Control Airborn State
        if (!isGrounded && _characterController.velocity.y > 0f)
        {
            _playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
        }
        else if (!isGrounded && _characterController.velocity.y <= 0f)
        {
            _playerState.SetPlayerMovementState(PlayerMovementState.Falling);
        }
    }

    private void HandleVerticalMovement()
    {
        bool isGrounded = _playerState.IsGroundedState();

        if (isGrounded && _verticalVelocity < 0f) _verticalVelocity = 0f;

        _verticalVelocity -= _gravity * Time.deltaTime;

        if (_playerInputReader.JumpPressed && isGrounded)
        {
            _verticalVelocity += Mathf.Sqrt(_jumpSpeed * 3 * _gravity);
        }
    }

    private void HandleLateralMovement()
    {
        bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
        bool isGrounded = _playerState.IsGroundedState();

        float lateralAcceleration = isSprinting ? _sprintAcceleration : _runAcceleration;
        float clampLateralSpeed = isSprinting ? _sprintSpeed : _runSpeed;

        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * _playerInputReader.MovementInput.x + cameraForwardXZ * _playerInputReader.MovementInput.y;

        Vector3 movementDelta = movementDirection * lateralAcceleration * Time.deltaTime;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        // Add drag to player
        Vector3 currentDrag = newVelocity.normalized * _drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > _drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, clampLateralSpeed);
        newVelocity.y += _verticalVelocity;

        _characterController.Move(newVelocity * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        _cameraRotation.x += _lookSensitivityH * _playerInputReader.LookInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - _lookSensitivityV * _playerInputReader.LookInput.y, -_lookLimitV, _lookLimitV);

        _playerTargetRotation.x += transform.eulerAngles.x + _lookSensitivityH * _playerInputReader.LookInput.x;
        transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);

        _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
    }

    private bool IsMovingLaterally()
    {
        Vector3 lateralVelocity = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.z);
        return lateralVelocity.magnitude > _movingThreshold;
    }

    private bool IsGrounded()
    {
        return _characterController.isGrounded;
    }
}