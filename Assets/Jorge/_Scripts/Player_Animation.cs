using System.Linq;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float locomotionBlendSpeed = 4f;

    private PlayerLocomotionInput_Reader _playerLocomotionInputReader;
    private Player_State _playerState;
    private Player_Controller _playerController;

    // Locomotion
    private static int inputXHash = Animator.StringToHash("inputX");
    private static int inputYHash = Animator.StringToHash("inputY");
    private static int inputMagnitudeHash = Animator.StringToHash("inputMagnitude");
    private static int isIdleHash = Animator.StringToHash("isIdle");
    private static int isGroundedHash = Animator.StringToHash("isGrounded");
    private static int isFallingHash = Animator.StringToHash("isFalling");
    private static int isJumpingHash = Animator.StringToHash("isJumping");

    // Camera/Rotation
    private static int isRotatingToTargetHash = Animator.StringToHash("isRotatingToTarget");
    private static int rotationMismatchHash = Animator.StringToHash("rotationMismatch");

    private Vector3 _currentBlendInput = Vector3.zero;

    private float _sprintMaxBlendValue = 1.5f;
    private float _runMaxBlendValue = 1.0f;
    private float _walkMaxBlendValue = 0.5f;

    private void Awake()
    {
        _playerLocomotionInputReader = GetComponent<PlayerLocomotionInput_Reader>();
        _playerState = GetComponent<Player_State>();
        _playerController = GetComponent<Player_Controller>();
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        bool isIdle = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idle;
        bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
        bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
        bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
        bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
        bool isGrounded = _playerState.InGroundedState();

        bool isRunBlendValue = isRunning || isJumping || isFalling;

        Vector2 inputTarget = isSprinting ? _playerLocomotionInputReader.MovementInput * _sprintMaxBlendValue :
                              isRunBlendValue ? _playerLocomotionInputReader.MovementInput * _runMaxBlendValue :
                                                _playerLocomotionInputReader.MovementInput * _walkMaxBlendValue;

        _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);

        _animator.SetBool(isGroundedHash, isGrounded);
        _animator.SetBool(isIdleHash, isIdle);
        _animator.SetBool(isFallingHash, isFalling);
        _animator.SetBool(isJumpingHash, isJumping);
        _animator.SetBool(isRotatingToTargetHash, _playerController.IsRotatingToTarget);

        _animator.SetFloat(inputXHash, _currentBlendInput.x);
        _animator.SetFloat(inputYHash, _currentBlendInput.y);
        _animator.SetFloat(inputMagnitudeHash, _currentBlendInput.magnitude);
        _animator.SetFloat(rotationMismatchHash, _playerController.RotationMismatch);
    }
}
