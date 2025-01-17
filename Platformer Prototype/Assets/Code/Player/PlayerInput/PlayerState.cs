using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    protected bool isAnimationFinished;
    private string animBoolName;
    protected bool isExitingState;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName){
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter(){
        DoChecks();
        startTime = Time.time;
        player.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;

        //Debug.Log(animBoolName);
    }

    public virtual void Exit(){
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate(){
        
    }

    public virtual void PhysicsUpdate(){
        DoChecks();
    }

    public virtual void DoChecks(){

    }

    public virtual void AnimationTrigger(){

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
