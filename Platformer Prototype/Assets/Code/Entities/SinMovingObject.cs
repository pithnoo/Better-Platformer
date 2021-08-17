using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovingObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 startPoint;
    public movementMode state;

    public bool canMove;

    public enum movementMode{
        MY,
        MX
    }

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove){
            switch(state){
                case movementMode.MX:
                    transform.position = startPoint + new Vector3(Mathf.Sin(Time.time) * moveSpeed * Time.fixedDeltaTime, 0f, 0f);
                    //transform.position = startPoint + (Vector3.right * Mathf.Sin(Time.time/2*moveSpeed)*20 - Vector3.up * Mathf.Sin(Time.time * moveSpeed)*5);
                    break;
                case movementMode.MY:
                    transform.position = startPoint + new Vector3(0f, Mathf.Sin(Time.time) * moveSpeed * Time.fixedDeltaTime, 0f);
                    break;
            }
        }
    }
}
