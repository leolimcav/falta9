using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovement : MonoBehaviour
{
    private LayerMask platformLayerMask;
    private readonly float jumpSpeed = GameConstants.PLAYER_BASE_JUMPHEIGHT;
    private float moveSpeed = GameConstants.PLAYER_BASE_MOVESPEED;
    private GameObject playerObject;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private StaminaBar staminaBar;

    private Animator animate;
    float inputHorizontal;
    bool facingRight = true;

    // Start is called before the first frame update
    private void Awake()
    {
        playerObject = GetComponent<GameObject>();  
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animate = gameObject.GetComponent<Animator>();
        platformLayerMask = LayerMask.GetMask("Platform");
        staminaBar = FindObjectOfType<StaminaBar>();
    }

    // private void Start()
    // {
    // }

    // Update is called once per frame
    // private void Update()
    // {
    // }

    private void FixedUpdate(){
        HandleMovement();
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(inputHorizontal > 0 && !facingRight){
            Flip();
        }
        
        if(inputHorizontal < 0 && facingRight){
            Flip();
        }
    }

    private void Flip(){
        var currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    
    private bool IsGrounded()
    {
        var boxColliderBounds = boxCollider2D.bounds;
        var raycastHit2D = Physics2D.BoxCast(
            boxColliderBounds.center, 
            boxColliderBounds.size, 
            0f, Vector2.down,
            1f,
            platformLayerMask
            );
        return raycastHit2D.collider.IsTouchingLayers(platformLayerMask);
    }

    private bool HasStamina()
    {
        return staminaBar.staminaSystem.GetStaminaNormalized() > 0;
    }
    
    private void HandleJump()
    {
        if(IsGrounded() && Input.GetKey(KeyCode.UpArrow)){
            animate.SetBool("isJump", true);
            rigidBody2D.velocity = Vector2.up * jumpSpeed;
        }
        else
        {
            animate.SetBool("isJump", false);
        }
    }

    private void HandleRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && HasStamina())
        {
            moveSpeed = GameConstants.PLAYER_BASE_RUNSPEED;
            animate.speed = 1.2f;
        }
        else
        {
            moveSpeed = GameConstants.PLAYER_BASE_MOVESPEED;
            animate.speed = 1f;
        }
    }

    private void HandleMovement()
    {
        HandleRun();
        HandleJump();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
            animate.SetBool("isRun", true);
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidBody2D.velocity = new Vector2(+moveSpeed, rigidBody2D.velocity.y);
                animate.SetBool("isRun", true);

            }
            else
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
                animate.SetBool("isRun", false);
            }
        }
    }

}
