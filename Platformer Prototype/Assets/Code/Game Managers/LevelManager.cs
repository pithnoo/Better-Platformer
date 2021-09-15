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
    private HealthManager healthManager;
    public bool isGameOver;
    private LevelLoader levelLoader;

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
        healthManager = FindObjectOfType<HealthManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
        currentHealth = maxHealth;
        isGameOver = false;
    }


    public void ResetPlayerCheck() => player = FindObjectOfType<Player>();
    public void ResetSoulCheck() => soul = FindObjectOfType<Soul>();
    public void DecreasePlayerHealth(DamageDetails damageDetails){
        if(!isInvincible){
            currentHealth -= damageDetails.damageAmount;
            healthManager.UpdateHealth(currentHealth);
            StartCoroutine("InvincibleTimer");
            if (currentHealth <= 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerReset");
                //FindObjectOfType<AudioManager>().stopPlaying("LevelTheme");
                isGameOver = true;
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
                StartCoroutine("gameOver");
            }
            else{
                FindObjectOfType<AudioManager>().Play("Hurt");
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
        currentHealth = maxHealth;
    }

    private IEnumerator gameOver(){
        Cursor.visible = true;
        yield return new WaitForSeconds(respawnTime);
        levelLoader.loadMenu();
    }

    public IEnumerator Respawn(){
        yield return new WaitForSeconds(respawnTime);

        //respawn player
        switch(state){
            case currentPlayerState.PLAYER:
                player.transform.position = new Vector2(player.respawnPosition.x, player.respawnPosition.y + 4);
                player.respawnParticle.SetActive(true);
                player.gameObject.SetActive(true);
                break;
            case currentPlayerState.SOUL:
                soul.transform.position = new Vector2(soul.respawnPosition.x, soul.respawnPosition.y);
                soul.respawnParticle.SetActive(true);
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
