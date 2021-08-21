using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private StraightMovingObject straightMovingObject;
    private Boss boss;
    [SerializeField] private float secondSpeed;
    [SerializeField] private Transform lazer1, lazer2;
    private Animator anim;

    private enum lazerState{
        FIRST,
        SECOND,
        THIRD
    }

    [SerializeField] private lazerState state;

    void OnEnable() {
        boss = FindObjectOfType<Boss>();
        anim = transform.Find("Lazer").GetComponent<Animator>();

        if(boss.secondPhase){
            straightMovingObject.moveSpeed = secondSpeed;
            anim.SetBool("LazerFirst", false);
            anim.SetBool("LazerSecond", true);
        }
        else{
            anim.SetBool("LazerFirst", true);
            anim.SetBool("LazerSecond", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        straightMovingObject = GetComponent<StraightMovingObject>();
        lazer1 = boss.lazer1;
        lazer2 = boss.lazer2;
    }

    // Update is called once per frame
    void Update()
    {
        if(straightMovingObject.reachedPoint){
            switch(state){
                case lazerState.FIRST:
                    SpawnLazer("ReverseLazer1", lazer2);
                    break;
                case lazerState.SECOND:
                    SpawnLazer("Lazer2", lazer1);
                    SpawnLazer("ReverseLazer2", lazer2);
                    break;
                default:
                    break;
            }
            gameObject.SetActive(false);
        }
    }

    private void SpawnLazer(string projectileName, Transform attackPosition){
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectileName);
        if(projectile != null){
            projectile.transform.position = attackPosition.position;
            projectile.transform.rotation = attackPosition.rotation;
            projectile.SetActive(true);
        }
    }
}
