using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Portal : MonoBehaviour
{
    #region variables
    [Header("Entities")]
    public Player player;
    public Soul soul;
    private LevelManager levelManager;
    public GameObject portalParticle;
    public GameObject soulToSpawn;
    public GameObject playerToSpawn;

    [Header("Checks")]
    public float detectionRadius;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsSoul;
    public bool isWithPlayer;
    public bool isWithSoul;

    [Header("Current Portal State")]
    public PortalType state;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera virtualCamera;
    #endregion

    public enum PortalType{
        PLAYER,
        SOUL
    }


    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        
        // switch(state){
        //     case PortalType.PLAYER:
        //         canSpawnSoul = true;
        //         break;
        //     case PortalType.SOUL:
        //         canSpawnPlayer = true;
        //         break;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case PortalType.PLAYER:
                spawnSoul();
                break;
            case PortalType.SOUL:
                spawnPlayer();
                break;
            default:
                spawnSoul();
                break;
        }
    }

    public void spawnSoul(){
        isWithPlayer = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsPlayer);

        if(isWithPlayer && levelManager.player.isDashing){
            levelManager.player.isDashing = false;
            Destroy(levelManager.player.gameObject);

            Instantiate(portalParticle, transform.position, transform.rotation);
            Instantiate(soulToSpawn, transform.position, transform.rotation);

            levelManager.ResetSoulCheck();
            virtualCamera.m_Follow = levelManager.soul.transform;
        }
    }

    public void spawnPlayer(){
        isWithSoul = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsSoul);

        if(isWithSoul){
            Debug.Log("Active");
            Destroy(levelManager.soul.gameObject);

            Instantiate(portalParticle, transform.position, transform.rotation);
            Instantiate(playerToSpawn, transform.position, transform.rotation);

            levelManager.ResetPlayerCheck();
            virtualCamera.m_Follow = levelManager.player.transform;
            //canSpawnPlayer = false;
        }
    }
}
