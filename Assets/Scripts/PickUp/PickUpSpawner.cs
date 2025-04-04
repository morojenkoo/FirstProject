﻿using UnityEditor;
using UnityEngine;

namespace FirstProject.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;
        [SerializeField]
        private float _range = 2f;
        [SerializeField]
        private int _maxCount;
        private static float _minSpawnIntervalSeconds = 5f;
        private static float _maxSpawnIntervalSeconds = 10f;
        private float _currentSpawnTimerSeconds;
        private int _currentCount;
        private float randomTimerSeconds;

        private void Awake()
        {
            randomTimerSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }
        protected void Update()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;
                if (_currentSpawnTimerSeconds > randomTimerSeconds)
                {
                    randomTimerSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
                    _currentSpawnTimerSeconds = 0f;
                    _currentCount++;
                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0, randomPointInsideRange.y) + transform.position;
                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }
        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}