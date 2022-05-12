using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WinUI : CanvasGroupUI
{
	[SerializeField] private List<ParticleSystem> effects;
	[SerializeField] private Button button;

	public override void Show()
	{
		base.Show();
		button.interactable = true;

		foreach (var effect in effects)
			effect.Play();
	}

	public override void Hide()
	{
		base.Hide();
		button.interactable = false;
		GameC.Instance.NextLevel();
	}
}