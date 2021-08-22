using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private direction state;
    public float projectileSpeed;
    public float fireRate;
    [SerializeField] private string projectileName;
    private float nextTimeToFire;
    public enum direction{
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            ShootProjectile(projectileName);
        }
    }

    void ShootProjectile(string projectileString){
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectileString);
        if(projectile != null){
            projectile.transform.position = transform.position;
            //projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);
        }

        switch (state)
        {
            case direction.UP:
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
                break;
            case direction.DOWN:
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileSpeed, 0f);
                break;
            case direction.LEFT:
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(-projectileSpeed, 0f, 0f);
                break;
            case direction.RIGHT:
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f, 0f);
                break;
        }
    }
}
