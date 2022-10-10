using DG.Tweening;
using UnityEngine;

namespace SVell 
{
	public class GreenWall : Wall
	{
		[SerializeField] private Transform wall;

		[Header("Bounce")] 
		[SerializeField] private float moveLength = -0.1f;
		[SerializeField] private float moveDuration = 1f;

		protected override void OnPlayerCollide()
		{
			wall.DOMove(wall.position - wall.forward * moveLength, moveDuration / 2).OnComplete(() =>
			{
				wall.DOMove(wall.position + wall.forward * moveLength, moveDuration).SetEase(Ease.OutBounce);
			});
		}
	}
}
