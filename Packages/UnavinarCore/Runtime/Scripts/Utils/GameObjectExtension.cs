using UnityEngine;

public static class GameObjectExtension
{
	public static T GetRequiredComponent<T>(this GameObject obj) where T : Component 
	{
		T component = obj.GetComponent<T>();

		if (component == null)
		{
			Debug.LogError("Object type of " + typeof(T) + " is required but missing", obj);
		}

		return component;
	}

	public static bool IsInLayerMask(this GameObject obj, LayerMask layerMask)
	{
		return layerMask == (layerMask | (1 << obj.layer));
	}
}