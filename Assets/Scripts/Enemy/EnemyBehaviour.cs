using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Animator _anim;
    private bool beforeDoATK = false;
    private bool duringDoATK = false;
    private bool afterDoATK = false;

    public bool canCauseDmgByLightATK = false;
    public bool canCauseDmgByHeavyATK = false;
    public float causeDMGTime = 0;

    #region Heavy Attack
    public static bool isHeavyHit = false;
    float beforeHAtkTime = 0.1f;
    float duringHAtkTime = 1.0f;
    float afterHAtkTime = 2.0f; // set cooldown time for next attack
    #endregion

    private GameObject[] player;
    private GameObject testPlayer;
    public float playerDistance;
    EnemyAction enemyAction;

    void Start()
    {
        _anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
        testPlayer = GameObject.Find("Player");
        enemyAction = this.GetComponent<EnemyAction>();
    }

    void Update()
    {
        //Debug.Log(causeDMGTime);
        enemyAction.action = (int)ActionType.HeavyAttack;
        foreach (GameObject pl in player) //if multiplayer
        {
            playerDistance = Vector3.Distance(this.transform.position, pl.transform.position);
            if(playerDistance < 5 && enemyAction.action == (int)ActionType.HeavyAttack)
            {
                this.transform.LookAt(new Vector3(pl.transform.position.x, this.transform.position.y, pl.transform.position.z)); // only rotate y axis
                doHeavyAttack(pl);
            }
        }
    }

    void doHeavyAttack(GameObject pl)
    {
        if (beforeHAtkTime > 0 && beforeDoATK == false) // before do Action
        {
            beforeHAtkTime -= Time.deltaTime;
        }
        if (beforeHAtkTime <= 0 && beforeDoATK == false) //check before do atk action is finished
        {
            beforeDoATK = true;
            causeDMGTime = 2.0f;
            testPlayer.GetComponent<SwordCombat>().danger.Play(); // play warning sound
        }

        #region cause damge logic
        if (causeDMGTime >= 0f)
        {
            causeDMGTime -= Time.deltaTime;
        }
        if (causeDMGTime >= 0f && causeDMGTime <= 1.5f)
        {
            canCauseDmgByHeavyATK = true;
            //Debug.Log("is it time to cause dmg");
        }
        if (causeDMGTime <= 0f)
        {
            canCauseDmgByHeavyATK = false;
        }
        #endregion

        if (beforeDoATK == true && duringDoATK == false) // do Action
        {
            if (duringHAtkTime > 0 && duringDoATK == false) // doing attack action
            {

                if (duringHAtkTime >= 1.0f)
                {
                    isHeavyHit = true;
                    _anim.SetTrigger("Heavy Attack");
                }

                duringHAtkTime -= Time.deltaTime;
            }
            if (duringHAtkTime <= 0 && duringDoATK == false)
            {
                duringDoATK = true;
            }
        }

        if (duringDoATK == true && afterDoATK == false) // finished one loop of action
        {
            if (afterHAtkTime > 0 && afterDoATK == false)
            {
                afterHAtkTime -= Time.deltaTime;
            }
            if (afterHAtkTime <= 0 && afterDoATK == false)
            {
                afterDoATK = true;
            }
        }

        if (afterDoATK == true) //reset all values
        {
            beforeHAtkTime = 0.1f;
            duringHAtkTime = 1.0f;
            afterHAtkTime = 2.0f; // set cooldown time for next attack
            beforeDoATK = false;
            duringDoATK = false;
            afterDoATK = false;
            isHeavyHit = false;
        }
    }
 
}
