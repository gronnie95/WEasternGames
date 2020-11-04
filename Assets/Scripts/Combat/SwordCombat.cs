using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCombat : MonoBehaviour
{
   
    
    public AudioSource danger;
    public AudioSource light1;
    public AudioSource light2;
    public AudioSource light3;
    public AudioSource light4;
    public AudioSource heavy1;
    public AudioSource heavy2;
    
    public static bool isEnemyAttack = false;
    bool isPlayerBlock = false;
    private float enemyAttackTimer = 0.0f;
    private float enemyAttackCoolDown = 5.0f;
    private bool isAttackSoundPlaying = false;
    private float blockBar = 0;
    private Vector3 camDirection;
    private GameObject player;
    private GameObject capsule;
    private GameObject[] enemys;
    private GameObject playerCamera;
    private float knockBackTime = 0.0f;
    private float knockBackForce = 0.5f;
    private float Velocity;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        capsule = GameObject.Find("Capsule");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        playerCamera = GameObject.Find("PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {
       DetectAttack();
        //Debug.Log(enemyAttackTimer);
    }

    void DetectAttack()
    {
        foreach(GameObject dummy in enemys)
        {
            float EnemyDistance;
            EnemyDistance = Vector3.Distance(player.transform.position, dummy.transform.position);
            //Debug.Log(EnemyDistance);
            if (EnemyDistance < 3 && isEnemyAttack == false)
            {
                danger.Play();
                isEnemyAttack = true;
            }
            if (isEnemyAttack == true && enemyAttackTimer <= enemyAttackCoolDown)
            {
                enemyAttackTimer += Time.deltaTime;
            }
            if (!danger.isPlaying && isEnemyAttack == true && enemyAttackTimer >= enemyAttackCoolDown)
            {
                enemyAttackTimer = 0;
                isPlayerBlock = false;
                isEnemyAttack = false;
            }
            Attack(EnemyDistance);
            KnockBackPlayer(EnemyDistance);
        }
 
    }

    void Attack(float EnemyDistance)
    {
        #region Play Sound
        if(!light1.isPlaying && !light2.isPlaying && !light3.isPlaying && !light4.isPlaying && !heavy1.isPlaying && !heavy2.isPlaying)
        {
            isAttackSoundPlaying = false;
        }

        if(enemyAttackTimer >= 0.8f && enemyAttackTimer <= 1.2f && Input.GetMouseButtonDown(1) && isAttackSoundPlaying == false && EnemyDistance < 3 && isPlayerBlock == false)
        {
            isAttackSoundPlaying = true;
            isPlayerBlock = true;
            heavy1.Play();
            Debug.Log("Block Bar: " + blockBar);
        }
        else if(enemyAttackTimer > 1.2f && enemyAttackTimer <= 1.5f && Input.GetMouseButtonDown(1) && isAttackSoundPlaying == false && EnemyDistance < 3 && isPlayerBlock == false)
        {
            isAttackSoundPlaying = true;
            isPlayerBlock = true;
            heavy2.Play();
            Debug.Log("Block Bar: " + blockBar);
        }
        else if((enemyAttackTimer < 0.8f || enemyAttackTimer >1.5f) && Input.GetMouseButtonDown(1) && isAttackSoundPlaying == false && EnemyDistance < 3 && isPlayerBlock == false)
        {
            isAttackSoundPlaying = true;
            isPlayerBlock = true;
            Random ran = new Random();
            int playSoundNo = Random.Range(1, 4);
            if(playSoundNo == 1)
            {
                light1.Play();
                blockBar += 20;
                Debug.Log("Block Bar: " + blockBar);
            }
            else if(playSoundNo == 2)
            {
                light2.Play();
                blockBar += 20;
                Debug.Log("Block Bar: " + blockBar);
            }
            else if (playSoundNo == 3)
            {
                light3.Play();
                blockBar += 20;
                Debug.Log("Block Bar: " + blockBar);
            }
            else if (playSoundNo == 4)
            {
                light4.Play();
                blockBar += 20;
                Debug.Log("Block Bar: " + blockBar);
            }
        }

        #endregion
    }

    void KnockBackPlayer(float EnemyDistance)
    {
        if(blockBar >= 20)
        {
            player.GetComponent<PlayerMovement>().isOnKnockBack = true;
            blockBar = 0;
            knockBackTime = 0.4f;
        }
        if(player.GetComponent<PlayerMovement>().isOnKnockBack == true && knockBackTime >= 0)
        {
            knockBackTime -= Time.deltaTime;
            Velocity = knockBackForce;
            Vector3 knockBackVector = -player.transform.forward * knockBackForce;
            player.GetComponent<PlayerMovement>().myController.Move(knockBackVector);
        }
        if(knockBackTime <= 0)
        {
            player.GetComponent<PlayerMovement>().isOnKnockBack = false;
        }
    }


}