using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerLauncher : MonoBehaviour
{
    [SerializeField] private string lazerTag;
    [SerializeField] private float fireRate;
    private float nextTimeToFire;
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            ShootProjectile(lazerTag);
        }
    }

    private void ShootProjectile(string projectileString){
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject(projectileString);
        if(projectile != null){
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);
        }
    }
}
