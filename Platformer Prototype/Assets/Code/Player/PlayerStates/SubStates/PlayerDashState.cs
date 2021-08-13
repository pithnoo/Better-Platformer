using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash {get; private set;}
    private bool isHolding;
    //private bool DashInputStop;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAfterImagePosition;

    private float lastDashTime;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter(){
        base.Enter();
        isHolding = true;
        CanDash = false;
        player.InputHandler.UseDashInput();
        dashDirection = Vector2.right * player.FacingDirection;
        startTime = Time.time;
    }

    public override void Exit(){
        base.Exit();

        if(player.CurrentVelocity.y > 0){
            player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate(){
        base.LogicUpdate();

        if(!isExitingState){
            if(isHolding){
                player.Dashing();
                //determine dash direction
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                //DashInputStop = player.InputHandler.DashInputStop;
                if(dashDirectionInput != Vector2.zero){
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                isHolding = false;
                startTime = Time.time;
                player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x)); 
                player.RB.drag = playerData.drag;
                player.SetDashVelocity(playerData.dashVelocity, dashDirection);
                //PlaceAfterImage();
                
            }
            else{
                CheckIfShouldPlaceAfterImage();
                player.SetDashVelocity(playerData.dashVelocity, dashDirection);
                if(Time.time >= startTime + playerData.dashTime){
                    player.RB.drag = 0f;
                    player.NotDashing();
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
            //float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
            //optional for dash indicator

        }
    }

    public bool CheckIfCanDash(){
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;

    private void PlaceAfterImage(){
        PlayerAfterImagePool.instance.GetFromPool();
        lastAfterImagePosition = player.transform.position;
    }

    private void CheckIfShouldPlaceAfterImage(){
        if(Vector2.Distance(player.transform.position, lastAfterImagePosition) >= playerData.distanceBetweenAfterImages){
            PlaceAfterImage();
        }
    }
}
