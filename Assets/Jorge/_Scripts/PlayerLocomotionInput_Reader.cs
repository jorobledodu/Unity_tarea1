using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput_Reader : MonoBehaviour, Controls.IPlayerLocomotionMapActions
{
    [SerializeField] private bool _holdToSprint = true;

    public bool SprintToggledOn { get; private set; }
    public bool WalkToggledOn { get; private set; }
    public Controls Controls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }

    private void OnEnable()
    {
        Controls = new Controls();
        Controls.Enable();

        Controls.PlayerLocomotionMap.Enable();
        Controls.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        Controls.PlayerLocomotionMap.Disable();
        Controls.PlayerLocomotionMap.RemoveCallbacks(this);
    }

    private void LateUpdate()
    {
        JumpPressed = false;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnToggleSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SprintToggledOn = _holdToSprint || !SprintToggledOn;
        }
        else if (context.canceled)
        {
            SprintToggledOn = !_holdToSprint && SprintToggledOn;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        JumpPressed = true;
    }

    public void OnToggleWalk(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        WalkToggledOn = !WalkToggledOn;
    }
}