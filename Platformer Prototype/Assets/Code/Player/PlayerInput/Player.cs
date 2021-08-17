using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine {get; private set;}
    public PlayerIdleState IdleState {get; private set;}
    public PlayerMoveState MoveState {get; private set;}
    public PlayerJumpState JumpState {get; private set;}
    public PlayerInAirState InAirState {get; private set;}
    public PlayerLandState LandState {get; private set;}
    public PlayerWallSlideState WallSlideState {get; private set;}
    public PlayerWallGrabState WallGrabState {get; private set;}
    public PlayerWallClimbState WallClimbState {get; private set;}
    public PlayerWallJumpState WallJumpState {get; private set;}
    public PlayerDashState DashState {get; private set;}
    public PlayerSoulState SoulState {get; private set;}
    [SerializeField]
    private PlayerData playerData;
    #endregion
    
    #region Components
    
    public Animator Anim {get; private set;}
    public PlayerInputHandler InputHandler {get; private set;}
    public Rigidbody2D RB {get; private set;}
    public CinemachineImpulseSource source {get; private set;}
    #endregion
    
    #region Check Transforms
    [SerializeField]
    private Transform GroundCheck;
    [SerializeField]
    private Transform WallCheck;

    #endregion
    
    #region Other Variables
    public Vector2 CurrentVelocity {get; private set;}
    public int FacingDirection {get; private set;}
    private Vector2 workspace;
    public bool isDashing;
    public bool isInvincible;
    private bool onPlatform;
    private LevelManager levelManager;
    [SerializeField] private Color hurtColour;
    private SpriteRenderer spriteRenderer;
    
    #endregion
    
    #region Unity Callback Functions
    private void Awake() {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        SoulState = new PlayerSoulState(this, StateMachine, playerData, "soul");
    }

    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        source = GetComponent<CinemachineImpulseSource>();
        levelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StateMachine.Initialise(IdleState);

        FacingDirection = 1;
        onPlatform = false;
    }

    private void Update() {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();    
    }
    #endregion
    
    #region Set Functions
    public void SetVelocity(float velocity, Vector2 angle, int direction){
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetDashVelocity(float velocity, Vector2 direction){
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float velocity){
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity){
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void TakeDamage(DamageDetails damageDetails){
        StartCoroutine("HitFlash");
        source.GenerateImpulse();
        levelManager.DecreasePlayerHealth(damageDetails);
    }
    #endregion
    
    #region Check Functions
    public bool CheckIfGrounded(){
        return Physics2D.OverlapCircle(GroundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckIfShouldFlip(int xInput){
        if(xInput != 0 && xInput != FacingDirection){
            Flip();
        }
    }

    public bool CheckIfTouchingWall(){
        return Physics2D.Raycast(WallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack(){
        return Physics2D.Raycast(WallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    #endregion
    
    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip(){
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public void DashShake() => source.GenerateImpulse();

    public void Damage(DamageDetails damageDetails){
        //take damage
    }

    public void Dashing() => isDashing = true;
    public void NotDashing() => isDashing = false;

    public void PlatformCheck(){
        if(onPlatform){
            //invoke multiplier
        }
        else{
            //undo multiplier
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "MovingPlatform"){
            transform.parent = other.transform;
            onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "MovingPlatform"){
            transform.parent = null;
            onPlatform = false;
        }
    }

    private IEnumerator HitFlash(){
        spriteRenderer.color = hurtColour;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
    #endregion
}
