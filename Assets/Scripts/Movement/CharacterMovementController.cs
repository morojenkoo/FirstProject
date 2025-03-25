using UnityEngine;
namespace FirstProject.Movement
{
	[RequireComponent(typeof(CharacterController))]
	public class CharacterMovementController : MonoBehaviour
	{
		private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon; 
		private CharacterController _characterController;
		[SerializeField]
		public float CurrentSpeed = 5f;
		[SerializeField]
		private float _maxRadians = 10f;
		public float DefaultSpeed = 5f;
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
	}
}