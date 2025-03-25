using FirstProject.Movement;
using UnityEngine;
using System.Collections;

namespace FirstProject 
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        [SerializeField]
        private int _acceleration = 2;
        private Coroutine _speedBoostCoroutine;
        
        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
                SpeedUp(true);
            if (Input.GetKeyUp(KeyCode.Space))
                SpeedUp(false);
        }
        public void ActivateTemporarySpeedBoost(float acceleration, float duration) //Метод для ускорения с таймером
        {
            if (_speedBoostCoroutine != null)
            {
                StopCoroutine(_speedBoostCoroutine);
            }
            _characterMovementController.CurrentSpeed *= acceleration;
            _speedBoostCoroutine = StartCoroutine(SpeedBoostTimer(duration));
        }

        private IEnumerator SpeedBoostTimer(float duration) //Таймер ускорения
        {
            yield return new WaitForSeconds(duration);
            _characterMovementController.CurrentSpeed = _characterMovementController.DefaultSpeed;
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