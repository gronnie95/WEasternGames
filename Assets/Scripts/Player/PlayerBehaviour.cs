using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    ActionType playerAction;
    private Animator _anim;
    
    public GameObject weapon;

    #region Attack Status Check
    //public bool isOnAttackAction = false;
    public bool isOnLightAction = false;
    public bool isOnHeavyAction = false;
    private bool beforeDoATK = false;
    private bool duringDoATK = false;
    private bool afterDoATK = false;
    public bool canCauseDmgByLightATK = false;
    public bool canCauseDmgByHeavyATK = false;
    public float causeDMGTime = 0;
    #endregion

    #region Light Attack
    float beforeLAtkTime = 0f;
    float duringLAtkTime = 0.5f;
    float afterLAtkTime = 0.8f;
    #endregion

    #region Heavy Attack
    public float beforeHAtkTime = 0f;
    public float duringHAtkTime = 0.5f;
    public float afterHAtkTime = 1.6f;
    #endregion

    PlayerAction playerActionType;

    void Start()
    {
        _anim = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>().action;
        playerActionType = this.GetComponent<PlayerAction>();
    }

    void FixedUpdate()
    {
        
    }




    private void doHeavyAttack()
    {
        if (isOnHeavyAction == true)
        {
            if (beforeHAtkTime > 0 && beforeDoATK == false) // before do Action
            {
               beforeHAtkTime -= Time.deltaTime;
            }
            if (beforeHAtkTime <= 0 && beforeDoATK == false) //check before do atk action is finished
            {
                GetComponent<PlayerStats>().stamina -= 10;
                GetComponent<PlayerStats>().readyToRestoreStaminaTime = GetComponent<PlayerStats>().setReadyToRestoreStaminaTime();
                beforeDoATK = true;
                causeDMGTime = 1.0f;
                canCauseDmgByHeavyATK = true;
            }

            #region cause damge logic
            if(causeDMGTime >= 0f)
            {
                causeDMGTime -= Time.deltaTime;
            }
            if(causeDMGTime > 0f && causeDMGTime < 0.35f)
            {
                //Debug.Log("is it time to cause dmg");
            }
            if(causeDMGTime <= 0f)
            {
                canCauseDmgByHeavyATK = false;
            }
            #endregion

            if (beforeDoATK == true && duringDoATK == false) // do Action
            {
                if (duringHAtkTime > 0 && duringDoATK == false) // doing attack action
                {
                    if (duringHAtkTime >= 0.5f)
                    {
                        _anim.SetTrigger("Heavy Attack");
                    }
                    duringHAtkTime -= Time.deltaTime;
                }
                if (duringHAtkTime <= 0 && duringDoATK == false)
                {
                    duringDoATK = true;
                }
            }

            if (duringDoATK == true && afterDoATK == false) // finished one loop of action
            {
                if (afterHAtkTime > 0 && afterDoATK == false)
                {
                    afterHAtkTime -= Time.deltaTime;
                }
                if (afterHAtkTime <= 0 && afterDoATK == false)
                {
                    afterDoATK = true;
                }
            }
        }

        if (afterDoATK == true || isOnHeavyAction == false) //reset all values
        {
            beforeHAtkTime = 0.1f;
            duringHAtkTime = 0.5f;
            afterHAtkTime = 1.6f; // set cooldown time for next attack
            isOnHeavyAction = false;
            beforeDoATK = false;
            duringDoATK = false;
            afterDoATK = false;
            playerActionType.action = ActionType.Idle;
        }
    }

    private void doLightAttack()
    {
        if(isOnLightAction == true)
        {
            if (beforeLAtkTime > 0 && beforeDoATK == false) // before do Action
            {
               beforeLAtkTime -= Time.deltaTime;
            }
            if (beforeLAtkTime <= 0 && beforeDoATK == false) //check before do atk action is finished
            {
                GetComponent<PlayerStats>().stamina -= 5;
                //GetComponent<PlayerStats>().readyToRestoreStaminaTime = GetComponent<PlayerStats>().setReadyToRestoreStaminaTime();
                //Debug.Log("Before Action is Done");
                beforeDoATK = true;
                causeDMGTime = 1.0f;
                canCauseDmgByLightATK = true;
            }

            #region cause damge logic
            if (causeDMGTime >= 0f)
            {
                causeDMGTime -= Time.deltaTime;
            }
            if (causeDMGTime > 0f && causeDMGTime < 1.0f)
            {
                //Debug.Log("is it time to cause dmg");
            }
            if (causeDMGTime <= 0f)
            {
                canCauseDmgByLightATK = false;
            }
            #endregion

            if (beforeDoATK == true && duringDoATK == false) // do Action
            {     
                if (duringLAtkTime > 0 && duringDoATK == false) // doing attack action
                {
                    if(duringLAtkTime >= 0.5f)
                    {
                        _anim.SetTrigger("Light Attack");
                    }
                    duringLAtkTime -= Time.deltaTime;
                }
                if (duringLAtkTime <= 0 && duringDoATK == false)
                {
                    //Debug.Log("Doing Action is Done");
                    duringDoATK = true;
                }
            }

            if (duringDoATK == true && afterDoATK == false) // finished one loop of action
            {
                if (afterLAtkTime > 0 && afterDoATK == false)
                {
                    afterLAtkTime -= Time.deltaTime;
                }
                if (afterLAtkTime <= 0 && afterDoATK == false)
                {
                    //Debug.Log("After Action is Done");
                    afterDoATK = true;
                }
            }
        }

        if (afterDoATK == true || isOnLightAction == false) //reset all values
        {
            beforeLAtkTime = 0.1f;
            duringLAtkTime = 0.5f;
            afterLAtkTime = 0.8f; // set cooldown time for next attack
            isOnLightAction = false;
            beforeDoATK = false;
            duringDoATK = false;
            afterDoATK = false;
            playerActionType.action = ActionType.Idle;
        }
    }
}