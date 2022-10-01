using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SVell 
{
	public class Mole : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Hole"))
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}
