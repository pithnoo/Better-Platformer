using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public int maxHealth = 8;
    public int collisionDamage;
    public float xScaleFlight;
    public float yScaleFlight;
}
