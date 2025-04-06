using UnityEngine;

namespace FirstProject.PickUp
{
    public class PickUpSpeed : PickUpItem
    {
        [SerializeField] 
        private float _acceleration = 2f;
        [SerializeField]
        private float _duration = 5f;
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character._characterMovementController.ActivateTemporarySpeedBoost(_acceleration, _duration);
        }
    }
}