using System.Collections.Generic;
using UnityEngine;

namespace SVell 
{
	public class Mover : MonoBehaviour
	{
		[SerializeField] private float moveSpeed = 5f;
		
		[SerializeField] private List<Transform> destinations;

		private int _currentDestinationIndex;
		private bool _increase = true;

		private void Update()
		{
			MoveToDestination();
		}

		private void MoveToDestination()
		{
			transform.position = Vector3.MoveTowards(transform.position,
				destinations[_currentDestinationIndex].position, moveSpeed * Time.deltaTime);

			if (Vector3.Distance(transform.position, destinations[_currentDestinationIndex].position) <= 0.01f)
			{
				_currentDestinationIndex += _increase ? 1 : -1;

				if (_currentDestinationIndex >= destinations.Count - 1)
				{
					_increase = false;
				}

				if (_currentDestinationIndex < 0)
				{
					_increase = true;
				}
				
			}
		}
	}
}
