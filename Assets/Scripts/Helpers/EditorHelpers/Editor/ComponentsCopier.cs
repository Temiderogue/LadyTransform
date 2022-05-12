using System.Collections;
using UnityEditor;
using UnityEngine;

public class ComponentsCopier : EditorWindow
{
	static Component[] copiedComponents;

	[MenuItem("Tools/Anton/Components/Copy Components")]
	static void Copy()
	{
		copiedComponents = Selection.activeGameObject.GetComponents<Component>();
	}

	[MenuItem("Tools/Anton/Components/Past Components")]
	static void Paste()
	{
		foreach (var targetGameObject in Selection.gameObjects)
		{
			if (!targetGameObject || copiedComponents == null) continue;
			foreach (var copiedComponent in copiedComponents)
			{
				if (!copiedComponent) continue;
				UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponent);
				UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject);
			}
		}
	}
}