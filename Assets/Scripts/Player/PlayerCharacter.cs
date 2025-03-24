using FirstProject.Movement;
using UnityEngine;

namespace FirstProject {
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        private const int _acceleration = 2;
        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
                SpeedUp(true);
            if (Input.GetKeyUp(KeyCode.Space)) //Если отпустили - возвращаем изначальную скорость
                SpeedUp(false);

        }

        private void SpeedUp(bool isBoosting)
        {
            if (isBoosting)
                _characterMovementController.CurrentSpeed *= _acceleration;
            else
                _characterMovementController.CurrentSpeed = _characterMovementController.DefaultSpeed;
        }
    }
}