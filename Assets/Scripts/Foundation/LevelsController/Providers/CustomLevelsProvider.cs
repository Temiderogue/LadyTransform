using System;
using UnityEngine;

public class CustomLevelsProvider : MonoBehaviour, ILevelProvider
{
	public int LevelsCount => 0;

	public event Action OnLevelLoaded;

	public void LoadLevel(int number)
	{
		// Load here
		OnLevelLoaded?.Invoke();
	}

	public void Reload()
	{
		// Reload here
		OnLevelLoaded?.Invoke();
	}
}