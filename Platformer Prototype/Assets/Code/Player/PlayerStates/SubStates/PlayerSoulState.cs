using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulState : PlayerGroundedState
{
    public PlayerSoulState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.gameObject.SetActive(false);
        GameObject.Instantiate(playerData.soulParticle, player.transform.position, player.transform.rotation);
        //emit audio q
    }

}
