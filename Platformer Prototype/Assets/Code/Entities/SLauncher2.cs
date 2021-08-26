using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLauncher2 : MonoBehaviour
{
    public bool shootFast;
    private ProjectileLauncher projectileLauncher;
    [SerializeField] private float velocity1, velocity2;
    [SerializeField] private float fireRate1, fireRate2;

    void OnEnable() {
        projectileLauncher = GetComponent<ProjectileLauncher>();

        if(shootFast){
            projectileLauncher.projectileSpeed = velocity2;
            projectileLauncher.fireRate = fireRate2;
        }
        else{
            projectileLauncher.projectileSpeed = velocity1;
            projectileLauncher.fireRate = fireRate1;
        }
    }
}
