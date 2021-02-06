using System.Collections;
using System.Collections.Generic;
using AI;
using AI.States;
using TMPro;
using UnityEngine;

public class EvasiveState : State
{
    private Animator _anim;
    private Transform _player;
    private float _moveSpeed = 0.5f;
    private Rigidbody _rb;
    private Vector3 _centre;
    private float _angle;
    private float _radius;
    private float _timer;
    
    #region Animation Triggers
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private float _xVel;
    private float _zVel;
    
    private int _xVelHash;
    private int _zVelHash;

    #endregion
    
    
    public EvasiveState(GameObject go, StateMachine sm) : base(go, sm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _anim = _go.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = _go.GetComponent<Rigidbody>();
        _radius = 5f;
        _timer = 20f;
        
        //For the blend tree animation
        _xVelHash = Animator.StringToHash("enemyVelX");
        _zVelHash = Animator.StringToHash("enemyVelZ");
    }

    public override void FixedUpdate()
    {
        _timer -= Time.deltaTime; 
        
        //Circular motion
        _centre = _player.transform.position;
        _angle += _moveSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)) * _radius;
        _go.transform.position = _centre + offset;

        _go.transform.LookAt(_player.transform.position);

        #region Blend Tree Updates
            _zVel = -1f;
            _xVel = 1f;
            _anim.SetFloat(_xVelHash, _zVel);
            //_anim.SetFloat(_zVelHash, _zVel);
        #endregion

        
        if (_timer <= 0)
        {
            //Return to a follow state to get back to the player's position to start combat again
            _sm._CurState = new FollowState(_go, _sm);
        }
    }
}
