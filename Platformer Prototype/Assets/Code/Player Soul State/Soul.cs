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
    public bool isInvincible;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hurtColour;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine("HitFlash");
        source.GenerateImpulse();
        levelManager.DecreasePlayerHealth(damageDetails);
    }
    private IEnumerator HitFlash(){
        spriteRenderer.color = hurtColour;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
