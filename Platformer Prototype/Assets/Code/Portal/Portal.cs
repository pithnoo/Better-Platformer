using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Portal : MonoBehaviour
{
    #region variables
    [Header("Entities")]
    private LevelManager levelManager;
    [SerializeField] private GameObject portalParticle, soulToSpawn, playerToSpawn;

    [Header("Checks")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask whatIsPlayer, whatIsSoul;
    [SerializeField] private bool isWithPlayer, isWithSoul;

    [Header("Current Portal State")]
    [SerializeField] private PortalType state;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineImpulseSource source;
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
            levelManager.state = LevelManager.currentPlayerState.SOUL;
        }
    }

    public void spawnPlayer(){
        isWithSoul = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsSoul);

        if(isWithSoul){
            //source.GenerateImpulse();
            Destroy(levelManager.soul.gameObject);

            Instantiate(portalParticle, transform.position, transform.rotation);
            Instantiate(playerToSpawn, transform.position, transform.rotation);

            levelManager.ResetPlayerCheck();
            virtualCamera.m_Follow = levelManager.player.transform;
            levelManager.state = LevelManager.currentPlayerState.PLAYER;
            //canSpawnPlayer = false;
        }
    }
}
