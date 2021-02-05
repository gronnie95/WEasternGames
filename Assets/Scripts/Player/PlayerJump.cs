﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerAction playerAction;
    private Rigidbody _rigidbody;
    public bool isJump = false;
    public float jumpForce = 3.0f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    public float nextJumpTime;
    public int jumpTimes = 0;
    public bool isFalling = false;
    public bool fallingToGround = false;
    public bool isGrounded = true;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerAction = GetComponent<PlayerAction>();
    }

    void FixedUpdate()
    {
        Jump();
    }   

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            nextJumpTime = 0.2f;
            _rigidbody.velocity = (Vector3.up * jumpForce);
            isJump = true;
            jumpTimes++;
            playerAction.action = ActionType.Jump;
            isGrounded = false;
        }
        if(_rigidbody.velocity.y < 0) // falling more quicker
        {
            _rigidbody.velocity += Vector3.up * (fallMultiplier - 1) * Time.fixedDeltaTime; 
        }
        if(_rigidbody.velocity.y < -0.5f) // is on falling status
        {
            isFalling = true;
        }
        else if(_rigidbody.velocity.y >= -0.5f && _rigidbody.velocity.y < 0)  // is almost falling to ground
        {
            fallingToGround = true;
        }
        else if(_rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) // Jump up velocity
        {
            _rigidbody.velocity += Vector3.up * (lowJumpMultiplier - 1) * Time.fixedDeltaTime; 
        }
        if(nextJumpTime >= 0)  //give a time to fixed double jump bug
        {
            nextJumpTime -= Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJump = false;
            jumpTimes = 0;
            isFalling = false;
            isGrounded = true;
            fallingToGround = false;
        }
    }

}
