using FirstProject.FSM;
using FirstProject.Movement;
using UnityEngine;

namespace FirstProject.States
{
    public class RunAwayState: BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private Vector3 _currentPoint;
        public RunAwayState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
        }
        public override void Execute()
        {
            Vector3 targetPosition = -_target.Closest.transform.position;
            if (_currentPoint != targetPosition)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(targetPosition);
            }
        }
    }
}