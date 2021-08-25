using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerStill : MonoBehaviour
{
    [SerializeField] private Boss boss;
    private Animator anim;
    void OnEnable()
    {
        boss = FindObjectOfType<Boss>();
        anim = GetComponent<Animator>();

        if(boss != null){
            if (boss.secondPhase)
            {
                anim.SetBool("LazerFirst", false);
                anim.SetBool("LazerSecond", true);
            }
            else
            {
                anim.SetBool("LazerFirst", true);
                anim.SetBool("LazerSecond", false);
            }
        }
        else
        {
            anim.SetBool("LazerFirst", true);
            anim.SetBool("LazerSecond", false);
        }

    }
}
