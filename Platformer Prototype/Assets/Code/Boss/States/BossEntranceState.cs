using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossEntranceState : BossAttackState
{
    private int stage;
    public BossEntranceState(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName, Transform attackPosition) : base(boss, stateMachine, bossData, animBoolName, attackPosition)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        stage = 1;
    }

    public override void Exit()
    {
        base.Exit();
        //stateMachine.ChangeState(boss.bossIdleState);
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

        switch (stage)
        {
            case 1:
                EntranceShake();
                EntranceParticle1();
                break;
            case 2:
                EntranceShake2();
                EntranceParticle2();
                break;
            
        }
        stage++;
    }

    private void EntranceParticle1() => GameObject.Instantiate(bossData.suctionParticle, boss.transform.position, boss.transform.rotation);
    private void EntranceParticle2()
    {
        Debug.Log("Active");
        GameObject.Instantiate(bossData.explosiveParticle1, boss.transform.position, boss.transform.rotation);
        GameObject.Instantiate(bossData.explosiveParticle2, boss.transform.position, boss.transform.rotation);
    }
    private void EntranceShake() => bossData.source1.GenerateImpulse();
    private void EntranceShake2() => bossData.source2.GenerateImpulse();
}
