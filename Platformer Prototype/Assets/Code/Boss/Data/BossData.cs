using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newBossData", menuName = "Data/Boss Data/Base Data")]

public class BossData : ScriptableObject
{
    public LayerMask whatIsPlayer;

    [Header("Boss Stats")]
    public int maxHealth = 8;
    public int collisionDamage;

    [Header("Boss Idle State")]
    public int maxAttacks;
    public float xScaleFlight;
    public float yScaleFlight;
    public int minIdleTime, maxIdleTime;
    public float flightSpeed;

    [Header("Entities to spawn")]
    public GameObject playerToSpawn, soulToSpawn, portalParticle;
    public GameObject projectile;

    [Header("Burst Attack")]
    public int numberOfProjectiles = 8;
    public float burstRadius = 0.25f;
    public float burstProjectileSpeed = 3f;

    public List<Transform> topSpikePositions;
    public List<Transform> bottomSpikePositions;

    [Header("Lazer attack positions")]
    public Transform lazer1, lazer2;

    [Header("Silhouette positions")]
    public Transform vertical1;
    public Transform vertical2;
    public Transform horizontal1, horizontal2;
}
