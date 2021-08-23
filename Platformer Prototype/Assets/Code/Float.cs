using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float velocity;
    public float height;
    private float yValue;
    private Vector2 startPoint;

    void FixedUpdate() {
        startPoint = transform.position;
        yValue = Mathf.Sin(Time.time * velocity);
        transform.position = startPoint + (new Vector2(0f, yValue) * height);
    }
}
