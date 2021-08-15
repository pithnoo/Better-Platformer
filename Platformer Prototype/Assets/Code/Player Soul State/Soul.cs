using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class Soul : MonoBehaviour
{
    private Vector2 MovementInput;
    private int xRaw;
    private int yRaw;
    private PlayerInput playerInput;
    [SerializeField] private float velocity;
    public float playerHealth;
    private Rigidbody2D rb;
    private LevelManager levelManager;
    [SerializeField] private CinemachineImpulseSource source;
    private bool isInvincible;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = FindObjectOfType<LevelManager>();
        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        rb.velocity = MovementInput * velocity;
    }

    public void Move(InputAction.CallbackContext context){
        MovementInput = context.ReadValue<Vector2>().normalized;

        // xRaw = (int)(MovementInput * Vector2.right).normalized.x;
        // yRaw = (int)(MovementInput * Vector2.up).normalized.y;
    }

    public void TakeDamage(DamageDetails damageDetails){
        if(!isInvincible){
            source.GenerateImpulse();
            StartCoroutine("InvincibleTimer");
            levelManager.DecreasePlayerHealth(damageDetails);
        }
    }
    private IEnumerator InvincibleTimer(){
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
}
