using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Player player;
    private LevelManager levelManager;
    private bool pointSet;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        pointSet = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!pointSet){
                Debug.Log("Active");
                if(levelManager.state == LevelManager.currentPlayerState.PLAYER){
                    FindObjectOfType<Player>().respawnPosition = transform.position;
                }
                else{
                    FindObjectOfType<Soul>().respawnPosition = transform.position;
                }
                
                pointSet = true;
            }
        }
    }
}
