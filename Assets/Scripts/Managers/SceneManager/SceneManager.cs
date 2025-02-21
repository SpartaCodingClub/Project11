using System.Collections;
using UnityEngine;

public class SceneManager
{
    private Scene_Base currentScene;

    public T GetCurrentScene<T>() where T : Scene_Base => currentScene as T;

    public void LoadScene<T>() where T : Scene_Base
    {
        Managers.Instance.Clear();
        Managers.Instance.StartCoroutine(LoadingScene<T>(typeof(T).Name));
    }

    private IEnumerator LoadingScene<T>(string sceneName) where T : Scene_Base
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        currentScene = Object.FindObjectOfType<T>();
    }
}