﻿using UnityEngine;
namespace FirstProject.Shooting
{
    
    public class ShootingController : MonoBehaviour
    {
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => _target.transform.position;
        private Weapon _weapon;
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
                    _weapon.Shoot(TargetPosition);
                _nextShotTimerSec = _weapon.ShootFrequencySec;
            }
        }

        public void SetWeapon(Weapon weaponPrefab, Transform hand)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);
            _weapon = Instantiate(weaponPrefab, hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }

        private GameObject GetTarget()
        {
            GameObject target = null;
            var position = _weapon.transform.position;
            var radius = _weapon.ShootRadius;
            var EnemyMask = LayerUtils.EnemyMask;
            var PlayerMask = LayerUtils.PlayerMask;
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