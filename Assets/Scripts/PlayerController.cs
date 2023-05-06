using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask terrainLayerMask;

    private bool isRunning;
    private bool isGrounded;

    private int xInput = 0;
    private int facingDirection = 1;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();
        Debug.Log(isGrounded);

        Move();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (!isGrounded)
        {
            if (rb.velocity.y > 0f)
            {
                animator.SetBool("IsJumping", true);
            }
            else if (rb.velocity.y < 0f)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        
    }

    private void Move()
    {
        // Check for input to determine movement
        float moveX = Input.GetAxis("Horizontal");

        xInput = (int)Mathf.Sign(moveX);

        // Update the player's velocity based on input
        if (moveX != 0)
        {
            isRunning = true;
            rb.velocity = new Vector2(moveX * runSpeed, rb.velocity.y);
        }
        else
        {
            isRunning = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Update the animator parameter
        animator.SetBool("IsRunning", isRunning);

        // Check if player should flip
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, groundCheckDistance, terrainLayerMask);
    }

    private void Flip()
    {
        facingDirection *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void OnDrawGizmos()
    {
        if (col == null) return;
        Gizmos.DrawWireCube(col.bounds.center, col.bounds.size + new Vector3(0f, groundCheckDistance, 0f)); 
    }
}
