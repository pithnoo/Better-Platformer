using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : BossAttackState
{
    private int stage;
    public LazerAttack(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName) : base(boss, stateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        switch(stage){
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
