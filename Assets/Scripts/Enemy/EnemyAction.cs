using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public enum EnemyActionType
    {
        Idle,
        LightAttack,
        HeavyAttack,
        InstantBlock,
        LongBlock,
    }

    public EnemyActionType action;

    private Animator _anim;
    EnemyBehaviour enemyBehaviour;

    public bool isHitPlayer = false;
    public bool isReadyNextATK = true;

    public bool isImpact = false;
    public bool isPerfectBlockTiming = false;

    private void Start()
    {
        action = EnemyActionType.HeavyAttack;
        _anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    void Update()
    {
        if(isReadyNextATK == true)
        {
            switch (action)
            {
                case EnemyActionType.Idle:
                    break;

                case EnemyActionType.LightAttack:

                    break;

                case EnemyActionType.HeavyAttack:
                    HeavyAttack();
                    action = EnemyActionType.Idle;
                    break;
            }
        }

    }

    void HeavyAttack()
    {
        isReadyNextATK = false;
        _anim.SetTrigger("HeavyAttack");
    }
}
