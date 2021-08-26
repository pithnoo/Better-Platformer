using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newSoulData", menuName = "Data/Soul Data/BaseData")]
public class SoulData : ScriptableObject
{
    [Header("Move State")]
    public float moveSpeed;
}
