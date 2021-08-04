using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool GrabInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        
    }
    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumps();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        GrabInput = player.InputHandler.GrabInput;

        if(JumpInput && player.JumpState.CanJump()){
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if(!isGrounded){
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && GrabInput){
            stateMachine.ChangeState(player.WallGrabState);
        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
