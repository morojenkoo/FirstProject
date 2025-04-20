using FirstProject.Movement;
using UnityEngine;

namespace FirstProject 
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        public static bool isPlayerAlive = false;
        private Coroutine _speedBoostCoroutine;
        
        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterMovementController._isSpaceBoosting = true;
                _characterMovementController.UpdateSpeedSpace();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _characterMovementController._isSpaceBoosting = false;
                _characterMovementController.UpdateSpeedSpace();
            }
        }

        public override void Death()
        {
            base.Death();
            isPlayerAlive = false;
        }
    }
}