using FirstProject.Movement;
using FirstProject.Shooting;
using UnityEngine;
namespace FirstProject {
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;
        [SerializeField]
        private Transform _hand;
        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            _shootingController.SetWeapon(_baseWeaponPrefab, _hand);
        }
        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            _characterMovementController.Direction = direction;
        }
    }
}