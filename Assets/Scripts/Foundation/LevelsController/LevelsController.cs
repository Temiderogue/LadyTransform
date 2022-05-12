using System;
using UnityEngine;

public class LevelsController : MonoBehaviour
{
	public event Action OnLevelLoadingStart;
	public event Action OnLevelLoaded;

	public static LevelsController Instance { get; private set; }

	private ILevelProvider levelProvider;

	public int LevelsCount => levelProvider != null ? levelProvider.LevelsCount : 0;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		levelProvider = GetComponent<ILevelProvider>();

		if (levelProvider == null)
			Debug.LogWarning("Провайдер не выбран! Так это не работает", this);

		levelProvider.OnLevelLoaded += TriggerEvent;
	}

	private void Start()
	{
		GameC.Instance.OnLevelStart += LoadLevel;
	}

	private void OnDestroy()
	{
		GameC.Instance.OnLevelStart -= LoadLevel;
		levelProvider.OnLevelLoaded -= TriggerEvent;
	}

	private void TriggerEvent() => OnLevelLoaded?.Invoke();

	private void LoadLevel(int levelNumber)
	{
		OnLevelLoadingStart?.Invoke();
		levelProvider?.LoadLevel(levelNumber % levelProvider.LevelsCount);
	}
	private void Reload()
	{
		OnLevelLoadingStart?.Invoke();
		levelProvider?.Reload();
	}
}