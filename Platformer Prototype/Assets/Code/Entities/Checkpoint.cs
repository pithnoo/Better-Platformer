using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Player player;
    private bool pointSet;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        pointSet = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!pointSet){
                player.respawnPosition = transform.position;
                pointSet = true;
            }
        }
    }
}
