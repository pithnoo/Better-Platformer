using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator animator;
    private bool bossCamera = false;
    void Start() {
        animator = GetComponent<Animator>();
    }

    public void SwitchState(){
        if(!bossCamera){
            animator.Play("Boss");
            bossCamera = true;
        }
        else{
            animator.Play("Main");
            bossCamera = false;
        }
    }
}
