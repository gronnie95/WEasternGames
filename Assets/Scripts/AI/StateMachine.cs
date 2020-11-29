namespace UnityTemplateProjects.AI
{
    public class StateMachine
    {
        private State _curState;

        public State _CurState
        {
            get => _curState;
            
            set
            {
                //Protection in case a state already exists
                _curState?.Exit();

                _curState = value;

                //Sets up the new state
                _curState?.Enter();
            }
        }
        
    }
}