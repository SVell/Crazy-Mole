using UnityEngine;

namespace SVell 
{
	public class Arc : MonoBehaviour
	{
		private void OnCollisionExit(Collision other)
		{
			if (other.transform.CompareTag("Player"))
			{
				float magnitude = other.rigidbody.velocity.magnitude;
				other.rigidbody.AddForce(other.rigidbody.velocity.normalized * magnitude / 5, ForceMode.Impulse);
			}
		}
	}
}
