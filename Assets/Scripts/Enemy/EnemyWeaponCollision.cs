using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollision : MonoBehaviour
{
    EnemyBehaviour enemyBehaviour;
    public GameObject enemy;
    private GameObject player;
    private EnemyAction enemyAction;
    private GameObject testPlayer;

    public bool isWeaponEnter = false;

    void Start()
    {
        enemy = GameObject.Find("Enemy");
        testPlayer = GameObject.Find("Player");
        enemyBehaviour = this.enemy.GetComponent<EnemyBehaviour>();
        enemyAction = this.enemy.GetComponent<EnemyAction>();
    }

    void FixedUpdate()
    {
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Random ran = new Random();
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log(enemyBehaviour.causeDMGTime);

    //        //if (other.gameObject.GetComponent<PlayerAction>().PlayerStatus == (int)ActionType.InstantBlock)
    //        //{
    //        //    isPlayerDoBlock = true;
    //        //    if (enemyBehaviour.canCauseDmgByHeavyATK == true)
    //        //    {
    //        //        enemyBehaviour.canCauseDmgByHeavyATK = false;
    //        //        playerHeavyAtkKnockBackTime = setPlayerHeavyAtkKnockBackTime();
    //        //        player = other.gameObject;

    //        //        #region play sound and calculate dmg/hp

    //        //if (causeDMGTime >= 0.6f && causeDMGTime <= 1.2f) //perfect block
    //        //{

    //        //}

    //        //        if(enemyBehaviour.causeDMGTime > 1.2f && enemyBehaviour.causeDMGTime < 0.6f)
    //        //        {
    //        //            other.gameObject.GetComponent<SwordCombat>().normalBlockFromHeavyAttack = true;
    //        //            other.gameObject.GetComponent<SwordCombat>().isEncounter = true;
    //        //            other.GetComponent<PlayerStats>().health -= 30;
    //        //            other.GetComponent<PlayerStats>().stamina -= 30;
    //        //        }
    //        //        #endregion
    //        //    }
    //        //}

    //        if (other.gameObject.GetComponent<PlayerAction>().PlayerStatus == (int)ActionType.LongBlock) // long block
    //        {
    //            if (enemyBehaviour.canCauseDmgByHeavyATK == true)
    //            {
    //                isPlayerDoBlock = true;
    //                enemyBehaviour.canCauseDmgByHeavyATK = false;
    //                playerHeavyAtkKnockBackTime = setPlayerHeavyAtkKnockBackTime();
    //                player = other.gameObject;

    //                other.gameObject.GetComponent<SwordCombat>().normalBlockFromHeavyAttack = true;
    //                other.gameObject.GetComponent<SwordCombat>().isEncounter = true;
    //                other.GetComponent<PlayerStats>().health -= 20;
    //                other.GetComponent<PlayerStats>().stamina -= 20;
    //            }
    //        }
    //        else// no block action
    //        {
    //            if (enemyBehaviour.causeDMGTime >= 0f && enemyBehaviour.causeDMGTime <= 2f)
    //            {
    //                if (enemyBehaviour.canCauseDmgByHeavyATK == true)
    //                {
    //                    enemyBehaviour.canCauseDmgByHeavyATK = false;
    //                    playerHeavyAtkKnockBackTime = setPlayerHeavyAtkKnockBackTime();
    //                    player = other.gameObject;
    //                    other.gameObject.GetComponent<SwordCombat>().normalBlockFromHeavyAttack = true;
    //                    other.gameObject.GetComponent<SwordCombat>().isEncounter = true;
    //                    other.GetComponent<PlayerStats>().health -= 50;
    //                    other.GetComponent<PlayerStats>().stamina -= 50;
    //                }
    //            }
    //        }
    //    }




    //}

    private void OnCollisionEnter(Collision collision)
    {
        isWeaponEnter = true;
        //Debug.Log("message");
        if (collision.gameObject.tag == "Player")
        {
            ContactPoint contact = collision.GetContact(0);
            int list = collision.GetContacts(collision.contacts);
            //if (enemyBehaviour.canCauseDmgByHeavyATK == true)
            //{
            //    enemyBehaviour.canCauseDmgByHeavyATK = false;
               //Debug.Log("collide times?");
            //}
        }
        if (collision.gameObject.tag == "PlayerWeapon")
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
            if (collision.gameObject.GetComponent<WeaponCollision>().playerAction.isKeepBlocking == true)
            {
                int playSoundNo = Random.Range(1, 4);
                switch (playSoundNo)
                {
                    case 1:
                        testPlayer.gameObject.GetComponent<SwordCombat>().light1.Play();
                        break;
                    case 2:
                        testPlayer.gameObject.GetComponent<SwordCombat>().light2.Play();
                        break;
                    case 3:
                        testPlayer.gameObject.GetComponent<SwordCombat>().light3.Play();
                        break;
                    case 4:
                        testPlayer.gameObject.GetComponent<SwordCombat>().light4.Play();
                        break;
                }
            }
            #endregion
            if(enemyAction.isHitPlayer == false && testPlayer.GetComponent<PlayerAction>().isPerfectBlock == false)
            {
                enemyAction.isHitPlayer = true;
            }
        }
    }

}
