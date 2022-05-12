﻿using TFPlay.UI;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
	[SerializeField] private SettingsUI settingsUI;

	[SerializeField] private WinUI winUI;
	[SerializeField] private LoseUI loseUI;
	[SerializeField] private LevelUI levelUI;

	[SerializeField] private Transform coinsUI;

	[SerializeField] private Image blackImage;
	[SerializeField] public Button restartButton;

	public static MenuUI Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	private void Start()
	{
		GameC.Instance.OnInitComplite += Init;
	}

	public void Init()
	{
		GameC.Instance.OnLevelEnd += OnLevelEnd;
		LevelsController.Instance.OnLevelLoadingStart += OnLevelLoadingStart;
		LevelsController.Instance.OnLevelLoaded += OnLevelLoaded;
	}

	private void OnDestroy()
	{
		GameC.Instance.OnInitComplite -= Init;
		GameC.Instance.OnLevelEnd -= OnLevelEnd;
		LevelsController.Instance.OnLevelLoadingStart -= OnLevelLoadingStart;
		LevelsController.Instance.OnLevelLoaded -= OnLevelLoaded;
	}

	public void OnLevelEnd(bool playerWon)
	{
		if (playerWon)
			winUI.Show();
		else
			loseUI.Show();
	}

	private void OnLevelLoaded()
	{
		restartButton.interactable = true;
		blackImage.ChangeAlpha(time: 0.2f, graphic: blackImage, to: 0);
	}

	private void OnLevelLoadingStart()
	{
		restartButton.interactable = false;
		blackImage.color = blackImage.color.WithAlpha(1);
	}
}