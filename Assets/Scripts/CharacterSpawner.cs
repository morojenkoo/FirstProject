using System;
using Unity.VisualScripting;
using UnityEngine;

namespace FirstProject
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _playerPrefab;
        [SerializeField] private EnemyCharacter _enemyPrefab;
        private static int _maxEnemyCount = 2;
        public BaseCharacter CurrentCharacter { get; private set; }
        public event Action<GameObject> OnCharacterSpawned;
        private static float _currentSpawnTimerSeconds;
        private const float _spawnInterval = 5;
        private void Awake()
        {
            SpawnPlayer();
        }

        private void Update()
        {
            if (!PlayerCharacter.IsPlayerAlive)
                SpawnPlayer();
            
            if (EnemyCharacter.СurrentEnemyCount < _maxEnemyCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;
                if (_currentSpawnTimerSeconds > _spawnInterval)
                {
                    _currentSpawnTimerSeconds = 0f;
                    SpawnEnemy();
                }
            }
        }

        public void SpawnPlayer()
        {
            if (PlayerCharacter.IsPlayerAlive)
                return;
            var spawnPosition = new Vector3(0, 1, 0) + transform.position;
            CurrentCharacter = Instantiate(_playerPrefab, spawnPosition, Quaternion.identity);
            PlayerCharacter.IsPlayerAlive = true;
            OnCharacterSpawned?.Invoke(CurrentCharacter.GameObject());
        }

        public void SpawnEnemy()
        {
            var spawnPosition = new Vector3(0, 1, 0) + transform.position;
            CurrentCharacter = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            EnemyCharacter.СurrentEnemyCount++;
       }
        private void OnDrawGizmos()
        {
            var cashedColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, 2f);
            UnityEditor.Handles.color = cashedColor;
        }
    }
}