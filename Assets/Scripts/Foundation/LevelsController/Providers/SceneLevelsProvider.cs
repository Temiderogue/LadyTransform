using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevelsProvider : MonoBehaviour, ILevelProvider
{
	[SerializeField] private bool activateNewScene;

	public event Action OnLevelLoaded;
	public int LevelsCount => SceneManager.sceneCountInBuildSettings - 1;

	private int currentScene = -1;

	public void LoadLevel(int number) => StartCoroutine(LoadLevelCor(number));
	public void Reload() => StartCoroutine(ReloadLevelCor());

	private IEnumerator LoadLevelCor(int number)
	{
#if UNITY_EDITOR
		if (SceneTester.Enabled)
		{
			yield return StartCoroutine(LoadTestScene());
		}
		else
#endif
		{
			yield return UnloadScenes(1);
			currentScene = number + 1;
			yield return StartCoroutine(Load());
		}

		yield return null;

		OnLevelLoaded?.Invoke();
	}

	private IEnumerator ReloadLevelCor()
	{
#if UNITY_EDITOR
		if (SceneTester.Enabled)
		{
			yield return StartCoroutine(ReloadTestScene());
		}
		else
#endif
		{
			yield return StartCoroutine(Unload());
			yield return StartCoroutine(Load());
		}

		yield return null;

		OnLevelLoaded?.Invoke();
	}

	private IEnumerator LoadTestScene()
	{
		yield return UnloadScenes(2);
		yield return ReloadTestScene();
	}

	private IEnumerator ReloadTestScene()
	{
		var scene = SceneManager.GetSceneAt(1);
		var path = scene.path;

		yield return SceneManager.UnloadSceneAsync(scene);
		yield return SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);

		if (activateNewScene) SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

		yield return null;
	}

	private IEnumerator Unload()
	{
		if (currentScene > 0)
			yield return SceneManager.UnloadSceneAsync(currentScene);
	}

	private IEnumerator Load()
	{
		yield return SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
		if (activateNewScene) SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
	}

	private IEnumerator UnloadScenes(int startFrom)
	{
		for (var i = startFrom; i < SceneManager.sceneCount; i++)
			yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
	}
}