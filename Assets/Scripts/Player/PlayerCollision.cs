using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Player Get Enemy Hit

        // player is blocking and get hit by enemy
        if (collision.gameObject.tag == "EnemyWeapon" && player.GetComponent<PlayerAction>().isKeepBlocking == true && player.GetComponent<PlayerAction>().isPerfectBlock == false 
            && player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("B")) 
        {
            player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetBlockingImpact");

        }

        // player is blocking and get hit by enemy
        else if (collision.gameObject.tag == "EnemyWeapon" && player.GetComponent<PlayerAction>().isKeepBlocking == true && player.GetComponent<PlayerAction>().isPerfectBlock == false
            && player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("BI")) 
        {
            player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isGetBlockingImpact");

        }

        // player is in idle action and get hit by enemy
        else if (collision.gameObject.tag == "EnemyWeapon" && player.GetComponent<PlayerAction>().isKeepBlocking == false) 
        {
            player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isInjured");
            //Debug.Log("player injured");
        }

        // player is in perfect block Transistion but not in perfect block timing
        // (GetCurrentAnimatorStateInfo(0).IsTag("PB")) get current animator state by tag https://forum.unity.com/threads/current-animator-state-name.331803/
        if (collision.gameObject.tag == "EnemyWeapon" && (player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("PB") ||
            player.GetComponent<PlayerAnimation>()._anim.GetCurrentAnimatorStateInfo(0).IsTag("A")) && player.GetComponent<PlayerAction>().isPerfectBlock == false) 
        {
           // Debug.Log("aaa");
            player.GetComponent<PlayerAnimation>()._anim.SetTrigger("isInjured");
        }
        #endregion
    }
}
