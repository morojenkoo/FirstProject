using UnityEngine;
namespace FirstProject.Shooting
{
    
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;
        private Weapon Weapon;
        private float _nextShotTimerSec;
        private GameObject _target;
        private Collider[] _colliders = new Collider[2];
        protected void Update()
        {
            _target = GetTarget();
            _nextShotTimerSec -= Time.deltaTime;
            if (_nextShotTimerSec < 0)
            {
                if (HasTarget)
                    Weapon.Shoot(TargetPosition);
                _nextShotTimerSec = Weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if (Weapon != null)
                Destroy(Weapon.gameObject);
            Weapon = Instantiate(weaponPrefab, hand);
            Weapon.transform.localPosition = Vector3.zero;
            Weapon.transform.localRotation = Quaternion.identity;
        }

        public Weapon.WeaponType GetWeaponType()
        {
            return Weapon._weaponType;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;
            var position = Weapon.transform.position;
            var radius = Weapon.ShootRadius;
            var combinedMask = LayerUtils.EnemyMask | LayerUtils.PlayerMask; //побитовое или для объединения масок
            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, combinedMask); //в функцию передаём объединённую маску
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }
            return target;
        }
    }
}