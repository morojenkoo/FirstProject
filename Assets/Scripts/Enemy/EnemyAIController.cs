using FirstProject.Movement;
using FirstProject.States;
using UnityEngine;
namespace FirstProject.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;
        [SerializeField]
        private EnemyCharacter _character;
        [SerializeField] 
        private float _runAwayAcceleration = 1.5f;
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius, _character);
            _stateMachine = new EnemyStateMachine(_target, navMesher, enemyDirectionController, _character);
        }
        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
            if (_stateMachine.GetCurrentState() is RunAwayState)
            {
                _character._characterMovementController.CurrentSpeed = _character._characterMovementController.DefaultSpeed * _runAwayAcceleration;
            }
        }
    }
}