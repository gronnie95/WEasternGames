using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollision : MonoBehaviour
{
    public GameObject enemy;
    private GameObject player;
    private EnemyAction enemyAction;
    private GameObject testPlayer;

    void Start()
    {
        enemy = GameObject.Find("Enemy");
        testPlayer = GameObject.Find("Player");
        enemyAction = this.enemy.GetComponent<EnemyAction>();
    }

    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            //if (testPlayer.GetComponent<PlayerAction>().isKeepBlocking == false)
            //{
            //    Debug.Log("message");
            //    collision.gameObject.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetDamage");
            //    this.GetComponent<Collider>().isTrigger = true;
            //    //enemyAction.isHitPlayer = true;

            //}
        }
        if (collision.gameObject.tag == "PlayerWeapon" || collision.gameObject.tag == "Player")
        {
            #region Weapon Collision Sound
            //if (collision.gameObject.GetComponent<WeaponCollision>().playerAction.isPerfectBlock == true)
            //{
            //    int playSoundNo = Random.Range(1, 2);
            //    switch (playSoundNo)
            //    {
            //        case 1:
            //            testPlayer.gameObject.GetComponent<SwordCombat>().heavy1.Play();
            //            break;
            //        case 2:
            //            testPlayer.gameObject.GetComponent<SwordCombat>().heavy2.Play();
            //            break;
            //    }
            //}
            //if (collision.gameObject.GetComponent<WeaponCollision>().playerAction.isKeepBlocking == true)
            //{
            //    int playSoundNo = Random.Range(1, 4);
            //    switch (playSoundNo)
            //    {
            //        case 1:
            //            testPlayer.gameObject.GetComponent<SwordCombat>().light1.Play();
            //            break;
            //        case 2:
            //            testPlayer.gameObject.GetComponent<SwordCombat>().light2.Play();
            //            break;
            //        case 3:
            //            testPlayer.gameObject.GetComponent<SwordCombat>().light3.Play();
            //            break;
            //        case 4:
            //            testPlayer.gameObject.GetComponent<SwordCombat>().light4.Play();
            //            break;
            //    }
            //}
            #endregion

            //player keep blocking & enemy attack is impacting on player
            if (testPlayer.GetComponent<PlayerAction>().isPerfectBlock == false && testPlayer.GetComponent<PlayerAction>().isKeepBlocking == true)
            {
                enemyAction.isHitPlayer = true;
                //this.GetComponent<Collider>().isTrigger = true;
            }

            //player perfect block reaction
            else if (enemyAction.isPerfectBlockTiming == true && testPlayer.GetComponent<PlayerAction>().isKeepBlocking == true)
            {
               // this.GetComponent<Collider>().isTrigger = true;
            }

            //player get hurt without blocking action
            else if (enemyAction.isHitPlayer == false && testPlayer.GetComponent<PlayerAction>().isPerfectBlock == false && testPlayer.GetComponent<PlayerAction>().isKeepBlocking == false)
            {
                testPlayer.GetComponent<PlayerAction>().isHurt = true;
                //this.GetComponent<Collider>().isTrigger = true;
            }

            //player fail to perfect block and get hit case 1
            else if (enemyAction.isHitPlayer == false && testPlayer.GetComponent<PlayerAction>().isPerfectBlock == true && enemyAction.isPerfectBlockTiming == false)
            {
                //Debug.Log("message");
                testPlayer.GetComponent<PlayerAction>().isHurt = true;
                //this.GetComponent<Collider>().isTrigger = true;
            }

            //player fail to perfect block and get hit case 2
            else if (enemyAction.isHitPlayer == false && testPlayer.GetComponent<PlayerAction>().isKeepBlocking == true && enemyAction.isPerfectBlockTiming == false)
            {
                //Debug.Log("message");
                testPlayer.GetComponent<PlayerAction>().isHurt = true;
               // this.GetComponent<Collider>().isTrigger = true;
            }
        }
    }

}
