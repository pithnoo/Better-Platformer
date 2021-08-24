using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public Soul soul;
    public currentPlayerState state;    
    public bool isInvincible;
    public float invincibleTimer;
    public int maxHealth;
    public int currentHealth; 
    public float respawnTime;
    [SerializeField] private GameObject deathParticle;
    private ResetOnRespawn[] objectsToReset;

    public enum currentPlayerState{
        PLAYER,
        SOUL
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        soul = FindObjectOfType<Soul>();
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerCheck() => player = FindObjectOfType<Player>();
    public void ResetSoulCheck() => soul = FindObjectOfType<Soul>();
    public void DecreasePlayerHealth(DamageDetails damageDetails){
        if(!isInvincible){
            currentHealth -= damageDetails.damageAmount;
            StartCoroutine("InvincibleTimer");
            if (currentHealth <= 0)
            {
                switch (state)
                {
                    case currentPlayerState.PLAYER:
                        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
                        Destroy(player.gameObject);
                        break;
                    case currentPlayerState.SOUL:
                        Instantiate(deathParticle, soul.transform.position, soul.transform.rotation);
                        Destroy(soul.gameObject);
                        break;
                }
            }
        }
    }

    private IEnumerator InvincibleTimer(){
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTimer);
        isInvincible = false;
    }

    public void healthRestore(int healthToRestore){
        if(currentHealth < maxHealth){
            currentHealth++;
        }
    }

    public void fullRestore(){
        if(currentHealth < maxHealth){
            currentHealth = maxHealth;
        }
    }

    public IEnumerator Respawn(){
        yield return new WaitForSeconds(respawnTime);

        //respawn player
        switch(state){
            case currentPlayerState.PLAYER:
                player.transform.position = player.respawnPosition;
                player.gameObject.SetActive(true);
                break;
            case currentPlayerState.SOUL:
                soul.transform.position = soul.respawnPosition;
                soul.gameObject.SetActive(true);
                break;
        }

        if(objectsToReset.Length > 0){
            for (int i = 0; i < objectsToReset.Length; i++)
            {
                objectsToReset[i].ResetObject();
                objectsToReset[i].gameObject.SetActive(true);
            }
        }
    }

}
