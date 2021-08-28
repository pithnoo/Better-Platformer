using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    private int currentAttack;
    private bool isIdleTimeOver;
    private int idleTime, maxIdleTime, minIdleTime;
    private float flightSpeed;
    private int numAttacks;
    private int maxAttacks;
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
        boss.bossTrail.gameObject.SetActive(true);

        if(boss.currentHealth <= (bossData.maxHealth / 2)){
            boss.secondPhase = true;

            minIdleTime = 1;
            maxIdleTime = 2;
            flightSpeed = 5;
        }
        else{
            maxIdleTime = bossData.maxIdleTime;
            minIdleTime = bossData.minIdleTime;
            maxAttacks = bossData.maxAttacks;
            flightSpeed = bossData.flightSpeed;
        }


        SetRandomIdleTime();
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

        if(boss.currentHealth <= (boss.currentHealth / 2)){
            boss.secondPhase = true;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        boss.transform.position = boss.originalPoint.position + (Vector3.right * Mathf.Sin((Time.time - startTime)/2*flightSpeed)*bossData.xScaleFlight - Vector3.up * Mathf.Sin((Time.time - startTime) * flightSpeed)*bossData.yScaleFlight);
        boss.LookTowardsEntity();
    }

    private void AttackPlayer(){
        if(numAttacks < maxAttacks){
            boss.bossVanish();
            currentAttack = Random.Range(1, 4);
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
            stateMachine.ChangeState(boss.burstAttack);
        }
    }

    private void DecideAttackPlayer(){
        switch (currentAttack)
        {
            case 1:
                //spike attack
                stateMachine.ChangeState(boss.spikeAttack);
                GameObject.Instantiate(bossData.spikeParticle, boss.transform.position, boss.transform.rotation);
                break;
            case 2:
                //lazer attack
                stateMachine.ChangeState(boss.lazerAttack);
                GameObject.Instantiate(bossData.lazerParticle, boss.transform.position, boss.transform.rotation);
                break;
            case 3:
                //disk attack
                stateMachine.ChangeState(boss.diskAttack);
                GameObject.Instantiate(bossData.diskParticle, boss.transform.position, boss.transform.rotation);
                break;
        }

    }

    private void DecideAttackSoul(){
        switch (currentAttack)
        {
            case 1:
                //silhouette attack 1
                stateMachine.ChangeState(boss.silhouetteAttackHorizontal);
                GameObject.Instantiate(bossData.silhouetteParticleHorizontal, boss.transform.position, boss.transform.rotation);
                break;
            case 2:
                //silhouette attack 2
                stateMachine.ChangeState(boss.silhouetteAttackVertical);
                GameObject.Instantiate(bossData.silhouetteParticleVertical, boss.transform.position, boss.transform.rotation);
                break;
            case 3:
                //spike attack
                stateMachine.ChangeState(boss.spikeAttack);
                GameObject.Instantiate(bossData.spikeParticle, boss.transform.position, boss.transform.rotation);
                break;
        }
    }

    private void SetRandomIdleTime(){
        idleTime = Random.Range(minIdleTime, maxIdleTime);
    }

    public void ResetNumAttacks() => numAttacks = 0;
}
