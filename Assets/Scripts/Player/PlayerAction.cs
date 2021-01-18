using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Idle,
    Jump,
    LightAttack,
    HeavyAttack,
    SwordBlock,
}

public class PlayerAction : MonoBehaviour
{
    public ActionType action;

    private Animator _anim;
    PlayerJump playerJump;
    DoubleJump doubleJump;


    #region Sword Block
    public bool isPerfectBlock = false;
    public bool isKeepBlocking = false;
    public bool isBlockingEnd = false;
    #endregion
    
    public bool isHurt = false;

    private void Awake()
    {
        action = ActionType.Idle;
        _anim = GetComponent<Animator>();
        _anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimationController/PlayerAnimator"); //Load controller at runtime https://answers.unity.com/questions/1243273/runtimeanimatorcontroller-not-loading-from-script.html
    }

    void Update()
    {
        switch (action)
        {
            case ActionType.Idle:
                break;

            case ActionType.LightAttack:
                LightAttack();
                action = ActionType.Idle;
                break;

            case ActionType.HeavyAttack:
                HeavyAttack();
                action = ActionType.Idle;
                break;

            case ActionType.SwordBlock:
                Block();
                action = ActionType.Idle;
                break;

            case ActionType.Jump:
                Jump();
                action = ActionType.Idle;
                break;
        }
    }

    void LightAttack()
    {
        _anim.SetTrigger("isPlayerLightAttack");
    }

    void HeavyAttack()
    {
        _anim.SetTrigger("isPlayerHeavyAttack");
    }

    void Block()
    {
        isKeepBlocking = true;
        _anim.SetTrigger("Block");
    }

    void Jump()
    {
        _anim.SetTrigger("Jump");
    }
}