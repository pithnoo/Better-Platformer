using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InActiveOverTime : MonoBehaviour
{
    private float lifeTime;
    public float lifeTimeSet;
    // Update is called once per frame
    void OnEnable() {
        lifeTime = lifeTimeSet;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f){
            gameObject.SetActive(false);
        }
    }
}
