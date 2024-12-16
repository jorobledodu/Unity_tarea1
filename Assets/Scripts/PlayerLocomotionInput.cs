using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Personaje
{
    [DefaultExecutionOrder(-2)]
 public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocoMotionMapaActions
    {
        public PlayerControls PlayerControls { get ; private set; }
        public Vector2 MovementInput;
        public Vector2 LookInput;
        private void OnEnable()
        {
            PlayerControls = new PlayerControls();
            PlayerControls.Enable ();

            PlayerControls.PlayerLocoMotionMapa.Enable ();
            PlayerControls.PlayerLocoMotionMapa.SetCallbacks (this);
        }

        private void OnDisable()
        {
            PlayerControls.PlayerLocoMotionMapa.Disable ();
            PlayerControls.PlayerLocoMotionMapa.RemoveCallbacks(this);
        }

        public void OnMovimiento(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            print (MovementInput);
        }

        public void OnMirar(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
        }
    }
}

