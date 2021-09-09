using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeadZone : MonoBehaviour
{
    private LevelManager levelManager;
    [SerializeField] private CinemachineImpulseSource source;
    [SerializeField] private int damage;
    [SerializeField] private GameObject deathParticle;
    private HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Instantiate(deathParticle, other.transform.position, other.transform.rotation);

            levelManager.currentHealth -= damage;
            healthManager.UpdateHealth(levelManager.currentHealth);

            other.gameObject.SetActive(false);
            
            source.GenerateImpulse();
            levelManager.StartCoroutine("Respawn");
        }
    }
}
