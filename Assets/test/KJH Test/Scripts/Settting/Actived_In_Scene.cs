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
        if (inGameUICanvas == null)
        {
            inGameUICanvas = GameObject.FindGameObjectWithTag("InGameUI"); //GameObject.FindWithTag("InGameUI");
        }
        if(questUI == null)
        {
            questUI = GameObject.FindGameObjectWithTag("QuestUI"); //GameObject.FindWithTag("QuestUI");
        }
    }

    public void SetActiveInGameUI()
    {
        inGameUICanvas.SetActive(true);
        UIManager.Instance.isControl = true;
    }

    public void DeActiveInGameUI()
    {
        inGameUICanvas.SetActive(false);
        UIManager.Instance.isControl = false;
    }

    public void SetActiveQuestUI()
    {
        questUI.SetActive(true);
    }

    public void DeActiveQuestUI()
    {
        questUI.SetActive(false);
    }

    public void HideQuestUI()
    {
        QuestTitle.instance.QusetPanelHide();
    }
}
