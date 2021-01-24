using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerAction playerAction;
    PlayerJump playerJump;
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    PlayerAnimation playerAnimation;
    float clickStartTime;

    void Awake()
    {
        playerAction = GetComponent<PlayerAction>();
        playerJump = GetComponent<PlayerJump>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        Control();
        //Debug.Log(GamePreload.images[1].name);
    }

    void Control()
    {
        if(!playerStats.isBlockStun && !playerStats.isHitStun && !playerAnimation._anim.GetCurrentAnimatorStateInfo(0).IsTag("BI"))
        {
            AttackType();
            Block();
            Sprint();
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerAction.isKeepBlocking = false;
        }
    }

    void Sprint()
    {
        if(playerJump.isJump == false && !playerAnimation._anim.GetCurrentAnimatorStateInfo(0).IsTag("LT") && !playerAnimation._anim.GetCurrentAnimatorStateInfo(0).IsTag("HT") && playerAction.isKeepBlocking == false && playerStats.stamina > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerMovement.isSprinting = true;
                playerMovement.isDodging = true;
                playerAction.action = ActionType.Dodge;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerMovement.isSprinting = false;
            }
        }
    }

    void AttackType()
    {
        if(!playerAction.isPlayerAttacking && !playerStats.isHitStun) //if the player didnt do any attack action
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
                    playerAction.action = ActionType.HeavyAttack;
                }
            }

            if (Input.GetMouseButtonUp(0)) // Light Attack
            {
                float onHoldTime = Time.time - clickStartTime;

                if (onHoldTime < 0.25f)
                {
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
    }
}
