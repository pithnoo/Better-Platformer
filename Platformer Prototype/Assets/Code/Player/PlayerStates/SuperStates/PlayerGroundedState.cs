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
    private bool DashInput;
    private bool SoulInput;

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
        player.DashState.ResetCanDash();
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
        DashInput = player.InputHandler.DashInput;
        SoulInput = player.InputHandler.SoulInput;

        if(JumpInput && player.JumpState.CanJump()){
            stateMachine.ChangeState(player.JumpState);
        }
        else if(!isGrounded){
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(isTouchingWall && GrabInput){
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if(DashInput && player.DashState.CheckIfCanDash()){
            player.DashShake();
            stateMachine.ChangeState(player.DashState);
        }
        else if(SoulInput){
            Debug.Log("Entered soul state");
            stateMachine.ChangeState(player.ChargeState);
        }

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
