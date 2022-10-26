using UnityEngine;

namespace SVell 
{
	public class HoleColliderTrigger : MonoBehaviour
	{
		[SerializeField] private GameObject colliders;

		private void Start()
		{
			colliders.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.transform.CompareTag("Player"))
			{
				colliders.SetActive(true);
				Physics.IgnoreLayerCollision(7, 9);
				var player = other.GetComponent<Rigidbody>();
				player.constraints = RigidbodyConstraints.None;
			}
		}
	}
}
