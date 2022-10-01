using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SVell
{
	public enum MoleState
	{
		Launching,
		Moving,
		Stopped,
		InHole
	}

	public class Mole : MonoBehaviour
	{
		private MoleState _moleState = MoleState.Launching;

		public MoleState MoleState => _moleState;
		
		private void Update()
		{
			HandleState();
		}

		public void SetMoleState(MoleState state)
		{
			switch (state)
			{
				case MoleState.Launching:
					break;
				case MoleState.Moving:
					break;
				case MoleState.Stopped:
					break;
				case MoleState.InHole:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}

			_moleState = state;
		}

		private void HandleState()
		{
			switch (_moleState)
			{
				case MoleState.Launching:
					break;
				case MoleState.Moving:
					break;
				case MoleState.Stopped:
					break;
				case MoleState.InHole:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Hole"))
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}
