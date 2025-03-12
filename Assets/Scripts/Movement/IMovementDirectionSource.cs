using UnityEngine;

namespace FirstProject.Movement
{
	public interface IMovementDirectionSource 
	{
		Vector3 MovementDirection { get; }
	}
}