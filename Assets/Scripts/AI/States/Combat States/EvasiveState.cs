using AI;
using AI.States;
using TMPro;
using UnityEngine;

public class EvasiveState : State
{
    private Animator _anim;
    private Transform _player;
    private float _moveSpeed = 0.5f;
    private Vector3 _centre;
    private float _angle;
    private float _radius;
    private float _timer;
    
    #region Blend Tree Parameters
    
    private float _xVel;
    private int _xVelHash;
    
    #endregion
    
    public EvasiveState(GameObject go, StateMachine sm) : base(go, sm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Evasive State");
        _anim = _go.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _radius = 5f;
        _timer = 10f;
        
        //For the blend tree animation
        _xVelHash = Animator.StringToHash("enemyVelX");
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

        //Updates the blend tree to perform a walk animation
        _xVel = -1f;
        _anim.SetFloat(_xVelHash, _xVel);

        
        if (_timer <= 0)
        {
            //Return to a follow state to get back to the player's position to start combat again
            _xVel = 0;
            _anim.SetFloat(_xVelHash, _xVel);
            _sm._CurState = new CombatWalk(_go, _sm, true);
        }
    }
}
