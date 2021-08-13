using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Soul : MonoBehaviour
{
    private Vector2 MovementInput;
    private int xRaw;
    private int yRaw;
    private PlayerInput playerInput;
    [SerializeField] private float velocity;
    public float playerHealth;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}