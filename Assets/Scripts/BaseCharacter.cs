using System.Collections;
using FirstProject.Movement;
using FirstProject.PickUp;
using FirstProject.Shooting;

using UnityEngine;
namespace FirstProject {
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        private Weapon _baseWeaponPrefab;
        [SerializeField]
        private Transform _hand;
        [SerializeField]
        private float _health = 100f;
        [SerializeField]
        private Animator _animator;
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
            _animator.SetBool("IsMoving", direction != Vector3.zero);
            _animator.SetBool("IsShooting", _shootingController.HasTarget);
            if (_health <= 0f)
                Death();
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

        public virtual void Death()
        {
            StartCoroutine(DeathCoroutine());
        }
        public virtual IEnumerator DeathCoroutine()
        {   
            _animator.SetTrigger("IsDead");
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            float deathAnimationLength = stateInfo.length;
            yield return new WaitForSeconds(deathAnimationLength);
            Destroy(gameObject);
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