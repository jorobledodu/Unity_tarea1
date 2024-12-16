using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personaje
{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementState CurrentPlaterMovementState { get; private set; } = PlayerMovementState.Idling;

        public enum PlayerMovementState
        {
            Idling = 0,
            Walking = 1,
            Running = 2,
            Sprinting = 3,
            Jumping = 4,
            Falling = 5,
            Strafing = 6,
        }
    }

}
