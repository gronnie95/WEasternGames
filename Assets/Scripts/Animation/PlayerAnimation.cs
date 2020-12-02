using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator _anim;
    private PlayerAction playerAction;
    private PlayerControl playerControl;
    private PlayerBehaviour playerBehaviour;
    private PlayerJump playerJump;
    private DoubleJump doubleJump;
    public GameObject enemy;
    private EnemyAction enemyAction;

    void Start()
    {
        _anim = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>();
        playerControl = GetComponent<PlayerControl>();
        playerBehaviour = GetComponent<PlayerBehaviour>();
        playerJump = GetComponent<PlayerJump>();
        doubleJump = GetComponent<DoubleJump>();
        enemy = GameObject.Find("Enemy");
        enemyAction = this.enemy.GetComponent<EnemyAction>();

    }

    void FixedUpdate()
    {
        initialiseAnimatorBool();
    }

    void initialiseAnimatorBool()
    {
        #region Player Block
        _anim.SetBool("isKeepBlocking", playerAction.isKeepBlocking);
        _anim.SetBool("isPerfectBlock", playerAction.isPerfectBlock);
        _anim.SetBool("isBlockingEnd", playerAction.isBlockingEnd);
        #endregion
        #region Jump
        _anim.SetBool("isFirstJump", playerJump.isJump);
        _anim.SetBool("isSecondJump", doubleJump.isDoubleJump);
        _anim.SetBool("isFalling", playerJump.isFalling);
        _anim.SetBool("isGrounded", playerJump.isGrounded);
        _anim.SetBool("FallingToGround", playerJump.fallingToGround);
        #endregion

        _anim.SetBool("isBlockingImpact", playerAction.isBlockingImpact);
        _anim.SetBool("isGetHurt", playerAction.isHurt);
        _anim.SetBool("isHitPlayer", enemyAction.isHitPlayer);
        _anim.SetBool("IsPerfectBlockTiming", enemyAction.isPerfectBlockTiming);
    }

    public void OnAnimation_isPerfectBlock()
    {
        playerAction.isPerfectBlock = true;
    }

    public void OnAnimation_isPerfectBlockEnd()
    {
        playerAction.isPerfectBlock = false;
    }

    public void OnAnimation_isImpact()
    {
        playerAction.isBlockingImpact = false;
    }

    public void OnAnimation_isImpactEnd()
    {
        //playerAction.isImpact = false;
        playerControl.isOnAttackAction = false;
    }

    public void OnAnimation_endLightAttack()
    {
        playerControl.isOnAttackAction = false;
    }

    public void OnAnimation_endHeavyAttack()
    {
        playerControl.isOnAttackAction = false;
    }

    public void OnAnimation_isGetCriticalHit()
    {
        playerAction.isHurt = false;
        playerControl.isOnAttackAction = false;
    }
    public void OnAnimation_isGetCriticalHitEnd()
    {
        playerControl.isOnAttackAction = false;
    }
    



}
