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
        enemy = this.transform.root.Find("EnemyHolder/Enemy").gameObject;
        testPlayer = GameObject.Find("Player");
        enemyAction = this.enemy.GetComponent<EnemyAction>();
    }

    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            if (collision.transform.root.Find("Player").gameObject.GetComponent<PlayerAction>().isPerfectBlock == true && this.GetComponent<Collider>().isTrigger == false) //get player perfect block
            {
                enemy.GetComponent<EnemyAnimation>()._anim.SetTrigger("getPlayerPerfectBlockImpact");
            }
            this.GetComponent<Collider>().isTrigger = true;
        }
        if (collision.gameObject.tag == "Player")
        {
            //get player perfect block
            if (collision.gameObject.GetComponent<PlayerAction>().isPerfectBlock == true && this.GetComponent<Collider>().isTrigger == false) 
            {
                enemy.GetComponent<EnemyAnimation>()._anim.SetTrigger("getPlayerPerfectBlockImpact");
            }

            this.GetComponent<Collider>().isTrigger = true;
        }
    }

}
