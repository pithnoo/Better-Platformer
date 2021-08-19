using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    private int currentAttack;
    private int numAttacks;
    private bool isIdleTimeOver;
    private int idleTime;
    public BossIdleState(Boss boss, BossStateMachine stateMachine, BossData bossData, string animBoolName) : base(boss, stateMachine, bossData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Enter()
    {
        base.Enter();

        SetRandomIdleTime();
        boss.ReturnToCentre();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + idleTime){
            AttackPlayer();
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        boss.BossFlight();
        boss.LookTowardsEntity();
    }

    private void AttackPlayer(){
        if(numAttacks < bossData.maxAttacks){
            currentAttack = Random.Range(1, 3);
            Debug.Log(currentAttack);
            if (boss.attackType == Boss.bossAttackType.SOUL)
            {
                DecideAttackSoul();
            }
            else if (boss.attackType == Boss.bossAttackType.PLAYER)
            {
                DecideAttackPlayer();
            }
            numAttacks++;
        }
        else{
            //execute burst attack
            Debug.Log("burst attack");
        }
    }

    private void DecideAttackPlayer(){
        switch(currentAttack){
            case 1:
            //spike attack
            Debug.Log("spike");
            break;
            case 2:
            //lazer attack
            Debug.Log("lazer");
            break;
        }

    }

    private void DecideAttackSoul(){
        switch(currentAttack){
            case 1:
            //silhouette attack 1
            Debug.Log("silhouette 1");
            break;
            case 2:
            //silhouette attack 2
            Debug.Log("silhouette 2");
            break;
        }
    }

    private void SetRandomIdleTime(){
        idleTime = Random.Range(bossData.minIdleTime, bossData.maxIdleTime);
    }
}
