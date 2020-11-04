using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ActionType
{
    Idle,
    LightAttack,
    HeavyAttack,
}

public class PlayerAction : MonoBehaviour
{
    public int PlayerStatus;
    private float clickStartTime;
    public bool doOnce;

    private void Start()
    {
        doOnce = false;
        PlayerStatus = 0;
        clickStartTime = 0;
    }

    void Update()
    {
        AttackType();
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
}