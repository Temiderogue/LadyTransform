using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLevelsProvider : MonoBehaviour, ILevelProvider
{
	[SerializeField] private List<GameObject> levels = new List<GameObject>();

	private GameObject currentLevel;
	private int currentLevelIndex;

	public event Action OnLevelLoaded;

	public int LevelsCount => levels.Count;

	public void LoadLevel(int number)
	{
		currentLevelIndex = number;
		Load(currentLevelIndex);
		OnLevelLoaded?.Invoke();
	}

	public void Reload()
	{
		Load(currentLevelIndex);
		OnLevelLoaded?.Invoke();
	}

	public void Load(int index)
	{
		if (currentLevel != null)
			Destroy(currentLevel);

		currentLevel = Instantiate(levels[index]);
	}
}