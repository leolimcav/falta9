using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 0.2f;
    public float moveSpeed = 5f;
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
        var raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 1f, platformLayerMask);
        return raycastHit2D.collider != null;
    }

    private void HandleJump()
    {
        if(IsGrounded() && Input.GetKeyDown(KeyCode.UpArrow))
            rigidBody2D.velocity = Vector2.up * jumpSpeed; 
    }

    private void HandleMovement()
    {
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
