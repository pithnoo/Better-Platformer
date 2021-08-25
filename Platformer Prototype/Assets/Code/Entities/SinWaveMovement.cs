using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveMovement : MonoBehaviour
{
    public float velocity;
    public float frequency;
    public float magnitude;
    private Vector3 axis;
    private Vector3 point;
    public enum direction{
        RIGHT,
        LEFT
    }
    public direction state;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(transform.parent != null){
            point = transform.parent.position;
        }
        else{
            point = transform.position;
        }

        
        if (state == direction.RIGHT)
        {
            axis = transform.right;
        }
        else
        {
            axis = -transform.right;
        }
    }

    void FixedUpdate() {
        point -= transform.up * Time.fixedDeltaTime * velocity;
        transform.position = point + axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
