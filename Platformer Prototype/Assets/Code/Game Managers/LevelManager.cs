using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public Soul soul;
    public currentPlayerState state;    
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth; //to be changed to only private later

    public enum currentPlayerState{
        PLAYER,
        SOUL
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        soul = FindObjectOfType<Soul>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerCheck() => player = FindObjectOfType<Player>();
    public void ResetSoulCheck() => soul = FindObjectOfType<Soul>();
    public void DecreasePlayerHealth(DamageDetails damageDetails){
        currentHealth -= damageDetails.damageAmount;
        
        if(currentHealth <= 0){
            switch(state){
                case currentPlayerState.PLAYER:
                    Destroy(player.gameObject);
                    break;
                case currentPlayerState.SOUL:
                    Destroy(soul.gameObject);
                    break;
            }
        }
    }
}
