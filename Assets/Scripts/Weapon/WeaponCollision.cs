using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public GameObject player;
    public PlayerAction playerAction;
    GameObject targetEnemy;

    void Start()
    {
        player = this.transform.root.Find("Player").gameObject;
        playerAction = this.player.GetComponent<PlayerAction>();
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
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.SetTrigger("isInjured");
            }

            // player is blocking and get hit by enemy
            else if (this.GetComponent<Collider>().isTrigger == false && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == false && collision.gameObject.GetComponent<EnemyAction>().isKeepBlocking == true )
            {
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.SetTrigger("isGetBlockingImpact");

                // spawn sword clash effect
                collision.gameObject.GetComponentInParent<SwordEffectSpawner>().SpawnSwordClash();
            }
            // enemy is in perfect block
            else if (this.GetComponent<Collider>().isTrigger == false && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == true)
            {
                player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetEnemyPerfectBlock");

                // spawn sword clash effect
                collision.gameObject.GetComponentInParent<SwordEffectSpawner>().SpawnBigSwordClash();
            }
            
            //enemy is not in perfect block
            if (this.GetComponent<Collider>().isTrigger == false && (collision.gameObject.GetComponent<EnemyAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("PB") ||
                    collision.gameObject.GetComponent<EnemyAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("A")) && collision.gameObject.GetComponent<EnemyAction>().isPerfectBlock == false)
            {
                collision.gameObject.GetComponent<EnemyAnimation>()._anim.SetTrigger("isInjured");
                collision.gameObject.GetComponent<Enemy>().HP -= 20;
            }
            this.GetComponent<Collider>().isTrigger = true;
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Enemy")
    //    {
    //        if (playerBehaviour.causeDMGTime > 0f && playerBehaviour.causeDMGTime < 1.0f)
    //        {
    //            if(playerBehaviour.canCauseDmgByLightATK == true)
    //            {
    //                playerBehaviour.canCauseDmgByLightATK = false;
    //                isOnCombat = true;
    //                enemyLightAtkKnockBackTime = setEnemyLightAtkKnockBackTime();
    //                targetEnemy = other.gameObject;
    //                other.GetComponent<Enemy>().HP -= 30;
    //            }
    //        }

    //        if (playerBehaviour.causeDMGTime > 0f && playerBehaviour.causeDMGTime < 0.35f)
    //        {
    //            if(playerBehaviour.canCauseDmgByHeavyATK == true)
    //            {
    //                playerBehaviour.canCauseDmgByHeavyATK = false;
    //                isOnCombat = true;
    //                enemyLightAtkKnockBackTime = setEnemyHeavyAtkKnockBackTime();
    //                targetEnemy = other.gameObject;
    //                other.GetComponent<Enemy>().HP -= 50;
    //            }
    //        }
    //    }
    //}

    //void LightAtkKnockBackEnemy(GameObject enemy)
    //{
    //    float Velocity = 2f;
    //    if (enemyLightAtkKnockBackTime > 0)
    //    {
    //        enemyLightAtkKnockBackTime -= Time.fixedDeltaTime;
    //        Vector3 knockBackVector = (GameObject.Find("Player").transform.forward * Velocity * Time.fixedDeltaTime).normalized;
    //        enemy.GetComponent<Enemy>().enemyController.Move(knockBackVector);
    //    }
    //    if(enemyLightAtkKnockBackTime <= 0)
    //    {
    //        targetEnemy = null;
    //    }
    //    if(isOnCombat == true)
    //    {
    //        isOnCombat = false;
    //        player.GetComponent<SwordCombat>().resetOutOfCombatTime = player.GetComponent<SwordCombat>().setOutOfCombatTime();
    //        player.GetComponent<SwordCombat>().isOnCombat = true;
    //    }
    //}

    //void HeavyAtkKnockBackEnemy(GameObject enemy)
    //{
    //    float Velocity = 5f;
    //    if (enemyHeavyAtkKnockBackTime > 0)
    //    {
    //        enemyHeavyAtkKnockBackTime -= Time.fixedDeltaTime;
    //        Vector3 knockBackVector = (GameObject.Find("Player").transform.forward * Velocity * Time.fixedDeltaTime).normalized;
    //        enemy.GetComponent<Enemy>().enemyController.Move(knockBackVector);
    //    }
    //    if(enemyHeavyAtkKnockBackTime <= 0)
    //    {
    //        targetEnemy = null;
    //    }

    //    if (isOnCombat == true)
    //    {
    //        isOnCombat = false;
    //        player.GetComponent<SwordCombat>().resetOutOfCombatTime = player.GetComponent<SwordCombat>().setOutOfCombatTime();
    //        player.GetComponent<SwordCombat>().isOnCombat = true;
    //    }
    //}

    //float setEnemyHeavyAtkKnockBackTime()
    //{
    //    return 0.2f;
    //}

    //float setEnemyLightAtkKnockBackTime()
    //{
    //    return 0.1f;
    //}
}

