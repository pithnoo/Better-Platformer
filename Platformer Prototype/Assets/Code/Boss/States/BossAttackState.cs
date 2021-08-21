using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    protected bool isAnimationFinished;
    protected Transform attackPosition;

    public BossAttackState(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName, Transform attackPosition) : base(boss, stateMachine, bossData, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();

        boss.atsm.attackState = this;
        isAnimationFinished = false;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public virtual void TriggerAttack(){

    }

    public virtual void FinishAttack(){
        stateMachine.ChangeState(boss.bossIdleState);
        isAnimationFinished = true;
    }
}
