using UnityEngine;

namespace AI.States
{
    public class FollowState : State
    {
        private FieldOfView _fieldOfView;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private float moveSpeed = 8f;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        public FollowState(GameObject go, StateMachine sm) : base(go, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _fieldOfView = _go.GetComponent<FieldOfView>();
            _rigidbody = _go.GetComponent<Rigidbody>();
            _animator = _go.GetComponent<Animator>();
            if(!_animator.GetBool(IsRunning))
                _animator.SetBool(IsRunning, true);
        }

        public override void FixedUpdate()
        {
           base.FixedUpdate();

           float distanceToPlayer = Vector3.Distance(_go.transform.position, _fieldOfView.Player.transform.position);
          
           Vector3 moveDirection = (_fieldOfView.Player.transform.position - _go.transform.position).normalized;
               
           
           if (_fieldOfView.Player != null && distanceToPlayer >= 1.5)
           { 
               //_go.transform.forward = Vector3.RotateTowards(_go.transform.forward,_fieldOfView.Player.transform.position, moveSpeed* Time.deltaTime, 0.0f);
               _go.transform.LookAt(_fieldOfView.Player.transform.position);
               _go.transform.position += _go.transform.forward * moveSpeed * Time.deltaTime;
           }

           if (distanceToPlayer < 1.5)
           {
               _animator.SetBool(IsRunning, false);
               _rigidbody.velocity = Vector3.zero;
               _sm._CurState = new CombatState(_go, _sm);
           }
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}