using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.FindObjectOfType<AudioManager>().Play("Jump");
        
        player.InputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJumps();
        player.InAirState.SetIsJumping();
    }

    public bool CanJump(){
        if(amountOfJumpsLeft > 0){
            return true;
        }
        else{
            return false;
        }
    }

    public void ResetAmountOfJumps() => amountOfJumpsLeft = playerData.amountOfJumps;
    public void DecreaseAmountOfJumps() => amountOfJumpsLeft--;

}
