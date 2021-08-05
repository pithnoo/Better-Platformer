using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 RawMovementInput {get; private set;}
    public Vector2 RawDashDirectionInput {get; private set;}
    public Vector2Int DashDirectionInput {get; private set;}
    public int NormInputX {get; private set;}
    public int NormInputY {get; private set;}
    public bool JumpInput {get; private set;}
    public bool JumpInputStop {get; private set;}
    public bool GrabInput {get; private set;}
    public bool DashInput {get; private set;}
    public bool DashInputStop {get; private set;}

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float dashInputStartTime;
    private float jumpInputStartTime;

    private void Start(){
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update() {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if(context.canceled){
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context){
        if(context.started){
            GrabInput = true;
        }
        
        if(context.canceled){
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context){
        if(context.started){
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if(context.canceled){
            DashInputStop = true;
        }
    }

    public void UseDashInput() => DashInput = false;
    private void CheckDashInputHoldTime(){
        if(Time.time >= dashInputStartTime + inputHoldTime){
            DashInput = false;
        }
    }
    
    public void OnDashDirectionInput(InputAction.CallbackContext context){
        RawDashDirectionInput = context.ReadValue<Vector2>();

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime(){
        if(Time.time >= jumpInputStartTime + inputHoldTime){
            JumpInput = false;
        }
    }
}
