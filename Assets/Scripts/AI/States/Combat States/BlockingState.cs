using System.Collections;
using System.Collections.Generic;
using AI;
using AI.States;
using UnityEngine;

public class BlockingState : State
{
    private Animator _anim;
    private float _blockingCountDown;
    private bool _alreadBlocking;
    
    private static readonly int Block = Animator.StringToHash("Block");
    private static readonly int IsKeepBlocking = Animator.StringToHash("isKeepBlocking");

    public BlockingState(GameObject go, StateMachine sm) : base(go, sm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Blocking State");
        _anim = _go.GetComponent<Animator>();
        _alreadBlocking = false;
        _blockingCountDown = 4f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!_alreadBlocking)
        {
            DoBlock();
            _alreadBlocking = true;
        }

        _blockingCountDown -= Time.fixedDeltaTime;

        if (_blockingCountDown <= 0)
        {
            _anim.SetBool(IsKeepBlocking, false);
            _anim.ResetTrigger(Block);
            _sm._CurState = new AttackingState(_go, _sm);
        }

    }

    private void DoBlock()
    {
        _anim.SetTrigger(Block);
    }
    
}
