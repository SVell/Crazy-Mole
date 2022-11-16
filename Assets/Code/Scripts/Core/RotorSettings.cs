using UnityEngine;

namespace SVell 
{
	[CreateAssetMenu]
	public class RotorSettings : ScriptableObject
	{
		[SerializeField] private float rotationSpeed;

		public float RotationSpeed => rotationSpeed;
	}
}
