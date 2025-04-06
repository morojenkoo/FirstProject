using FirstProject.States;
using UnityEngine;
namespace FirstProject.Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;
        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;
        private EnemyCharacter _character;
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var navMesher = new NavMesher(transform);
            _target = new EnemyTarget(transform, player, _viewRadius);
            _stateMachine = new EnemyStateMachine(_target, navMesher, enemyDirectionController, _character);
        }
        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}