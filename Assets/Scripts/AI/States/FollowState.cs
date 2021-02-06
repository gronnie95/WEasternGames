using UnityEngine;

namespace AI.States
{
    public class FollowState : State
    {
        private FieldOfView _fieldOfView;
        private Animator _animator;
        private float _moveSpeed;

        #region Animation Triggers
        
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        private float _zVel;
        private int _zVelHash;

        #endregion


        public FollowState(GameObject go, StateMachine sm) : base(go, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _fieldOfView = _go.GetComponent<FieldOfView>();
           _animator = _go.GetComponent<Animator>();
            if(!_animator.GetBool(IsRunning))
                _animator.SetBool(IsRunning, true);
            _moveSpeed = 8f;
            _zVelHash = Animator.StringToHash("enemyVelZ");
        }

        public override void FixedUpdate()
        {
           base.FixedUpdate();

           float distanceToPlayer = Vector3.Distance(_go.transform.position, _fieldOfView.Player.transform.position);
          
           Vector3 moveDirection = (_fieldOfView.Player.transform.position - _go.transform.position).normalized;
               
           
           if (_fieldOfView.Player != null && distanceToPlayer >= 1.5)
           {
               _zVel = 2;
               _go.transform.LookAt(_fieldOfView.Player.transform.position);
               _go.transform.position += _go.transform.forward * _moveSpeed * Time.deltaTime;
               _animator.SetFloat(_zVelHash, _zVel);
           }

           if (distanceToPlayer < 1.5)
           {
               _zVel = 0;
               _animator.SetFloat(_zVelHash, _zVel);
               _animator.SetBool(IsRunning, false);
               _sm._CurState = new AttackingState(_go, _sm);
           }
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}