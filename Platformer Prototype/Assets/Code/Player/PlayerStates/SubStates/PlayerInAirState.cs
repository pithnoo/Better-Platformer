using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private bool jumpInput;
    private bool jumpInputStop;
    private int xInput;
    private bool GrabInput;
    private bool DashInput;


    //Checks
    private bool isGrounded;
    private bool isJumping;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    
    //Timers
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        
    }
    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack  && (oldIsTouchingWall || oldIsTouchingWallBack)){
            StartWallJumpCoyoteTime();
        }
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        GrabInput = player.InputHandler.GrabInput;
        DashInput = player.InputHandler.DashInput;

        CheckCoyoteTime();
        CheckJumpMultiplier();
        CheckWallJumpCoyoteTime();
        
        if(isGrounded && player.CurrentVelocity.y < 0.01f){
            stateMachine.ChangeState(player.LandState);
            // Debug.Log("works");
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime)){
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if(jumpInput && player.JumpState.CanJump()){
            stateMachine.ChangeState(player.JumpState);
        }
        else if(isTouchingWall && GrabInput){
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if(isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0){
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if(DashInput && player.DashState.CheckIfCanDash()){
            player.DashShake();
            stateMachine.ChangeState(player.DashState);
        }
        else{
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier(){
        if(isJumping){
            if(jumpInputStop){
                player.SetVelocityY(player.CurrentVelocity.y * playerData.jumpHeightMultiplier);
                isJumping = false;
            }
            else if(player.CurrentVelocity.y <= 0){
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime(){
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime){
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumps();
        }
    }

    private void CheckWallJumpCoyoteTime(){
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime){
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;
    public void StartWallJumpCoyoteTime(){
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time; 
    }
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;
    public void SetIsJumping() => isJumping = true;
}
