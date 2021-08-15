using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float Radius;

    [SerializeField] private GameObject target;
    private Vector2 centre;
    private float angle;
    [Range(-3.14f, 3.14f)] public float AngleRange;

    void Start()
    {
        angle = AngleRange;
    }

    void FixedUpdate()
    {
        centre = target.transform.position;
        angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = centre + offset;
    }
  
}
