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
}