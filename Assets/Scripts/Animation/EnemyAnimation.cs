﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;
    private EnemyAction enemyAction;
    
    public float AttackCD;
    private bool isCDOn = false;

    public Collider collider;

    private PlayerAction playerAction;
    public GameObject player;

    void Start()
    {
        _anim = GetComponent<Animator>();
        enemyAction = GetComponent<EnemyAction>();
        collider = GameObject.FindGameObjectWithTag("EnemyWeapon").GetComponent<BoxCollider>();
        player = GameObject.Find("Player");
        playerAction = this.player.GetComponent<PlayerAction>();
    }

    void FixedUpdate()
    {
        initialiseAnimatorBool();
        resetAttackCD();
    }

    void resetAttackCD()
    {
        if(AttackCD > 0 && isCDOn == true)
        {
            AttackCD -= Time.fixedDeltaTime;
        }
        if(AttackCD <= 0 && isCDOn == true)
        {
            isCDOn = false;
            enemyAction.isReadyNextATK = true;
            enemyAction.action = EnemyAction.EnemyActionType.HeavyAttack;
        }
    }

    void initialiseAnimatorBool()
    {
        _anim.SetBool("isReadyNextHeavyATK", enemyAction.isReadyNextATK);
        _anim.SetBool("IsEnemyAttack", enemyAction.IsEnemyAttack);
        _anim.SetBool("isPlayerPerfectBlock", playerAction.isPerfectBlock);
        _anim.SetBool("isImpact", enemyAction.isImpact);
        _anim.SetBool("PerfectBlockTiming", enemyAction.isPerfectBlockTiming);
    }
    
    public void OnAnimation_NextAttackCD()
    {
        AttackCD = 2.0f;
        isCDOn = true;
    }

    public void OnAnimation_IsEnemyAttack()
    {
        enemyAction.IsEnemyAttack = true;
        collider.isTrigger = false;
    }

    public void OnAnimation_IsEnemyAttackEnd()
    {
        enemyAction.IsEnemyAttack = false;
        enemyAction.isHitPlayer = false;
        collider.isTrigger = true;
    }

    public void OnAnimation_StopHeavyAttack()
    {
        enemyAction.IsEnemyAttack = false;
        enemyAction.isHitPlayer = false;
        collider.isTrigger = true;
        AttackCD = 2.0f;
        isCDOn = true;
        enemyAction.isImpact = true;
    }

    public void OnAnimation_StopHeavyAttackEnd()
    {
        enemyAction.isImpact = false;
    }

    public void OnAnimation_PerfectBlockTiming()
    {
        enemyAction.isPerfectBlockTiming = true;
    }

    public void OnAnimation_PerfectBlockTimingEnd()
    {
        enemyAction.isPerfectBlockTiming = false;
    }

}