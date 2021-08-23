using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : BossAttackState
{
    private int stage;
    public BossDeadState(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName, Transform attackPosition) : base(boss, stateMachine, bossData, animBoolName, attackPosition)
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        boss.ReturnToCentre();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        switch (stage)
        {
            case 1:
                deathParticle1();
                DeadShake();
                break;
            case 2:
                Debug.Log("active");
                deathParticle2();
                DeadShake2();
                break;
            case 3:
                boss.gameObject.SetActive(false);
                break;
        }

        stage++;
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    private void deathParticle1() => GameObject.Instantiate(bossData.suctionParticle, boss.transform.position, boss.transform.rotation);

    private void deathParticle2()
    {
        Debug.Log("Active");
        GameObject.Instantiate(bossData.explosiveParticle1, boss.transform.position, boss.transform.rotation);
        GameObject.Instantiate(bossData.explosiveParticle2, boss.transform.position, boss.transform.rotation);
    }
    private void DeadShake() => bossData.source1.GenerateImpulse();
    private void DeadShake2() => bossData.source2.GenerateImpulse();
}
