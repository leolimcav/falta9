using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 17f;
    public float moveSpeed = 20f;
    [SerializeField] private LayerMask platformLayerMask;
    private GameObject playerObject;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    
    // Start is called before the first frame update
    private void Start()
    {
        playerObject = GetComponent<GameObject>();  
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
    }

    private bool IsGrounded()
    {
        var boxColliderBounds = boxCollider2D.bounds;
        var raycastHit2D = Physics2D.BoxCast(boxColliderBounds.center, boxColliderBounds.size, 0f, Vector2.down, 1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

    private void HandleJump()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.UpArrow))
            rigidBody2D.velocity = Vector2.up * jumpSpeed; 
    }

    private void HandleRun()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) && IsGrounded() ? 40f : 20f;
    }

    private void HandleMovement()
    {
        HandleRun();
        HandleJump();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidBody2D.velocity = new Vector2(+moveSpeed, rigidBody2D.velocity.y);
            }
            else
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }
        }
    }
}
