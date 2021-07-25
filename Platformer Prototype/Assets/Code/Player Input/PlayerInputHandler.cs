using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movementInput;

    public void OnMoveInput(InputAction.CallbackContext context){
        //Debug.Log("IM MOVING");
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            Debug.Log("Jump button pushed");
        }

        if(context.performed){
            Debug.Log("Jump being held");
        }

        if(context.canceled){
            Debug.Log("Jump released");
        }

        //Debug.Log("IM JUMPING");
    }


}
