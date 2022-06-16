using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 0.2f;
    public float moveSpeed = 5f;
    private GameObject playerObject;
    private Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            rigidBody2D.velocity = Vector2.up * jumpSpeed; 
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidBody2D.velocity = new Vector2(-moveSpeed, rigidBody2D.velocity.y);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
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
