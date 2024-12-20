using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput_Reader : MonoBehaviour, Controls.IPlayerLocomotionMapActions
{
    [SerializeField] private bool holdToSprint = true;

    public Controls Controls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool SprintToggledOn { get; private set; }

    private void OnEnable()
    {
        Controls = new Controls();
        Controls.Enable();

        Controls.PlayerLocomotionMap.Enable();
        Controls.PlayerLocomotionMap.SetCallbacks(this);
    }
    void OnDisable()
    {
        Controls.PlayerLocomotionMap.Disable();
        Controls.PlayerLocomotionMap.RemoveCallbacks(this);
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
            SprintToggledOn = holdToSprint || !SprintToggledOn;
        }
        else if (context.canceled)
        {
            SprintToggledOn = !holdToSprint && SprintToggledOn;
        }
    }
}
