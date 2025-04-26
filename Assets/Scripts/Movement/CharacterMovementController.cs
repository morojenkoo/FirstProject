using System.Collections;
using UnityEngine;
namespace FirstProject.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMovementController : MonoBehaviour
	{
		private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon; 
		private CharacterController _characterController;
		public float CurrentSpeed = 5f;
		[SerializeField]
		private float _maxRadians = 10f;
		[SerializeField]
		private float _acceleration = 2f;
		public float DefaultSpeed = 5f;
		public float MaxSpeed = 10f;
		private Coroutine _speedBoostCoroutine;
		public bool IsSpaceBoosting;
		public bool IsBonusBoosting;
		public Vector3 MovementDirection { get; set; }
		public Vector3 LookDirection { get; set; }
		protected void Awake()
		{
			_characterController = GetComponent<CharacterController>();
		}
		protected void Update()
		{
			Translate();

			if (_maxRadians > 0f && LookDirection != Vector3.zero)
				Rotate();
		}
		private void Translate()
		{
			var delta = MovementDirection * (CurrentSpeed * Time.deltaTime);
			_characterController.Move(delta);
		}
		private void Rotate()
		{
			var currentLookDirection = transform.rotation * Vector3.forward;
			float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;
			if (sqrMagnitude > SqrEpsilon)
			{
				var newRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookDirection, Vector3.up), 
					_maxRadians * Time.deltaTime);
				transform.rotation = newRotation;
			}
		}
		public void ActivateTemporarySpeedBoost(float acceleration, float duration)
		{
			if (_speedBoostCoroutine != null)
			{
				StopCoroutine(_speedBoostCoroutine);
			}
			IsBonusBoosting = true;
			UpdateSpeedPickUp(acceleration);
            
			_speedBoostCoroutine = StartCoroutine(SpeedBoostTimer(acceleration, duration));
		}
		private IEnumerator SpeedBoostTimer(float acceleration, float duration)
		{
			yield return new WaitForSeconds(duration);
			IsBonusBoosting = false;
			UpdateSpeedPickUp(acceleration);
		}

		public void UpdateSpeedPickUp(float acceleration)
		{
			float targetSpeed = DefaultSpeed;
			if (IsBonusBoosting)
				targetSpeed = Mathf.Min(targetSpeed * acceleration, MaxSpeed);
			CurrentSpeed = targetSpeed;
		}
		public void UpdateSpeedSpace()
		{
			float targetSpeed = DefaultSpeed;
			if (IsSpaceBoosting || IsBonusBoosting)
				targetSpeed = Mathf.Min(targetSpeed * _acceleration, MaxSpeed);
			CurrentSpeed = targetSpeed;
		}
	}
}