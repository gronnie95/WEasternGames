using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Player Stats
    public float health;
    public float stamina;
    public float speed;
    public HP hpUI;
    public Stamina staminaUI;
    private float maxStamina;
    private float restorePerSecond;
    #endregion

    #region Trigger
    public float readyToRestoreStaminaTime = 0;
    private float RestoreStaminaTime = 0;
    private bool isRestoreStamina = false;
    #endregion


    void Start()
    {
        health = 200;
        stamina = 200;
        speed = 4;
        maxStamina = stamina;
        restorePerSecond = maxStamina * 1 / 50;
        hpUI.SetMaxHP(health);
        staminaUI.SetMaxStaminaSlider(stamina);
    }

    void Update()
    {
        restoreStamina();
        loseCondition();
        setHealthUI();
        setStaminaUI();
    }

    void setStaminaUI()
    {
        staminaUI.setStaminaSlider(stamina);
    }

    void setHealthUI()
    {
        hpUI.setHealth(health);
    }

    void loseCondition()
    {
        if (health <= 0)
        {
            GetComponent<SwordCombat>().enabled = false;
            GetComponent<PlayerBehaviour>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAction>().enabled = false;
        }
    }

    void restoreStamina()
    {
        if (GetComponent<PlayerBehaviour>().isOnLightAction == false && GetComponent<PlayerBehaviour>().isOnHeavyAction == false
            && GetComponent<PlayerMovement>().isSprinting == false && GetComponent<SwordCombat>().isOnCombat == false)
        {
            if(readyToRestoreStaminaTime > 0) // Time preparation before restore stamina
            {
                readyToRestoreStaminaTime -= Time.deltaTime;
                isRestoreStamina = false;
            }
            if (readyToRestoreStaminaTime <= 0) // Time preparation before restore stamina
            {
                isRestoreStamina = true;
            }
            if (isRestoreStamina == true)
            {
                if (RestoreStaminaTime > 0)
                {
                    RestoreStaminaTime -= Time.deltaTime;
                }
                if (RestoreStaminaTime <= 0 && stamina <= maxStamina)
                {
                    stamina += restorePerSecond;
                    if (stamina >= maxStamina)
                    {
                        stamina = maxStamina;
                    }
                    if(stamina > 0)
                    {
                        GetComponent<PlayerMovement>().isOnKnockBack = false;
                    }
                    RestoreStaminaTime = setRestoreStaminaTime();
                }
            }      
        }
        if (stamina <= 0)
        {
            stamina = 0;
            speed = 4;
            GetComponent<PlayerMovement>().isSprinting = false;
            GetComponent<PlayerMovement>()._sprinting = false;

        }
        //Debug.Log(readyToRestoreStaminaTime);
    }

    public float setReadyToRestoreStaminaTime()
    {
        return 2.5f;
    }
    private float setRestoreStaminaTime()
    {
        return 0.2f;
    }
}
