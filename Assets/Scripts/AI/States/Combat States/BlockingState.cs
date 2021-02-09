using System.Collections;
using System.Collections.Generic;
using AI;
using AI.States;
using UnityEngine;

public class BlockingState : State
{
    private Animator _anim;
    private EnemyAction _enemyAction;
    
    private float _blockingCountDown;
    private bool _alreadBlocking;
    
    public BlockingState(GameObject go, StateMachine sm) : base(go, sm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _anim = _go.GetComponent<Animator>();
        _enemyAction = _go.GetComponent<EnemyAction>();
        _alreadBlocking = false;
        _blockingCountDown = 5f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!_alreadBlocking)
        {
            _alreadBlocking = true; 
            DoBlock();
        }

        _blockingCountDown -= Time.fixedDeltaTime;

        if (_blockingCountDown <= 0)
        {
            Debug.Log("Exit Block");
            _enemyAction.isKeepBlocking = false;
            _sm._CurState = new AttackingState(_go, _sm);
        }

    }

    private void DoBlock()
    {
       _enemyAction.isKeepBlocking = true;
       _enemyAction.action = EnemyAction.EnemyActionType.Block;
    }
    
}
