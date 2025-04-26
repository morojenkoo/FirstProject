using UnityEngine;
using System;
using Unity.VisualScripting;

namespace FirstProject.Camera
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Vector3 _followCameraOffset = Vector3.zero;
		[SerializeField] private Vector3 _rotationOffset = Vector3.zero;
		private Transform _playerTransform;
		private CharacterSpawner _spawner;
		protected void Awake()
		{
			_spawner = FindObjectOfType<CharacterSpawner>();
		}
		protected void Start()
		{
			_spawner.OnCharacterSpawned += HandleCharacterSpawned;
			if (_spawner.CurrentCharacter is PlayerCharacter)
			{
				HandleCharacterSpawned(_spawner.CurrentCharacter.GameObject());
			}
		}
		private void HandleCharacterSpawned(GameObject player)
		{
			_playerTransform = player.transform;
		}
		// Update is called once per frame
		protected void LateUpdate()
		{
			if (_playerTransform != null)
			{
				Vector3 targetRotation = _rotationOffset - _followCameraOffset;
				transform.position = _playerTransform.transform.position + _followCameraOffset;
				transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
			}
		}
		protected void OnDestroy()
		{
			// Отписываемся от события при уничтожении
			if (_spawner != null)
			{
				_spawner.OnCharacterSpawned -= HandleCharacterSpawned;
			}
		}
	}
}