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
        if (collision.gameObject.tag == "EnemyWeapon" && player.GetComponent<PlayerAction>().isKeepBlocking == true && player.GetComponent<PlayerAction>().isPerfectBlock == false)
        {
            player.GetComponent<PlayerAction>().isBlockingImpact = true;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            Debug.Log("get hit");
        }
    }
}
