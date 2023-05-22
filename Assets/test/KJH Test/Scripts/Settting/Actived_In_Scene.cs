using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actived_In_Scene : MonoBehaviour
{
    [SerializeField] GameObject inGameUICanvas;
    // Start is called before the first frame update

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoeaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoeaded;
    }

    private void OnSceneLoeaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (inGameUICanvas == null)
        {
            inGameUICanvas = GameObject.FindWithTag("InGameUI");
        }
    }

    public void SetActiveInGameUI()
    {
        inGameUICanvas.SetActive(true);
    }

    public void DeActiveInGameUI()
    {
        inGameUICanvas.SetActive(false);
    }
}
