using FirstProject.Movement;
using FirstProject.PickUp;
using FirstProject.Shooting;

using UnityEngine;
namespace FirstProject {
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        public Weapon _baseWeaponPrefab;
        [SerializeField]
        private Transform _hand;
        [SerializeField]
        private float _health = 100f;
        private IMovementDirectionSource _movementDirectionSource;
        public CharacterMovementController _characterMovementController;
        public ShootingController _shootingController;
        protected void Awake()
        {
            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }
        protected virtual void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
            {
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            }
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;
            if (_health <= 0f)
                Destroy(gameObject);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.GetComponent<Bullet>();
                _health -= bullet.Damage;
                Destroy(other.gameObject);
            } else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.GetComponent<PickUpItem>();
                pickUp.PickUp(this);
                Destroy(other.gameObject);
            }
        }
        
        public float GetHealth()
        {
            return _health;
        }
        
        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
    }
}