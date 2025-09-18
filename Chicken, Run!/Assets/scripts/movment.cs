using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class movment : MonoBehaviour
{
    float horizontal;
    float speed = 8f;
    float JumpPower = 16f;
    bool isFacingRight = true;
    bool canDash = false;

        

    [SerializeField] Rigidbody2D body;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheckRight;
    [SerializeField] Transform wallCheckLeft;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] SpriteRenderer sprite;

    void Start()
    {
      
    }

   
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        flip();

        //DASH OBTAINED
        if(isTuchingWallLeft() || isTutchingWallRight()) {canDash = true;}
        if (isGrounded()) { canDash = true; }
        if (canDash) { sprite.color = Color.red; } else { sprite.color = Color.white; }

        
        //DASH
        if (Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0) && !isGrounded())
        {
                
            for (float i = 0f; i < 4f; i = i + 0.002f)
            {
            if (isFacingRight && canDash)
            {
                body.AddForce(Vector2.right * 3, ForceMode2D.Force);
            }
            if (!isFacingRight && canDash)
            {
                body.AddForce(Vector2.left * 3, ForceMode2D.Force);
                    
            }
                    
            }
         canDash = false;
        }

        //JUMP
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded())
        {
            body.velocity = new Vector2 (body.velocity.x, JumpPower);
            
            
        }

        //DOUBLE JUMP
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded() && canDash)
        {
            body.velocity = new Vector2(body.velocity.x, JumpPower);
            canDash = false;
        }
            
        
        //JUMP DROP
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }


      

    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isTuchingWallLeft()
    {
        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wallLayer);
        
    }

    private bool isTutchingWallRight()
    {
        return Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wallLayer);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
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
