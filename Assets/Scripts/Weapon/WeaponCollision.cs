using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public GameObject player;
    public PlayerAction playerAction;
    PlayerStats playerStats;
    GameObject targetEnemy;

    void Start()
    {
        player = this.transform.root.Find("Player").gameObject;
        playerAction = this.player.GetComponent<PlayerAction>();
        playerStats = this.player.GetComponent<PlayerStats>();
        this.GetComponent<Collider>().isTrigger = true;
    }

    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // player is in perfect block Transistion but not in perfect block timing
            if (this.GetComponent<Collider>().isTrigger == false && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == false && collision.gameObject.GetComponent<EnemyAction>().isKeepBlocking == false)
            {
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.ResetTrigger("isInjured");
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.SetTrigger("isInjured");
            }

            // player is blocking and get hit by enemy
            else if (this.GetComponent<Collider>().isTrigger == false && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == false && collision.gameObject.GetComponent<EnemyAction>().isKeepBlocking == true )
            {
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.ResetTrigger("isGetBlockingImpact");
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.SetTrigger("isGetBlockingImpact");

                // spawn sword clash effect
                collision.gameObject.GetComponentInParent<SwordEffectSpawner>().SpawnSwordClash();
            }
            // enemy is in perfect block
            else if (this.GetComponent<Collider>().isTrigger == false && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == true)
            {
                player.GetComponent<PlayerAnimation>()._anim.ResetTrigger("isGetEnemyPerfectBlock");
                player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetEnemyPerfectBlock");
                playerAction.isPlayerAttacking = false;
                playerStats.isHitStun = true;

                // spawn sword clash effect
                collision.gameObject.GetComponentInParent<SwordEffectSpawner>().SpawnBigSwordClash();
            }
            
            //enemy is not in perfect block
            if (this.GetComponent<Collider>().isTrigger == false && (collision.gameObject.GetComponent<EnemyAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("PB") ||
                    collision.gameObject.GetComponent<EnemyAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("A")) && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == false)
            {
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.ResetTrigger("isInjured");
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.SetTrigger("isInjured");
                collision.gameObject.GetComponent<Enemy>().HP -= 20;
                collision.gameObject.GetComponent<Enemy>().stamina -= 20;
                collision.gameObject.GetComponent<Enemy>().readyToRestoreStaminaTime = 5.0f;
            }
            this.GetComponent<Collider>().isTrigger = true;
        }
    }
}

