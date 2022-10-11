using DG.Tweening;
using UnityEngine;

namespace SVell 
{
	public class Wall : MonoBehaviour
	{
		[SerializeField] private Transform wall;

		[Header("Settings")] 
		[SerializeField] private WallSettings settings;
		

		private bool CheckForPlayer(Transform target)
		{
			return target.CompareTag("Player");
		}

		private void OnPlayerEnter()
		{
			wall.DOMove(wall.position - wall.forward * settings.MoveLength, settings.MoveInDuration).OnComplete(() =>
			{
				wall.DOMove(wall.position + wall.forward * settings.MoveLength, settings.MoveOutDuration);
			});
		}

		private void OnPlayerExit(Rigidbody player)
		{
			player.AddForce(player.velocity.normalized * settings.BounceForce, ForceMode.Impulse);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (CheckForPlayer(collision.transform))
			{
				OnPlayerEnter();
			}
		}

		private void OnCollisionExit(Collision collision)
		{
			if (CheckForPlayer(collision.transform))
			{
				OnPlayerExit(collision.rigidbody);
			}
		}
	}
}
