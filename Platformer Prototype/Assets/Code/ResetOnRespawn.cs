using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    void Start() {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ResetObject(){
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
