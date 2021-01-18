using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator _anim;
    private PlayerAction playerAction;
    private PlayerControl playerControl;
    private PlayerJump playerJump;
    private DoubleJump doubleJump;
    private AnimatorClipInfo[] clipInfo;
    private Collider collider;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimationController/PlayerAnimator"); //Load controller at runtime https://answers.unity.com/questions/1243273/runtimeanimatorcontroller-not-loading-from-script.html
        playerAction = GetComponent<PlayerAction>();
        playerControl = GetComponent<PlayerControl>();
        playerJump = GetComponent<PlayerJump>();
        doubleJump = GetComponent<DoubleJump>();
        clipInfo = _anim.GetCurrentAnimatorClipInfo(0); // get name of current animation state   https://stackoverflow.com/questions/34846287/get-name-of-current-animation-state
        collider = this.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand/" +
            "katana").gameObject.GetComponent<BoxCollider>(); // to find a child game object by name   //https://docs.unity3d.com/ScriptReference/Transform.Find.html
    }

    void FixedUpdate()
    {
        initialiseAnimatorBool();
        //Debug.Log(clipInfo[0].clip.name);
    }

    void initialiseAnimatorBool()
    {
        #region Player Block
        _anim.SetBool("isKeepBlocking", playerAction.isKeepBlocking);
        _anim.SetBool("isPerfectBlock", playerAction.isPerfectBlock);
        _anim.SetBool("isAttacking", collider.isTrigger);
        #endregion
        #region Jump
        _anim.SetBool("isFirstJump", playerJump.isJump);
        _anim.SetBool("isSecondJump", doubleJump.isDoubleJump);
        _anim.SetBool("isFalling", playerJump.isFalling);
        _anim.SetBool("isGrounded", playerJump.isGrounded);
        _anim.SetBool("FallingToGround", playerJump.fallingToGround);
        #endregion
    }

    #region Player Block Logic
    public void OnAnimation_isPerfectBlock()
    {
        playerAction.isPerfectBlock = true;
    }

    public void OnAnimation_isPerfectBlockEnd()
    {
        playerAction.isPerfectBlock = false;
    }
    #endregion

    #region Player Attack Logic 
    public void OnAnimation_IsHeavyAttackActive()
    {
        collider.isTrigger = false;
    }

    public void OnAnimation_IsHeavyAttackDeactive()
    {
        collider.isTrigger = true;
        playerControl.isOnAttackAction = false;
    }

    public void OnAnimation_IsLightAttackActive()
    {
        collider.isTrigger = false;
    }

    public void OnAnimation_IsLightAttackDeactive()
    {
        collider.isTrigger = true;
        playerControl.isOnAttackAction = false;
    }
    #endregion

    #region Player Get Hurt Logic
    public void OnAnimation_isGetCriticalHit()
    {
        playerControl.isOnAttackAction = false;
    }
    #endregion



}
