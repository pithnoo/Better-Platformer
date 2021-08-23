using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Boss : MonoBehaviour
{
    #region Variables
    public BossStateMachine stateMachine {get; private set;}
    [SerializeField] private BossData bossData;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public BossTrail bossTrail {get; private set;}
    public float currentHealth;
    protected bool isDead;
    private LevelManager levelManager;
    private Player player;
    private Soul soul;
    private Portal portal;
    public bool bossInvincible;
    private DamageDetails damageDetails;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform attackPosition;
    public Transform originalPoint;
    public Transform escapePoint;
    private bool isWithPlayer, isWithSoul;
    public CinemachineVirtualCamera virtualCamera;
    private SpriteRenderer SR;
    public bool secondPhase;
    public GameObject bossVunerable;

    public enum bossAttackType{
        SOUL,
        PLAYER
    }
    public bossAttackType attackType;
    #endregion

    #region States
    public enum bossCollisionState{
        SOUL,
        PLAYER
    }

    public bossCollisionState collisionState;
    public BossIdleState bossIdleState {get; private set;}
    public BossEntranceState bossEntranceState {get; private set;}
    public BossDeadState bossDeadState {get; private set;}
    public BurstAttack burstAttack {get; private set;}
    public DiskAttack diskAttack {get; private set;}
    public LazerAttack lazerAttack {get; private set;}
    public SpikeAttack spikeAttack {get; private set;}
    public SilhouetteAttackHorizontal silhouetteAttackHorizontal {get; private set;}
    public SilhouetteAttackVertical silhouetteAttackVertical {get; private set;}


    #endregion

    #region Attack Positions
    [Header("Disk attack position")]
    public Transform diskPosition;
    [Header("Spike attack positions")]
    public List<Transform> topSpikePositions;
    public List<Transform> bottomSpikePositions;

    [Header("Lazer attack positions")]
    public Transform lazer1, lazer2;

    [Header("Silhouette positions")]
    public Transform vertical1;
    public Transform vertical2;
    public Transform horizontal1, horizontal2;
    #endregion

    #region Unity Callback Functions
    private void Awake() {
        //bossData = Resources.Load<BossData>("Assets/Code/Boss/Data/BossPhase1");

        stateMachine = new BossStateMachine();
        bossIdleState = new BossIdleState(this, stateMachine, bossData, "idle");
        bossEntranceState = new BossEntranceState(this, stateMachine, bossData, "entrance", attackPosition);
        bossDeadState = new BossDeadState(this, stateMachine, bossData, "dead", attackPosition);
        burstAttack = new BurstAttack(this, stateMachine, bossData, "burst", attackPosition);
        diskAttack = new DiskAttack(this, stateMachine, bossData, "disk", attackPosition);
        lazerAttack = new LazerAttack(this, stateMachine, bossData, "lazer", attackPosition);
        spikeAttack = new SpikeAttack(this, stateMachine, bossData, "spike", attackPosition);
        silhouetteAttackHorizontal = new SilhouetteAttackHorizontal(this, stateMachine, bossData, "sHorizontal", attackPosition);
        silhouetteAttackVertical = new SilhouetteAttackVertical(this, stateMachine, bossData, "sVertical", attackPosition);
    }

    private void Start(){
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        portal = aliveGO.GetComponent<Portal>();
        SR = aliveGO.GetComponent<SpriteRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();
        bossTrail = FindObjectOfType<BossTrail>();

        bossTrail.gameObject.SetActive(false);
        currentHealth = bossData.maxHealth;
        facingDirection = 1;
        bossInvincible = true;
        secondPhase = false;

        attackType = bossAttackType.PLAYER;

        stateMachine.Initialise(bossEntranceState);
        //stateMachine.Initialise(bossIdleState);
    }

    private void Update(){
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate(){
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion
    
    #region Transform Functions
    
    public void flip(){
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f,180f,0f);
    }

    public void LookTowardsEntity(){
        switch(collisionState){
            case bossCollisionState.PLAYER:
                LookTowardsPlayer();
                break;
            case bossCollisionState.SOUL:
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

    public void ReturnToCentre() => transform.position = Vector2.MoveTowards(transform.position, originalPoint.position, bossData.flightSpeed);
    #endregion
    
    #region Other Functions
    public void SpawnGameObject(string entityTag, Transform spawnPosition){
        //Debug.Log(spawnPosition);
        GameObject item = ObjectPool.SharedInstance.GetPooledObject(entityTag);
        if (item != null)
        {
            item.transform.position = spawnPosition.position;
            item.transform.rotation = spawnPosition.rotation;
            item.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!bossInvincible){
                DecideSpawn();
                stateMachine.ChangeState(bossIdleState);
                bossIdleState.ResetNumAttacks();

                if(currentHealth == 0){
                    stateMachine.ChangeState(bossDeadState);
                }
                else{
                    currentHealth--;
                }
            }
        }    
    }

    private void DecideSpawn(){
        switch (collisionState)
        {
            case bossCollisionState.PLAYER:
                spawnSoul();
                attackType = bossAttackType.SOUL;
                bossTrail.ResetSoulCheck();
                break;
            case bossCollisionState.SOUL:
                spawnPlayer();
                bossTrail.ResetPlayerCheck();
                attackType = bossAttackType.PLAYER;
                break;
        }
    }

    private void spawnSoul(){
        if(levelManager.player.isDashing){
            //levelManager.currentHealth++;
            levelManager.player.isDashing = false;
            Destroy(levelManager.player.gameObject);

            Instantiate(bossData.portalParticle, transform.position, transform.rotation);
            Instantiate(bossData.soulToSpawn, transform.position, transform.rotation);

            levelManager.ResetSoulCheck();
            ResetSoulCheck();
            virtualCamera.m_Follow = levelManager.soul.transform;
            levelManager.state = LevelManager.currentPlayerState.SOUL;

            bossInvincible = true;
            collisionState = bossCollisionState.SOUL;
        }
    }

    private void spawnPlayer(){
        //source.GenerateImpulse();
        //levelManager.currentHealth++;
        Destroy(levelManager.soul.gameObject);

        Instantiate(bossData.portalParticle, transform.position, transform.rotation);
        Instantiate(bossData.playerToSpawn, transform.position, transform.rotation);

        levelManager.ResetPlayerCheck();
        ResetPlayerCheck();
        virtualCamera.m_Follow = levelManager.player.transform;
        levelManager.state = LevelManager.currentPlayerState.PLAYER;
        //canSpawnPlayer = false;

        bossInvincible = true;
        collisionState = bossCollisionState.PLAYER;
    }

    private void ResetPlayerCheck() => player = FindObjectOfType<Player>();
    private void ResetSoulCheck() => soul = FindObjectOfType<Soul>();
    public void bossVanish() => SR.enabled = false;
    public void bossReturn() {
        transform.position = originalPoint.position; 
        GameObject.Instantiate(bossData.bossReturnParticle, transform.position, transform.rotation);
        SR.enabled = true;
    }
    #endregion
}
