using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 50f;
    public float moveSpeed = 5f;
    [SerializeField] private LayerMask platformLayerMask;
    private GameObject playerObject;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;

    private Animator animate;
    float inputHorizontal;
    float inputVertical;
    bool facingRight = true;

    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;

    // Start is called before the first frame update
    private void Start()
    {
        playerObject = GetComponent<GameObject>();  
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animate = gameObject.GetComponent<Animator>();

        jumpForce = 60f;
        isJumping = false;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();

        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate(){
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if(inputHorizontal > 0 && !facingRight){
            Flip();
        }
         if(inputHorizontal < 0 && facingRight){
            Flip();
        }

        if (!isJumping && moveVertical > 0.1f){
            rigidBody2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            animate.SetBool("isJump", true);
        }else{
            animate.SetBool("isJump", false);
            
        }
    }

    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Platform"){
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Platform"){
            isJumping = true;
        }
    }

    private bool IsGrounded()
    {
        var boxColliderBounds = boxCollider2D.bounds;
        var raycastHit2D = Physics2D.BoxCast(boxColliderBounds.center, boxColliderBounds.size, 0f, Vector2.down, 1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

    private void HandleJump()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.UpArrow)){
            animate.SetBool("isJump", true);
            rigidBody2D.velocity = Vector2.up * jumpSpeed;
        }
            animate.SetBool("isJump", false);

    }

    private void HandleRun()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) && IsGrounded() ? 40f : 20f;
    }

    private void HandleMovement()
    {
        HandleRun();
        //HandleJump();
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
