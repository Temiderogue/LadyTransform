using UnityEngine;
using UnityEngine.UI;

public class LoseUI : CanvasGroupUI
{
	[SerializeField] private Button button;

	public override void Show()
	{
		base.Show();
		button.interactable = true;
	}

	public override void Hide()
	{
		base.Hide();
		button.interactable = false;
		GameC.Instance.RestartLevel();
	}
}