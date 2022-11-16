using System;
using UnityEngine;

namespace SVell 
{
	public class Rotor : MonoBehaviour
	{
		[SerializeField] private RotorSettings settings;

		private void Update()
		{
			transform.Rotate(Vector3.up * settings.RotationSpeed * Time.deltaTime);
		}
	}
}
