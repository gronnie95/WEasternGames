using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private PlayerAction playerAction;
    private PlayerBehaviour playerBehaviour;
    private PlayerJump playerJump;
    private DoubleJump doubleJump;
    public GameObject enemy;
    private EnemyAction enemyAction;


    void Start()
    {
        _anim = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>();
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
        _anim.SetBool("isKeepBlocking", playerAction.isKeepBlocking);
        _anim.SetBool("isPerfectBlock", playerAction.isPerfectBlock);
        _anim.SetBool("isBlockingEnd", playerAction.isBlockingEnd);
        _anim.SetBool("isFirstJump", playerJump.isJump);
        _anim.SetBool("isSecondJump", doubleJump.isDoubleJump);
        _anim.SetBool("isFalling", playerJump.isFalling);
        _anim.SetBool("isGrounded", playerJump.isGrounded);
        _anim.SetBool("FallingToGround", playerJump.fallingToGround);
        _anim.SetBool("isImpact", playerAction.isImpact);
        _anim.SetBool("isHitPlayer", enemyAction.isHitPlayer);
        _anim.SetBool("IsEnemyAttack", enemyAction.IsEnemyAttack);
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
        playerAction.isImpact = true;
        enemyAction.IsEnemyAttack = false;
    }

    public void OnAnimation_isImpactEnd()
    {
        playerAction.isImpact = false;
    }


}
