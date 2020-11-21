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
    public bool isImpact = false;
    #endregion

    private void Awake()
    {
        action = ActionType.Idle;
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (action)
        {
            case ActionType.Idle:
                break;

            case ActionType.LightAttack:

                break;

            case ActionType.HeavyAttack:

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

    void Block()
    {
        isKeepBlocking = true;
        _anim.SetTrigger("Block");
    }

    void Jump()
    {
        _anim.SetTrigger("Jump");
    }

    //void AttackType()
    //{
    //    if (Input.GetMouseButtonDown(0)) 
    //    {
    //        clickStartTime = Time.time; //record the clicking time
    //    }

    //    if (Input.GetMouseButton(0) && doOnce == false) //Heavy Attack
    //    {
    //        float onHoldTime = Time.time - clickStartTime;

    //        if (onHoldTime >= 0.4f)
    //        {
    //            PlayerStatus = (int)ActionType.HeavyAttack;
    //            Debug.Log(PlayerStatus);
    //            doOnce = true;
    //        } 
    //    }

    //    if (Input.GetMouseButtonUp(0) && doOnce == false) // Light Attack
    //    {
    //        float onHoldTime = Time.time - clickStartTime;

    //        if (onHoldTime < 0.2f)
    //        {
    //            PlayerStatus = (int)ActionType.LightAttack;
    //            Debug.Log(PlayerStatus);
    //            doOnce = true;

    //        }
    //    }
    //}


}