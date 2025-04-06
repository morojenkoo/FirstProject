using UnityEngine;
using FirstProject.FSM;
namespace FirstProject.States
{
    public class RunAwayState : BaseState
    {
        BaseCharacter _character;
        private readonly EnemyTarget _target;
        private Vector3 _currentPoint;
        private readonly NavMesher _navMesher;
        private readonly EnemyDirectionController _enemyDirectionController;
        public RunAwayState(EnemyTarget target, NavMesher navMesher, EnemyDirectionController enemyDirectionController)
        {
            _target = target;
            _navMesher = navMesher;
            _enemyDirectionController = enemyDirectionController;
        }
        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
            _currentPoint = -targetPosition;
            var currentPoint = _navMesher.GetCurrentPoint();
            if (_currentPoint != currentPoint)
            {
                _currentPoint = currentPoint;
                _enemyDirectionController.UpdateMovementDirection(currentPoint);
            }
        }
    }
}