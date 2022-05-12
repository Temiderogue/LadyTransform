using System;

public interface ILevelProvider
{
	event Action OnLevelLoaded;
	int LevelsCount { get; }
	void LoadLevel(int number);
	void Reload();
}