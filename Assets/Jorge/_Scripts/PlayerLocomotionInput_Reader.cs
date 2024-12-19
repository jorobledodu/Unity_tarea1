using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput_Reader : MonoBehaviour, Controls.IPlayerLocomotionMapActions
{
    public Controls Controls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }

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
}
