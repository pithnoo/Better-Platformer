using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineImpulseSource source;
    private LevelLoader levelLoader;


    #endregion

    public enum PortalType{
        PLAYER,
        SOUL,
        LEVEL
    }


    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        levelLoader = FindObjectOfType<LevelLoader>();

        
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
            case PortalType.LEVEL:
                nextLevel();
                break;
            default:
                spawnSoul();
                break;
        }
    }

    public void spawnSoul(){
        isWithPlayer = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsPlayer);

        if(isWithPlayer && levelManager.player.isDashing){
            FindObjectOfType<AudioManager>().Play("PortalWarp");
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
            FindObjectOfType<AudioManager>().Play("PortalWarp");
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

    public void nextLevel(){
        isWithSoul = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsSoul);
        isWithPlayer = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsPlayer);
        if (isWithPlayer || isWithSoul)
        {
            FindObjectOfType<AudioManager>().Play("PortalWarp");
            StartCoroutine("levelLoad");
        }
    }
    
    private IEnumerator levelLoad(){
        source.GenerateImpulse();
        Instantiate(portalParticle, transform.position, transform.rotation);
        if(isWithPlayer){
            Destroy(levelManager.player.gameObject);
        }
        else{
            Destroy(levelManager.soul.gameObject);
        }


        if (!PlayerPrefs.HasKey("Collectable0"))
        {
            if(SceneManager.GetActiveScene().buildIndex == 1){
                PlayerPrefs.SetInt("Collectable0", 1);
                Debug.Log("Active");
            }
        }
        yield return new WaitForSeconds(3);
        levelLoader.loadNextLevel();
    }
}
