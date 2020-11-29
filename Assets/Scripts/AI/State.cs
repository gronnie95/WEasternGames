using UnityEngine;

namespace UnityTemplateProjects.AI
{
    public abstract class State
    {
        protected GameObject _go;
        protected StateMachine _sm;

        public State(GameObject go, StateMachine sm)
        {
            _go = go;
            _sm = sm;
        }

        //Called when the state is entered
        public virtual void Enter(){}
        //Like GameObject.Update()
        public virtual void Update(){}
        //Like GameObject.FixedUpdate()        
        public virtual void FixedUpdate(){}
        //Called when the state is exited
        public virtual void Exit(){}
    }
}