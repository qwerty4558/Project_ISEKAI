using UnityEngine;

public class SceneLoaderExcess : MonoBehaviour
{
    public void LoadNewScene(string sceneName)
    {
        if (LoadingSceneController.Instance != null)
            LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
