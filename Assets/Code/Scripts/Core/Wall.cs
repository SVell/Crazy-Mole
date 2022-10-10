using UnityEngine;

namespace SVell 
{
	public abstract class Wall : MonoBehaviour
	{
		protected abstract void OnPlayerCollide();

		private void OnCollisionEnter(Collision other)
		{
			if (other.transform.CompareTag("Player"))
			{
				OnPlayerCollide();
			}
		}
	}
}
