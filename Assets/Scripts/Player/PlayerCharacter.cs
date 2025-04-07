using FirstProject.Movement;
using UnityEngine;

namespace FirstProject 
{
    [RequireComponent(typeof(PlayerMovementDirectionController))]
    public class PlayerCharacter : BaseCharacter
    {
        
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
        
        
    }
}