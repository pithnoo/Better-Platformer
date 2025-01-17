using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/BaseData")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public int amountOfJumps = 1;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround; 
    public float wallCheckDistance = 0.5f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.5f;
    
    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 12f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1,2);

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distanceBetweenAfterImages = 0.25f;
    [Header("Other Variables")]
    public int playerHealth;
    public GameObject dashParticle;
}
