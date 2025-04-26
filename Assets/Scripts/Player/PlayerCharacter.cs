using FirstProject.Movement;
using UnityEngine;

namespace FirstProject 
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        public static bool IsPlayerAlive = false;
        private Coroutine _speedBoostCoroutine;
        
        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterMovementController.IsSpaceBoosting = true;
                _characterMovementController.UpdateSpeedSpace();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _characterMovementController.IsSpaceBoosting = false;
                _characterMovementController.UpdateSpeedSpace();
            }
        }

        public override void Death()
        {
            base.Death();
            IsPlayerAlive = false;
        }
    }
}