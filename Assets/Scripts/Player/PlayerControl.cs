using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerAction playerAction;
    float clickStartTime;
    public bool isOnAttackAction;

    void Awake()
    {
        playerAction = GetComponent<PlayerAction>();
    }

    void Update()
    {
        Control();
        //Debug.Log(GamePreload.images[1].name);
    }

    void Control()
    {
        AttackType();
        Block();
    }

    void AttackType()
    {
        if(isOnAttackAction == false) //if the player didnt do any attack action
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickStartTime = Time.time; //record the clicking time
            }

            if (Input.GetMouseButton(0)) //Heavy Attack
            {
                float onHoldTime = Time.time - clickStartTime;

                if (onHoldTime >= 0.4f)
                {
                    isOnAttackAction = true;
                    playerAction.action = ActionType.HeavyAttack;
                }
            }

            if (Input.GetMouseButtonUp(0)) // Light Attack
            {
                float onHoldTime = Time.time - clickStartTime;

                if (onHoldTime < 0.25f)
                {
                    isOnAttackAction = true;
                    playerAction.action = ActionType.LightAttack;

                }
            }
        }
    }

    void Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            playerAction.action = ActionType.SwordBlock;
        }
        if (Input.GetMouseButton(1))
        {
            playerAction.isKeepBlocking = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerAction.isKeepBlocking = false;
        }
    }
}
