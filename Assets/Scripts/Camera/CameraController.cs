using UnityEngine;
using System;
namespace FirstProject.Camera
{
	public class CameraController : MonoBehaviour
	{
        [SerializeField]
        private Vector3 _followCameraOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;
        [SerializeField]
		private PlayerCharacter _player;
		// Use this for initialization
		protected void Awake()
		{
			if ( _player == null )
				throw new Exception($"Camera can't follow null player - {nameof(_player)}");
		}

		// Update is called once per frame
		protected void LateUpdate()
		{
			Vector3 targetRotation = _rotationOffset - _followCameraOffset;
			transform.position = _player.transform.position + _followCameraOffset;
			transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
		}
	}
}