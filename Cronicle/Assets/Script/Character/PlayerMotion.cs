using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotion : MonoBehaviour
{
    [Header("Character Input Value")]
    public Vector2 move;

#if ENABLE_INPUT_SYSTEM
    public void OnMove(InputValue value) 
    {
        MoveInput(value.Get<Vector2>());
    }

#endif
    public void MoveInput(Vector2 newMoveInput)
    {
        move = newMoveInput;
    }
    public void JumpInput(Vector2 newJumpInput)
    {
        move = newJumpInput;
    }
}

