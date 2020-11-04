using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    private Animator anim;

    //Animator Values
    private int _zVelHash;
    private int _xVelHash;
    private float _zVel = 0f;
    private float _xVel = 0f;

    //Move Setting
    public float turnSmooth = 0.1f;
    float turnSmoothVelocity;
    public float moveSpeed = 4f;
    public float horizontalVelocity;
    private bool _sprinting;

    //Jump Setting
    public float verticalVelocity;
    private float gravity = 10f;
    private float jumpForce = 5f;

    private Transform CameraPivot; //empty point on player
    public CharacterController myController;

    private Vector3 camDirection;
    public bool isOnKnockBack = false;

    //Sprint
    //private bool isAccelerated = false;
    private bool isAcceleratedFinished = false;
    private bool isAcclerationCoolDownOn = false;
    private float acclerationTime = 0.2f;
    private float acclerationCoolDown = 0.5f;

    //Remove later once network implementation is finished
    public bool player2;

    // Start is called before the first frame update
    void Start()
    {
        //this.CameraPivot = this.playerCamera.transform.parent;
        myController = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();

        _zVelHash = Animator.StringToHash("velZ");
        _xVelHash = Animator.StringToHash("velX");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        Movement(forwardPressed, rightPressed, leftPressed, backPressed, runPressed);
        Jump();

        anim.SetFloat(_xVelHash, _xVel);
        anim.SetFloat(_zVelHash, _zVel);
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //isAccelerated = true;
            isAcclerationCoolDownOn = true;
        }

        if (isAcclerationCoolDownOn == true && acclerationCoolDown >= 0) // accleration cool down
        {
            acclerationCoolDown -= Time.deltaTime;
        }

        if (isAcclerationCoolDownOn == true && acclerationTime >= 0) // player instant accleration 
        {
            _sprinting = true;
            moveSpeed = 15f;
            acclerationTime -= Time.deltaTime;
        }

        if (acclerationCoolDown <= 0)
        {
            acclerationTime = 0.2f;
            acclerationCoolDown = 0.5f;
            isAcclerationCoolDownOn = false;
            isAcceleratedFinished = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isAcceleratedFinished == true)
        {
            moveSpeed = 8f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _sprinting = false;
            moveSpeed = 4f;
            isAcceleratedFinished = false;
        }
    }

    public void Jump()
    {
        if (myController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime; //have a little pressure on player to stick to the floor
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        myController.Move(jumpVector * Time.deltaTime);
    }

    public void Movement(bool forwardPressed, bool rightPressed, bool leftPressed, bool backPressed, bool runPressed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Normalized so that if two keys are pressed the character doesn't go faster
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (forwardPressed && isOnKnockBack == false)
        {
            Sprint();
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            myController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            _zVel = _sprinting ? 2 : 1;
        }

        if (leftPressed && isOnKnockBack == false)
        {
            Sprint();
            Vector3 moveVector = -this.playerCamera.transform.right.normalized * moveSpeed;
            myController.Move(moveVector * Time.deltaTime);
            _xVel = _sprinting ? -2 : -1;
        }

        /*
        if (backPressed && isOnKnockBack == false)
        {
            anim.SetBool("walking", true);
            Sprint();
            Vector3 moveVector = new Vector3(-camDirection.x * moveSpeed, 0, -camDirection.z * moveSpeed);
            myController.Move(moveVector * Time.deltaTime);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(moveVector.normalized), 0.1f);
            //Debug.Log("pressing S");
            //this.transform.position += new Vector3(-camDirection.x * moveSpeed, 0, -camDirection.z * moveSpeed);
        }*/

        if (rightPressed && isOnKnockBack == false)
        {
            Sprint();
            Vector3 moveVector = this.playerCamera.transform.right.normalized * moveSpeed;
            myController.Move(moveVector * Time.deltaTime);
            _xVel = _sprinting ? 2 : 1;
        }

        if (!leftPressed && !rightPressed)
        {
            _xVel = 0.0f;
        }

        if (!forwardPressed)
        {
            _zVel = 0.0f;
        }
    }
}