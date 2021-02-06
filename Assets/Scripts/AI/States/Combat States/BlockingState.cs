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
    
    public BlockingState(GameObject go, StateMachine sm) : base(go, sm)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
        }

        _blockingCountDown -= Time.fixedDeltaTime;

        if (_blockingCountDown <= 0)
        {
            _anim.SetBool("isKeepBlocking", false);
            _anim.ResetTrigger(Block);
            _sm._CurState = new CombatFollow(_go, _sm);
        }

    }

    private void DoBlock()
    {
        _anim.SetTrigger(Block);
    }
    
}
