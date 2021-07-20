using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;


    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;


    private Rigidbody2D rb;
    private bool falling; 

    private float xRaw;
    private float yRaw;
    public Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        dir = new Vector2(xRaw,yRaw).normalized;

        if(Input.GetButtonDown("Jump")){
            //perform jump
        }

        if(xRaw > 0f){
            transform.localScale = new Vector2(1,1);
        }
        else{
            transform.localScale = new Vector2(-1,1);
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
    }
}
