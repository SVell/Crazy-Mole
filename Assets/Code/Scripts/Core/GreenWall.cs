using DG.Tweening;
using UnityEngine;

namespace SVell 
{
	public class GreenWall : Wall
	{
		[SerializeField] private Transform wall;

		[Header("Bounce")] 
		[SerializeField] private float moveLength = -0.25f;
		[SerializeField] private float moveInDuration = 0.1f;
		[SerializeField] private float moveOutDuration = 1f;

		protected override void OnPlayerCollide()
		{
			wall.DOMove(wall.position - wall.forward * moveLength, moveInDuration / 2).OnComplete(() =>
			{
				wall.DOMove(wall.position + wall.forward * moveLength, moveOutDuration);
			});
		}
	}
}
