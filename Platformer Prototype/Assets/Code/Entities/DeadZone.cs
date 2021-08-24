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
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Instantiate(deathParticle, other.transform.position, other.transform.rotation);
            levelManager.currentHealth -= damage;
            other.gameObject.SetActive(false);
            source.GenerateImpulse();
            levelManager.StartCoroutine("Respawn");
        }
    }
}
