using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Player_State : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState { get; private set; } = PlayerMovementState.Idle;

        public void SetPlayerMovementState(PlayerMovementState playerMovementState)
        {
            CurrentPlayerMovementState = playerMovementState;
        }

        public bool InGroundedState()
        {
            return IsStateGroundedState(CurrentPlayerMovementState);
        }

        public bool IsStateGroundedState(PlayerMovementState movementState)
        {
            return movementState == PlayerMovementState.Idle ||
                   movementState == PlayerMovementState.Walking ||
                   movementState == PlayerMovementState.Running ||
                   movementState == PlayerMovementState.Sprinting;
        }
    }
    public enum PlayerMovementState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
        Sprinting = 3,
        Jumping = 4,
        Falling = 5,
        Strafing = 6,
    }