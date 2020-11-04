using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int playerAction;
    private Animator _anim;
    
    public GameObject weapon;

    #region Attack Status Check
    public bool isOnAttackAction = false;
    private bool beforeDoATK = false;
    private bool duringDoATK = false;
    private bool afterDoATK = false;
    public static bool isLightHit = false;
    public static bool isHeavyHit = false;
    #endregion

    #region Light Attack
    float beforeLAtkTime = 0.3f;
    float duringLAtkTime = 0.001f;
    float afterLAtkTime = 0.2f;

    //float lightAction1 = 0.3f;
    float lightAction2Time = 0.15f;
    float lightAction3Time = 0.15f;
    float lightDuringTotal = 0.6f; //match with duringLAtkTime

    #endregion

    #region Heavy Attack
    float beforeHAtkTime = 0.8f;
    float duringHAtkTime = 0.5f;
    float afterHAtkTime = 0.5f;

    //float heavyAction1 = 0.3f;
    float heavyAction2Time = 0.2f;
    float heavyAction3Time = 0.2f;
    float heavyDuringTotal = 2.0f; //match with duringHAtkTime
    #endregion

    void Start()
    {
        _anim = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>().PlayerStatus;
    }

    void Update()
    {
        playerAction = GetComponent<PlayerAction>().PlayerStatus;

        #region Debug
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(playerAction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<PlayerAction>().PlayerStatus = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<PlayerAction>().PlayerStatus = 2;
        }
        #endregion

        switch (playerAction)
        {
            case (int)ActionType.Idle:
                //Debug.Log(playerAction);
                break;

            case (int)ActionType.LightAttack:
                isOnAttackAction = true;
                doLightAttack();
                break;

            case (int)ActionType.HeavyAttack:
                isOnAttackAction = true;
                doHeavyAttack();
                break;
        }
    }

  

    private void doHeavyAttack()
    {
        if (isOnAttackAction == true)
        {
            if (beforeHAtkTime > 0 && beforeDoATK == false) // before do Action
            {
               beforeHAtkTime -= Time.deltaTime;
            }
            if (beforeHAtkTime <= 0 && beforeDoATK == false) //check before do atk action is finished
            {
                Debug.Log("Before Action is Done");
                beforeDoATK = true;
            }

            if (beforeDoATK == true && duringDoATK == false) // do Action
            {
                if (duringHAtkTime > 0 && duringDoATK == false) // doing attack action
                {
                   
                    if (duringHAtkTime >= 0.5f && duringHAtkTime < 0.7f)
                    {
                        //pushWeapon(-2.0f, heavyAction3Time);
                        _anim.SetTrigger("Heavy Attack");
                    }

                    duringHAtkTime -= Time.deltaTime;
                }
                if (duringHAtkTime <= 0 && duringDoATK == false)
                {
                    Debug.Log("Doing Action is Done");
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
                    Debug.Log("After Action is Done");
                    afterDoATK = true;
                }
            }
        }

        if (afterDoATK == true || isOnAttackAction == false) //reset all values
        {
            beforeHAtkTime = 0.8f;
            duringHAtkTime = 0.5f;
            afterHAtkTime = 0.5f;
            isOnAttackAction = false;
            beforeDoATK = false;
            duringDoATK = false;
            afterDoATK = false;
            GetComponent<PlayerAction>().PlayerStatus = (int)ActionType.Idle;
            GetComponent<PlayerAction>().doOnce = false;
            //heavyAction1 = 0.3f;
            heavyAction2Time = 0.5f;
            heavyAction3Time = 0.5f;
            heavyDuringTotal = 2.0f;
            isHeavyHit = false;
        }
    }

    private void doLightAttack()
    {
        if(isOnAttackAction == true)
        {
            if (beforeLAtkTime > 0 && beforeDoATK == false) // before do Action
            {
               beforeLAtkTime -= Time.deltaTime;
            }
            if (beforeLAtkTime <= 0 && beforeDoATK == false) //check before do atk action is finished
            {
                Debug.Log("Before Action is Done");
                beforeDoATK = true;
            }
           
            if (beforeDoATK == true && duringDoATK == false) // do Action
            {
                
                if (duringLAtkTime > 0 && duringDoATK == false) // doing attack action
                {
                    _anim.SetTrigger("Light Attack");

                    duringLAtkTime -= Time.deltaTime;
                }
                if (duringLAtkTime <= 0 && duringDoATK == false)
                {
                    Debug.Log("Doing Action is Done");
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
                    Debug.Log("After Action is Done");
                    afterDoATK = true;
                }
            }
        }

        if (afterDoATK == true || isOnAttackAction == false) //reset all values
        {
            beforeLAtkTime = 0.3f;
            duringLAtkTime = 0.001f;
            afterLAtkTime = 0.2f;
            isOnAttackAction = false;
            beforeDoATK = false;
            duringDoATK = false;
            afterDoATK = false;
            GetComponent<PlayerAction>().PlayerStatus = (int)ActionType.Idle;
            GetComponent<PlayerAction>().doOnce = false;
            //lightAction1 = 0.3f;
            lightAction2Time = 0.15f;
            lightAction3Time = 0.15f;
            lightDuringTotal = 0.6f;
            isLightHit = false;
        }
    }
}