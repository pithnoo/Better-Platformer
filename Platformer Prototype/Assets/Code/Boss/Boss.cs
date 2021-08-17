using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region Variables
    public BossStateMachine stateMachine;
    public D_Entity entityData;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }
    public float currentHealth;
    protected bool isDead;
    private LevelManager levelManager;
    private Player player;
    private bool isInvincible;
    private DamageDetails damageDetails;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform originalPoint;
    #endregion

    #region States

    #endregion

    #region Unity Callback Functions
    private void Awake() {
        stateMachine = new BossStateMachine();
    }

    private void Start(){
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();

        currentHealth = entityData.maxHealth;
        facingDirection = 1;
        isInvincible = false;
    }

    private void Update(){
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate(){
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!isInvincible){
                //boss take damage
            }
            else{
                damageDetails.damageAmount = entityData.collisionDamage;
                other.transform.SendMessage("TakeDamage", damageDetails);
            }
        }    
    }

    public void ReturnToCentre() => transform.position = originalPoint.position;
    public void SpawnGameObject(string entityTag, Transform spawnPosition){
        GameObject item = ObjectPool.SharedInstance.GetPooledObject(entityTag);
        if (item != null)
        {
            item.transform.position = spawnPosition.position;
            item.transform.rotation = spawnPosition.rotation;
            item.SetActive(true);
        }
    }
}
