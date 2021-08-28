using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrail : MonoBehaviour
{
    public enum lookState{
        PLAYER,
        SOUL
    }
    [SerializeField] private lookState state;
    private Player player;
    private Soul soul;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void OnEnable()
    {
        player = FindObjectOfType<Player>();
        soul = FindObjectOfType<Soul>();

        if(player == null){
            state = lookState.SOUL;
        }
        else{
            state = lookState.PLAYER;
        }
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardsEntity();
    }

    public void LookTowardsEntity(){
        switch(state){
            case lookState.PLAYER:
                LookTowardsPlayer();
                break;
            case lookState.SOUL:
                LookTowardsSoul();
                break;
        }
    }

    private void LookTowardsPlayer(){
        //Debug.Log(player.transform.position.x);
        if(player == null){
            return;
        }
        else if(player.transform.position.x > transform.position.x){
            transform.localScale = new Vector3(1,1,1);
        }
        else if(player.transform.position.x < transform.position.x){
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private void LookTowardsSoul(){
        if(soul == null){
            return;
        }
        else if(soul.transform.position.x > transform.position.x){
            transform.localScale = new Vector3(1,1,1);
        }
        else if(soul.transform.position.x < transform.position.x){
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    public void ResetPlayerCheck() => player = FindObjectOfType<Player>();
    public void ResetSoulCheck() => soul = FindObjectOfType<Soul>();
    
}
