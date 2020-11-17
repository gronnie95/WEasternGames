using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    enum ActionType
    {
        Idle,
        LightAttack,
        HeavyAttack,
        InstantBlock,
        LongBlock,
    }

    public int action;
}
