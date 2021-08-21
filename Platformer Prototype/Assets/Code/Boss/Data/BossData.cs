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
    public GameObject spikeParticle, silhouetteParticleHorizontal, silhouetteParticleVertical, diskParticle, lazerParticle;

    [Header("Burst Attack")]
    public int numberOfProjectiles = 8;
    public float burstRadius = 0.25f;
    public float burstProjectileSpeed = 3f;
}
