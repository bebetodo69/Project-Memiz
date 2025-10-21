using System;
using System.Reflection.Emit;
using UnityEngine;
using Unity.Collections;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDist;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int totaljump;
    [SerializeField] private Animator anim;

    private int jumpLes;
    private bool canjump;
    private bool isGroundCheck;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float inputDirection;
    private bool isDirectionRight = true;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpLes = totaljump;
    }

    // Update is called once per frame
    void Update()
    {
       GetInputMove();
       DirectionCheck();
       Canjump();
       MoveAnim();
       jumpAnim();
    }

    private void FixedUpdate()
    {
        MoveLogic();
        CheckArea();
    }

    void Canjump()
    {
        if (isGroundCheck && rb2d.linearVelocity.y <= 0)
        {
            jumpLes = totaljump;
        }

        if (jumpLes <= 0)
        {
            canjump = false;
        }

        else
        {
            canjump = true;
        }
    }

    void CheckArea()
    {
        isGroundCheck = Physics2D.OverlapCircle(groundCheck.position, groundDist,groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDist);
    }

    void DirectionCheck()
    {
        if (isDirectionRight && inputDirection < 0)
        {
            Flip();
        }
        else if (!isDirectionRight && inputDirection > 0)
        {
            Flip();
        }
    }
    
    void GetInputMove()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump"))
        {
            jump();
        }
    }

    void MoveLogic()
    {
        rb2d.linearVelocity = new Vector2(inputDirection * moveSpeed, rb2d.linearVelocity.y);
    }

    void MoveAnim()
    {
        anim.SetFloat("HorizontalAnim", rb2d.linearVelocity.x);
    }
    void jump()
    {
        if (canjump)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            jumpLes--;
        }
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
    }

    void jumpAnim()
    {
        anim.SetFloat("VerticalAnim", rb2d.linearVelocity.y);
        anim.SetBool("groundCheck", isGroundCheck);
    }

    void Flip()
    {
        isDirectionRight = !isDirectionRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
