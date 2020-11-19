using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _anim;

    private GameObject testPlayer;
    public float playerDistance;
    EnemyAction enemyAction;

    void Start()
    {
        _anim = GetComponent<Animator>();
        testPlayer = GameObject.Find("Player");
        enemyAction = this.GetComponent<EnemyAction>();
    }

    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(this.transform.position, testPlayer.transform.position);
        if (playerDistance < 5)
        {
            this.transform.LookAt(new Vector3(testPlayer.transform.position.x, this.transform.position.y, testPlayer.transform.position.z)); // only rotate y axis
            
        }
    } 


    //void doHeavyAttack(GameObject pl)
    //{
    //    if (beforeHAtkTime > 0 && beforeDoATK == false) // before start doing action
    //    {
    //        beforeHAtkTime -= Time.fixedDeltaTime;
    //    }
    //    if (beforeHAtkTime <= 0 && beforeDoATK == false) //initialise attack data
    //    {
    //        duringHAtkTime = 2.0f;
    //        afterHAtkTime = 2.0f; // set cooldown time for next attack
    //        causeDMGTime = 2.0f;

    //        beforeDoATK = true;
    //        testPlayer.GetComponent<SwordCombat>().danger.Play(); // play warning sound
    //        _anim.SetTrigger("Heavy Attack");

    //        canCauseDmgByHeavyATK = true;
    //    }

    //    #region cause damge logic
    //    if (causeDMGTime >= 0f)
    //    {
    //        causeDMGTime -= Time.fixedDeltaTime;
    //    }
    //    if (causeDMGTime >= 0f && causeDMGTime <= 2.0f)
    //    {
    //        //Debug.Log("is it time to cause dmg");
    //    }
    //    if (causeDMGTime <= 0f || duringDoATK == true)
    //    {
    //        canCauseDmgByHeavyATK = false;
    //    }
    //    #endregion

    //    if (beforeDoATK == true && duringDoATK == false) // doing the Action
    //    {
    //        if (duringHAtkTime > 0 && duringDoATK == false) // doing attack action
    //        {
    //            duringHAtkTime -= Time.fixedDeltaTime;
    //        }
    //        if (duringHAtkTime <= 0 && duringDoATK == false)
    //        {
    //            duringDoATK = true;
    //        }
    //    }

    //    if (duringDoATK == true && afterDoATK == false) // delay time for next action
    //    {
    //        if (afterHAtkTime > 0 && afterDoATK == false)
    //        {
    //            afterHAtkTime -= Time.fixedDeltaTime;
    //        }
    //        if (afterHAtkTime <= 0 && afterDoATK == false)
    //        {
    //            afterDoATK = true;
    //        }
    //    }

    //    if (afterDoATK == true) //reset all values
    //    {
    //        beforeHAtkTime = 0.1f;
    //        duringHAtkTime = 2.0f;
    //        afterHAtkTime = 2.0f; // set cooldown time for next attack
    //        beforeDoATK = false;
    //        duringDoATK = false;
    //        afterDoATK = false;
    //    }
    //}
 
}
