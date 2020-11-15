using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ActionType
{
    Idle,
    LightAttack,
    HeavyAttack,
    InstantBlock,
    LongBlock,
}

public class PlayerAction : MonoBehaviour
{
    public int PlayerStatus;
    private float clickStartTime;
    public bool doOnce;
    public Animator _anim;

    private void Start()
    {
        doOnce = false;
        PlayerStatus = 0;
        clickStartTime = 0;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GetComponent<SwordCombat>().isLostBodyBalance == false && GetComponent<SwordCombat>().isStepBack == false)
        {
            AttackType();
            DefenseType();
        }
    }

    void AttackType()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            clickStartTime = Time.time; //record the clicking time
        }

        if (Input.GetMouseButton(0) && doOnce == false) //Heavy Attack
        {
            float onHoldTime = Time.time - clickStartTime;

            if (onHoldTime >= 0.4f)
            {
                PlayerStatus = (int)ActionType.HeavyAttack;
                Debug.Log(PlayerStatus);
                doOnce = true;
            } 
        }

        if (Input.GetMouseButtonUp(0) && doOnce == false) // Light Attack
        {
            float onHoldTime = Time.time - clickStartTime;

            if (onHoldTime < 0.2f)
            {
                PlayerStatus = (int)ActionType.LightAttack;
                Debug.Log(PlayerStatus);
                doOnce = true;
                
            }
        }
    }

    void DefenseType()
    {
        if (Input.GetMouseButtonDown(1))
        {
            clickStartTime = Time.time; //record the clicking time
        }

        if (Input.GetMouseButton(1) && doOnce == false) //Long Block
        {
            float onHoldTime = Time.time - clickStartTime;

            if (onHoldTime >= 0.2f)
            {
                PlayerStatus = (int)ActionType.LongBlock;
                Debug.Log(PlayerStatus);
                doOnce = true;
            }
        }

        if (Input.GetMouseButtonUp(1) && doOnce == false) // Instant Block
        {
            float onHoldTime = Time.time - clickStartTime;

            if (onHoldTime < 0.2f)
            {
                PlayerStatus = (int)ActionType.InstantBlock;
                Debug.Log("pressed instant block button" + PlayerStatus);
                doOnce = true;
            }
        }
    }
}