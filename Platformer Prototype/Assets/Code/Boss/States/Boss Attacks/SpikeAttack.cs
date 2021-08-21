using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : BossAttackState
{
    private int stage;
    public SpikeAttack(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName, Transform attackPosition) : base(boss, stateMachine, bossData, animBoolName, attackPosition)
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
        boss.transform.position = boss.escapePoint.transform.position;
        switch (stage)
        {
            case 1:
                SpawnSpikes(boss.bottomSpikePositions);
                break;
            case 2:
                SpawnSpikes(boss.topSpikePositions);
                break;
            case 3:
                SpawnSpikes(boss.bottomSpikePositions);
                break;
            case 4:
                SpawnSpikes(boss.topSpikePositions);
                break;
        }
        stage++;
    }

    private void SpawnSpikes(List<Transform> spikePositions){
        for(int i = 0; i < spikePositions.Count; i++){
            boss.SpawnGameObject("Lazer", spikePositions[i]);
        }
    }
}
