using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovingObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 currentTarget;

    public bool canMove;
    public float delay;
    public bool reachedPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = endPoint.position;
        canMove = false;
    }

    void Update() {
        delay -= Time.deltaTime;
        if(delay <= 0f){
            canMove = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove){
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.fixedDeltaTime);

            if (objectToMove.transform.position == endPoint.position)
            {
                currentTarget = startPoint.position;
                reachedPoint = true;
            }
            if (objectToMove.transform.position == startPoint.position)
            {
                currentTarget = endPoint.position;
            }
        }
    }
}
