using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class HealthRestore : MonoBehaviour
{
    public bool fullRestore;
    private LevelManager levelManager;
    private HealthManager healthManager;
    public int healthToRestore;
    public CinemachineImpulseSource source;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(levelManager.currentHealth < levelManager.maxHealth){
                
                Instantiate(particle, transform.position, transform.rotation);
                if(!fullRestore){
                    FindObjectOfType<AudioManager>().Play("Health");
                    levelManager.healthRestore(healthToRestore);
                }
                else{
                    FindObjectOfType<AudioManager>().Play("Collectable");
                    PlayerPrefs.SetInt("Collectable" + (SceneManager.GetActiveScene().buildIndex-1).ToString(), 1);
                }
                healthManager.UpdateHealth(levelManager.currentHealth);
                gameObject.SetActive(false);
            }
        }
    }

   
}
