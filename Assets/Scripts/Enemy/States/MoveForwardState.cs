﻿using FirstProject.FSM;
using UnityEngine;

namespace FirstProject.States
{
    public class MoveForwardState: BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private Vector3 _currentPoint;
        public MoveForwardState(EnemyTarget target, EnemyDirectionController enemyDirectionController)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
        }
        public override void Execute()
        {
            Vector3 targetPosition = _target.Closest.transform.position;
            if (_currentPoint != targetPosition)
            {
                _currentPoint = targetPosition;
                _enemyDirectionController.UpdateMovementDirection(targetPosition);
            }
        }
    }
}