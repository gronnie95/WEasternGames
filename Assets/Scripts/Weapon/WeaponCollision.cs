using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public float enemyLightAtkKnockBackTime = 0.2f;
    public float enemyHeavyAtkKnockBackTime = 0.4f;
    private bool isOnCombat = false;

    public GameObject player;
    public PlayerAction playerAction;
    GameObject targetEnemy;

    void Start()
    {
        player = GameObject.Find("Player");
        playerAction = this.player.GetComponent<PlayerAction>();
    }

    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
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

