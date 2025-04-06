using UnityEngine;
using FirstProject.FSM;
namespace FirstProject.States
{
    public class RunAwayState : BaseState
    {
        BaseCharacter _character;
        private const float MaxDistance = 3f;
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
            Vector3 targetPosition = -_target.Closest.transform.position;
            if (!_navMesher.IsPathCalculated || _navMesher.DistanceToTargetPointFrom(targetPosition) > MaxDistance)
                _navMesher.CalculatePath(targetPosition);
            var currentPoint = _navMesher.GetCurrentPoint();
            if (_currentPoint != currentPoint)
            {
                _currentPoint = currentPoint;
                _enemyDirectionController.UpdateMovementDirection(currentPoint);
            }
        }
    }
}