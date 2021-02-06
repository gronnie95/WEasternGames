using System.Collections;
using System.Collections.Generic;
using AI;
using AI.States;
using UnityEngine;

public class CombatFollow : FollowState
{
    private Animator _anim;
    private Transform _player;
    

    public CombatFollow(GameObject go, StateMachine sm) : base(go, sm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _anim = _go.GetComponent<Animator>();
        _player = 
    }
}
