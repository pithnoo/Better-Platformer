using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDistance : MonoBehaviour
{
    private Player player;
    private LevelManager levelManager;
    private Soul soul;
    public AudioSource audioSource;
    public float minDist=1;
    public float maxDist=400;
    private float dist;
    private bool foundPlayer = false;
    private bool foundSoul = false;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(levelManager.state == LevelManager.currentPlayerState.PLAYER){
            foundSoul = false;
            if(!foundPlayer){
                player = FindObjectOfType<Player>();
                foundPlayer = true;
            }
            dist = Vector3.Distance(transform.position, player.transform.position);
        }
        else{
            foundPlayer = false;
            if(!foundSoul){
                soul = FindObjectOfType<Soul>();
                foundSoul = true;
            }
            dist = Vector3.Distance(transform.position, soul.transform.position);
        }
 
        if(dist < minDist)
        {
            audioSource.volume = 0.2f;
        }
        else if(dist > maxDist)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 0.2f - ((dist - minDist) / (maxDist - minDist));
        }
    }
}
