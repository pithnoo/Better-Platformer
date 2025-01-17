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
    public Vector3 respawnPosition;
    public GameObject respawnParticle;
    private PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = FindObjectOfType<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputHandler = GetComponent<PlayerInputHandler>();
        isInvincible = false;
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = inputHandler.RawMovementInput.normalized * velocity;
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

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Checkpoint"){
            respawnPosition = other.transform.position;
        }
    }
}
