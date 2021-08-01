using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround; 
}