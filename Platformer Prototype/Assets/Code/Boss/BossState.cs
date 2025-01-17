using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
    protected BossStateMachine stateMachine;
    protected Boss boss;
    protected BossData bossData;
    protected BossData bossData2;
    protected float startTime;
    protected string animBoolName;

    public BossState(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName){
        this.boss = boss;
        this.stateMachine = stateMachine;
        this.bossData = bossData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter(){
        startTime = Time.time;
        boss.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit(){
        boss.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate(){
        DoChecks();
    }

    public virtual void PhysicsUpdate(){
        DoChecks();
    }

    public virtual void DoChecks(){

    }
}
