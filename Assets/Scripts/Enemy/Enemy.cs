using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterController enemyController;
    public int HP;
    public HP3D hpUI;

    void Start()
    {
        HP = 100;
        hpUI.SetMaxHP(HP);
    }

    void Update()
    {
        IsKilled();
        setEnemyHP();
    }

    private void setEnemyHP()
    {
        hpUI.setHealth(HP);
    }

    private void IsKilled()
    {
        if(HP <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}

