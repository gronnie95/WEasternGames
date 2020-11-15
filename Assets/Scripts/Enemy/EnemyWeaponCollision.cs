using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollision : MonoBehaviour
{
    public float playerLightAtkKnockBackTime = 0.1f;
    public float playerHeavyAtkKnockBackTime = 0.1f;

    EnemyBehaviour enemyBehaviour;
    public GameObject enemy;
    private GameObject player;
    bool isPlayerDoBlock = false;
    bool isPerfectBlock = false;

    private GameObject testPlayer;
    

    void Start()
    {
        enemy = GameObject.Find("Enemy");
        testPlayer = GameObject.Find("Player");
        enemyBehaviour = this.enemy.GetComponent<EnemyBehaviour>();
    }

    void Update()
    {
        if (player != null)
        {
            HeavyAtkKnockBackEnemy(player);
            playNormalBlockSound();
        }
        Debug.Log(enemyBehaviour.causeDMGTime);
    }

    void playNormalBlockSound()
    {
        int playSoundNo = Random.Range(1, 4);
        if(isPlayerDoBlock == true && isPerfectBlock == false)
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
        isPlayerDoBlock = false;
        isPerfectBlock = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Random ran = new Random();
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(enemyBehaviour.causeDMGTime);

            //if (other.gameObject.GetComponent<PlayerAction>().PlayerStatus == (int)ActionType.InstantBlock)
            //{
            //    isPlayerDoBlock = true;
            //    if (enemyBehaviour.canCauseDmgByHeavyATK == true)
            //    {
            //        enemyBehaviour.canCauseDmgByHeavyATK = false;
            //        playerHeavyAtkKnockBackTime = setPlayerHeavyAtkKnockBackTime();
            //        player = other.gameObject;

            //        #region play sound and calculate dmg/hp

            //if (causeDMGTime >= 0.6f && causeDMGTime <= 1.2f) //perfect block
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

            //        if(enemyBehaviour.causeDMGTime > 1.2f && enemyBehaviour.causeDMGTime < 0.6f)
            //        {
            //            other.gameObject.GetComponent<SwordCombat>().normalBlockFromHeavyAttack = true;
            //            other.gameObject.GetComponent<SwordCombat>().isEncounter = true;
            //            other.GetComponent<PlayerStats>().health -= 30;
            //            other.GetComponent<PlayerStats>().stamina -= 30;
            //        }
            //        #endregion
            //    }
            //}

            if (other.gameObject.GetComponent<PlayerAction>().PlayerStatus == (int)ActionType.LongBlock) // long block
            {
                if (enemyBehaviour.canCauseDmgByHeavyATK == true)
                {
                    isPlayerDoBlock = true;
                    enemyBehaviour.canCauseDmgByHeavyATK = false;
                    playerHeavyAtkKnockBackTime = setPlayerHeavyAtkKnockBackTime();
                    player = other.gameObject;

                    other.gameObject.GetComponent<SwordCombat>().normalBlockFromHeavyAttack = true;
                    other.gameObject.GetComponent<SwordCombat>().isEncounter = true;
                    other.GetComponent<PlayerStats>().health -= 20;
                    other.GetComponent<PlayerStats>().stamina -= 20;
                }
            }
            //else// no block action
            //{
            //    if (enemyBehaviour.causeDMGTime >= 0f && enemyBehaviour.causeDMGTime <= 2f)
            //    {
            //        if (enemyBehaviour.canCauseDmgByHeavyATK == true)
            //        {
            //            enemyBehaviour.canCauseDmgByHeavyATK = false;
            //            playerHeavyAtkKnockBackTime = setPlayerHeavyAtkKnockBackTime();
            //            player = other.gameObject;
            //            other.gameObject.GetComponent<SwordCombat>().normalBlockFromHeavyAttack = true;
            //            other.gameObject.GetComponent<SwordCombat>().isEncounter = true;
            //            other.GetComponent<PlayerStats>().health -= 50;
            //            other.GetComponent<PlayerStats>().stamina -= 50;
            //        }
            //    }
            //}
        }




    }

    private void HeavyAtkKnockBackEnemy(GameObject targetEnemy)
    {
        
    }

    float setPlayerHeavyAtkKnockBackTime()
    {
        return 0.1f;
    }
}
