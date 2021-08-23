using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : MonoBehaviour
{
    public bool fullRestore;
    private LevelManager levelManager;
    public int healthToRestore;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(levelManager.currentHealth < levelManager.maxHealth){
                if(fullRestore){
                    levelManager.healthRestore(healthToRestore);
                }
                else{
                    levelManager.fullRestore();
                }
                gameObject.SetActive(false);
            }
        }
    }

   
}
