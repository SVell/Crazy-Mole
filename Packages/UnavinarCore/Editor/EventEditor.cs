using Unavinar.HCUnavinarCore.Runtime;
using UnityEditor;
using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
#if UNITY_EDITOR
	[CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
	public class EventEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUI.enabled = Application.isPlaying;

			GameEvent e = target as GameEvent;
			if (GUILayout.Button("Raise"))
			{
				e.Raise();
			}
		}
	}
#endif
}
