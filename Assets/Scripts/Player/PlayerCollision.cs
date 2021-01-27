using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Player Get Enemy Hit
        if (collision.gameObject.tag == "EnemyWeapon")
        {
            // player is blocking and get hit by enemy
            if (collision.gameObject.GetComponent<Collider>().isTrigger == false && 
                player.GetComponent<PlayerAction>().isKeepBlocking == true &&
                player.GetComponent<PlayerAction>().isPerfectBlock == false && 
                player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("B"))
            {
                #region get enemy heavy attack
                if (player.GetComponent<PlayerStats>().hitStunValue > 0 && 
                    collision.gameObject.GetComponent<EnemyWeaponCollision>().enemyActionType == EnemyAction.EnemyActionType.HeavyAttack)
                {
                    player.GetComponent<PlayerStats>().hitStunValue -= 100;
                    if (player.GetComponent<PlayerStats>().hitStunValue <= 0)
                    {
                        player.GetComponent<PlayerStats>().health -= 40;
                        player.GetComponent<PlayerStats>().stamina -= 40;
                        player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isInjured");
                    }
                }
                #endregion

                #region get enemy light attack
                //get enemy light attack
                if (player.GetComponent<PlayerStats>().hitStunValue > 0 && 
                    collision.gameObject.GetComponent<EnemyWeaponCollision>().enemyActionType == EnemyAction.EnemyActionType.LightAttack)
                {
                    // player.GetComponent<PlayerStats>().health -= 20;
                    player.GetComponent<PlayerStats>().stamina -= 10;
                    player.GetComponent<PlayerStats>().hitStunValue -= 20;
                    player.GetComponent<PlayerStats>().hitStunRestoreSecond = 5.0f;

                    if (player.GetComponent<PlayerStats>().hitStunValue > 0)
                    {
                        player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetBlockingImpact");

                        // spawn sword clash effect
                        player.GetComponent<SwordEffectSpawner>().SpawnSwordClash();
                    }

                    else if (player.GetComponent<PlayerStats>().hitStunValue <= 0)
                    {
                        player.GetComponent<PlayerStats>().health -= 40;
                        player.GetComponent<PlayerStats>().stamina -= 40;
                        player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isInjured");
                    }
                }
                #endregion
                collision.gameObject.GetComponent<Collider>().isTrigger = true;
                player.GetComponent<PlayerAction>().isPlayerAttacking = false;
            }

            // player is blocking and get hit by enemy
            else if (collision.gameObject.GetComponent<Collider>().isTrigger = false && 
                player.GetComponent<PlayerAction>().isKeepBlocking == true && 
                player.GetComponent<PlayerAction>().isPerfectBlock == false && 
                player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("BI"))
            {
                player.GetComponent<PlayerStats>().hitStunValue -= 20;
                Debug.Log("in BI");

                player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetBlockingImpact");
                player.GetComponent<PlayerAction>().isPlayerAttacking = false;

                // spawn sword clash effect
                player.GetComponent<SwordEffectSpawner>().SpawnSwordClash();
            }

            // player is in idle action and get hit by enemy
            else if (player.GetComponent<PlayerAction>().isKeepBlocking == false)
            {
                player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isInjured");
                player.GetComponent<PlayerAction>().isPlayerAttacking = false;
                //Debug.Log("player injured");
            }

            // player is in perfect block Transistion but not in perfect block timing
            // (GetCurrentAnimatorStateInfo(0).IsTag("PB")) get current animator state by tag https://forum.unity.com/threads/current-animator-state-name.331803/
            if ((player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("PB") ||
                player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("A")) && 
                player.GetComponent<PlayerAction>().isPerfectBlock == false)
            {
                player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isInjured");
                player.GetComponent<PlayerAction>().isPlayerAttacking = false;
            }

            if ((player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("HT") || 
                player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("LT")))
            {
                player.GetComponent<PlayerAction>().isPlayerAttacking = false;
            }

            player.GetComponent<PlayerAnimation>()._anim.ResetTrigger("isPlayerLightAttack");
            player.GetComponent<PlayerAnimation>()._anim.ResetTrigger("isPlayerHeavyAttack");
            player.GetComponent<PlayerAnimation>()._anim.ResetTrigger("Dodge");
            #endregion
        }
    }


}
