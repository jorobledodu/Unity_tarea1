using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float locomotionBlendSpeed = 4f;

    private PlayerLocomotionInput_Reader _playerInputReader;
    private Player_State _playerState;

    private static int _inputXHash = Animator.StringToHash("inputX");
    private static int _inputYHash = Animator.StringToHash("inputY");
    private static int _inputMagnitudeHash = Animator.StringToHash("inputMagnitude");
    private static int _isGroundedHash = Animator.StringToHash("isGrounded");
    private static int _isFallingHash = Animator.StringToHash("isFalling");
    private static int _isJumpingHash = Animator.StringToHash("isJumping");

    private Vector3 _currentBlendInput = Vector3.zero;

    private void Awake()
    {
        _playerInputReader = GetComponent<PlayerLocomotionInput_Reader>();
        _playerState = GetComponent<Player_State>();
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
            bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
            bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
            bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
            bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
            bool isGrounded = _playerState.IsGroundedState();

            Vector2 inputTarget = isSprinting ? _playerInputReader.MovementInput * 1.5f : _playerInputReader.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);

            _animator.SetBool(_isGroundedHash, isGrounded);
            _animator.SetBool(_isFallingHash, isFalling);
            _animator.SetBool(_isJumpingHash, isJumping);
            _animator.SetFloat(_inputXHash, _currentBlendInput.x);
            _animator.SetFloat(_inputYHash, _currentBlendInput.y);
            _animator.SetFloat(_inputMagnitudeHash, _currentBlendInput.magnitude);
    }
}
