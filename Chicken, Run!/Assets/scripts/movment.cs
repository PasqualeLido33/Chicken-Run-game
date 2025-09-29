using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System;

public class movment : MonoBehaviour
{
    float horizontal;
    float speed = 8f;
    float JumpPower = 16f;
    float dashingPower = 1000f;
    float dashingTime = 0.3f;

    bool isFacingRight = true;
    bool canDash = false;
    bool isDashing;
    bool canDoubleJump = false;
    bool canJump = false;

    public killPlayer Killplayer;
    Animator animator;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheckRight;
    [SerializeField] Transform wallCheckLeft;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] TrailRenderer trail;

 
    void Start()
    {
      animator = GetComponent<Animator>();
    }


    void Update()
    {
        print(canDash);
        if (Killplayer.Died == false)
        {
            animator.SetBool("isDead", false);
            horizontal = Input.GetAxisRaw("Horizontal");
            flip();

            //DASH OBTAINED
            if (isTuchingWallLeft() || isTuchingWallRight()) { canDoubleJump = true; canJump = true; } else { animator.SetBool("isJumping", true); }
            if (isTuchingWallRight() && !isGrounded()) { animator.SetBool("isTuchingWallRight", true); animator.SetBool("isJumping", false); } else { animator.SetBool("isTuchingWallRight", false); }
            if (isTuchingWallLeft() && !isGrounded()) { animator.SetBool("isTuchingWallLeft", true); animator.SetBool("isJumping", false); } else { animator.SetBool("isTuchingWallLeft", false); }
            if (isGrounded()) { canJump = true; canDash = true; animator.SetBool("isJumping", false); }
           // if (isGrounded() && body.velocity.x == 0f ){ canDash = false; }


            //DASH
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && canDash)
            {

                if (!isTuchingWallLeft() || !isTuchingWallRight())
                {
                    animator.SetBool("isDashing", true);
                    StartCoroutine(Dash());
                    canDoubleJump = false;
                    canJump = false;
                }


            }
            else { animator.SetBool("isDashing", false); }

            //JUMP
            if (!isDashing) 
            { 
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canJump)
                {
                    body.velocity = new Vector2(body.velocity.x, JumpPower);
                    animator.SetBool("isJumping", true);
                    canJump = false;

                }
            }
            //DOUBLE JUMP

            if (!isGrounded() && canDash) { canDoubleJump = true; } else { canDoubleJump = false; }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canDoubleJump)
            {
                body.velocity = new Vector2(body.velocity.x, JumpPower);
                canDash = false;
            }


            //JUMP DROP
            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1)) && body.velocity.y > 0f)
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
            }




        }
        else { animator.SetBool("isDead", true); animator.SetBool("isDashing", false); animator.SetBool("isJumping", false); animator.SetBool("isTuchingWallRight", false); animator.SetBool("isTuchingWallLeft", false); }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0);
        trail.emitting = true;
        Time.timeScale = 2f;
        yield return new WaitForSeconds(dashingTime);
        Time.timeScale = 1f;
        trail.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }

    private bool isTuchingWallLeft()
    {
        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wallLayer);
        
    }

    private bool isTuchingWallRight()
    {
        return Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wallLayer);
    }


    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(body.velocity.x));
        animator.SetFloat("yVelocity", body.velocity.y);
    }
    private void flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale =  transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }
}
