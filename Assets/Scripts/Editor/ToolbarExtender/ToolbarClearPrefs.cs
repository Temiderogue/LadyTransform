using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public static class ToolbarClearPrefs
{
	static bool m_enabled;

	static bool Enabled
	{
		get { return m_enabled; }
		set
		{
			m_enabled = value;
			EditorPrefs.SetBool("ToolbarClearPrefs", value);
		}
	}

	static ToolbarClearPrefs()
	{
		ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
	}

	static void OnToolbarGUI()
	{
		GUILayout.FlexibleSpace();

		var tex = EditorGUIUtility.IconContent(@"d_CacheServerDisabled@2x").image;

		if (GUILayout.Button(new GUIContent(null, tex, $"Clear Player Prefs"), "Command"))
			PlayerPrefs.DeleteAll();
	}
}
