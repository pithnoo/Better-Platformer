using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool GrabInput;
    protected bool JumpInput;
    protected bool DashInput;
    protected int xInput;
    protected int yInput;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        GrabInput = player.InputHandler.GrabInput;
        JumpInput = player.InputHandler.JumpInput;
        DashInput = player.InputHandler.DashInput;

        if(JumpInput && player.canMove){
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(isGrounded && !GrabInput){
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (xInput != player.FacingDirection && !GrabInput)){
            stateMachine.ChangeState(player.InAirState);
        }
        else if(DashInput && player.DashState.CheckIfCanDash() && player.canMove){
            player.DashShake();
            stateMachine.ChangeState(player.DashState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
