using System.Collections;
using System.Collections.Generic;
using New_folder.Scripts.AIStateMachine;
using UnityEngine;
using UnityTemplateProjects.AI;

public class AIController : MonoBehaviour
{
    private StateMachine _sm;
    private CatchParticipant catchParticipant;

    private void Awake()
    {
        catchParticipant = GetComponent<CatchParticipant>();
        _sm = new StateMachine();
        _sm._CurState = new TauntingState(gameObject, _sm);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _sm._CurState.Update();
    }

    void FixedUpdate()
    {
        _sm._CurState.FixedUpdate();
    }
}
