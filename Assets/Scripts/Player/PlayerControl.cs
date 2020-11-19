using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerAction playerAction;
    PlayerBehaviour playerBehaviour;

    void Awake()
    {
        playerAction = GetComponent<PlayerAction>();
    }

    void Update()
    {
        Control();
    }

    void Control()
    {
        #region Block
        if(Input.GetMouseButtonDown(1))
        {
            playerAction.action = ActionType.SwordBlock;
        }
        if(Input.GetMouseButton(1))
        {
            playerAction.isKeepBlocking = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerAction.isKeepBlocking = false;
        }
        #endregion
    }
}
