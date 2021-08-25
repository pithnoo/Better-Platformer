using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLazer : MonoBehaviour
{
    private StraightMovingObject straightMovingObject;
    [SerializeField] private Animator animator;
    void OnEnable() {
        straightMovingObject = GetComponent<StraightMovingObject>();
        animator.SetBool("LazerFirst", true);
    }

    void Update() {
        if(straightMovingObject.reachedPoint){
            gameObject.SetActive(false);
        }
    }
}
