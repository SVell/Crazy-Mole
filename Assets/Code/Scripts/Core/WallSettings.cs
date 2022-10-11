using UnityEngine;

namespace SVell 
{
	[CreateAssetMenu]
	public class WallSettings : ScriptableObject
	{
		[Header("Bounce")] 
		[SerializeField] private float moveLength = 0.25f;
		[SerializeField] private float moveInDuration = 0.1f;
		[SerializeField] private float moveOutDuration = 1f;

		[Header("Player Interactions")] 
		[SerializeField] private float bounceForce;

		public float MoveLength => moveLength;
		public float MoveInDuration => moveInDuration;
		public float MoveOutDuration => moveOutDuration;
		public float BounceForce => bounceForce;
	}
}
