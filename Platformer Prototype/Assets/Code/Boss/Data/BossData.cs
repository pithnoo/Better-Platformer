using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newBossData", menuName = "Data/Boss Data/Base Data")]
public class BossData : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public int maxHealth = 8;
    public int collisionDamage;
    public float xScaleFlight;
    public float yScaleFlight;
    public float flightSpeed;
    public GameObject playerToSpawn, soulToSpawn, portalParticle;
}
