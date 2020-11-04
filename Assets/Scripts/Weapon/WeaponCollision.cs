using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public bool isLightHit = false;
    public bool isHeavyHit = false;
    public float enemyLightAtkKnockBackTime = 0.2f;
    public float enemyHeavyAtkKnockBackTime = 0.4f;

    void Update()
    {
        if(isLightHit == true)
        {
            LightKnockBackEnemy(gameObject);
        }
        if (isHeavyHit == true)
        {
            HeavyKnockBackEnemy(gameObject);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit Enemy");  
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Enemy")
        {
            if(PlayerBehaviour.isLightHit == true)
            {
                isLightHit = true;
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(-other.transform.forward.normalized), 1f);
            }

            if (PlayerBehaviour.isHeavyHit == true)
            {
                isHeavyHit = true;
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(-other.transform.forward.normalized), 1f);
            }

            Debug.Log(other.ToString());
            Debug.Log("Hit Enemy");
            
        }
        
    }

    void LightKnockBackEnemy(GameObject enemy)
    {
        float Velocity = 0.5f;
        if (enemyLightAtkKnockBackTime > 0 && isLightHit == true)
        {
            enemyLightAtkKnockBackTime -= Time.deltaTime;
            Vector3 knockBackVector = -enemy.transform.forward * Velocity;
            enemy.GetComponent<Enemy>().enemyController.Move(knockBackVector);
        }

        if(enemyLightAtkKnockBackTime <= 0)
        {
            enemyLightAtkKnockBackTime = 0.2f;
            isLightHit = false;
        }
    }

    void HeavyKnockBackEnemy(GameObject enemy)
    {
        float Velocity = 1.0f;
        if (enemyHeavyAtkKnockBackTime > 0 && isHeavyHit == true)
        {
            enemyHeavyAtkKnockBackTime -= Time.deltaTime;
            Vector3 knockBackVector = -enemy.transform.forward * Velocity;
            enemy.GetComponent<Enemy>().enemyController.Move(knockBackVector);
        }

        if (enemyHeavyAtkKnockBackTime <= 0)
        {
            enemyHeavyAtkKnockBackTime = 0.4f;
            isHeavyHit = false;
        }
    }
}

