using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchParticipant : MonoBehaviour
{
    public enum CatchRole
    {
        Runner,
        Catcher
    }

    public bool _isCatcherAtStart;
    public float _catchImmunityTime = 2;//Make the Runner x seconds immune to being caught again
    public CatchRole _catchRole;
    public event Action<CatchRole> onRoleChanged;
    public static event Action<CatchParticipant> onCatcherChanged;//Parameter is the new catcher
    public static CatchParticipant _catcher;

    private CharacterMotor _motor;
    private Renderer _renderer;
    private float _lastCatchCollisionTime;

    private void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
        _renderer = GetComponent<Renderer>();

        if (_isCatcherAtStart == true)
            _CatchRole = CatchRole.Catcher;
        else
            _CatchRole = CatchRole.Runner;
    }

    public void Stun(float stunDuration)
    {
        _motor.Stun(stunDuration);
    }

    public void OnCollisionStay(Collision collision)
    {
        CatchParticipant collidingGameParticipant = collision.gameObject.GetComponent<CatchParticipant>();
        //If no longer immune to being gaught
        //If I am the catcher and catch a runner
        if (collidingGameParticipant != null && _CatchRole == CatchRole.Catcher && collidingGameParticipant._CatchRole == CatchRole.Runner)
        {
            if (_lastCatchCollisionTime + _catchImmunityTime < Time.time && collidingGameParticipant._lastCatchCollisionTime + _catchImmunityTime < Time.time && collidingGameParticipant._lastCatchCollisionTime + _catchImmunityTime < Time.time)
            {
                _lastCatchCollisionTime = Time.time;
                collidingGameParticipant._lastCatchCollisionTime = Time.time;

                _CatchRole = CatchRole.Runner;
                collidingGameParticipant._CatchRole = CatchRole.Catcher;
            }
        }
    }

    public CatchRole _CatchRole
    {
        get => _catchRole;
        set
        {
            _catchRole = value;
            if (_catchRole == CatchRole.Runner)
                _renderer.material.color = Color.cyan;

            if (_catchRole == CatchRole.Catcher)
            {
                _renderer.material.color = Color.red;

                if (_catcher != this)
                {
                    _catcher = this;
                    if(onCatcherChanged != null)
                        onCatcherChanged(this);
                }

                Stun(2);
            }
            if (onRoleChanged != null)
                onRoleChanged(_catchRole);

        }
    }
}
