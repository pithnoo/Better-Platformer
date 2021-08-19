using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteAttackHorizontal : BossAttackState
{
    private int randomStart;
    public SilhouetteAttackHorizontal(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName) : base(boss, stateMachine, bossData, animBoolName)
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
        randomStart = Random.Range(1,3);
        
        switch(randomStart){
            case 1:
            break;
            case 2:
            break;
        }
    }
}
