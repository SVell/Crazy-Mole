using UnityEditor;
using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
#if UNITY_EDITOR
	public class SetRandomYRotation : EditorWindow
	{
		[MenuItem("Tools/Set Random Y Rotation")]
		static void CreateSetRandomRotation()
		{
			GetWindow<SetRandomYRotation>();
		}

		private void OnGUI()
		{
			
			if (GUILayout.Button("Rotate"))
			{
				var selection = Selection.gameObjects;

				for (var i = selection.Length - 1; i >= 0; --i)
				{
					
					var selected = selection[i];
					
					Undo.RecordObject(selected.transform, "Rotation");
					// Mark objects so that inspector saves changes
					EditorUtility.SetDirty(selected);
					
					Quaternion rotation = selected.transform.rotation;
					rotation.y = Random.rotation.y;
					
					selected.transform.rotation = rotation; 
				}
			}

			GUI.enabled = false;
			EditorGUILayout.LabelField("Selection count: " + Selection.objects.Length);
		}
	}
#endif
}