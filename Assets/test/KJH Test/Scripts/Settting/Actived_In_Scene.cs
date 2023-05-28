using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actived_In_Scene : MonoBehaviour
{
    [SerializeField] GameObject inGameUICanvas;
    [SerializeField] GameObject questUI;
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
        if (inGameUICanvas == null && questUI == null)
        {
            inGameUICanvas = GameObject.FindWithTag("InGameUI");
            questUI = GameObject.FindWithTag("QuestUI");
        }
    }

    public void SetActiveInGameUI()
    {
        inGameUICanvas.SetActive(true);
        questUI.SetActive(true);
    }

    public void DeActiveInGameUI()
    {
        inGameUICanvas.SetActive(false);
        questUI.SetActive(false);
    }
}
