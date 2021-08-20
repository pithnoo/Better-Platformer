using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    private StraightMovingObject straightMovingObject;
    // Start is called before the first frame update
    void Start()
    {
        straightMovingObject = GetComponent<StraightMovingObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(straightMovingObject.reachedPoint){
            gameObject.SetActive(false);
        }
    }
}
