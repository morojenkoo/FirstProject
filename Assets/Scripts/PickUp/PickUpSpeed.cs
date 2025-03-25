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
            if (character is PlayerCharacter player)
                player.ActivateTemporarySpeedBoost(_acceleration, _duration);
            else
                character._characterMovementController.CurrentSpeed *= _acceleration;
        }
    }
}