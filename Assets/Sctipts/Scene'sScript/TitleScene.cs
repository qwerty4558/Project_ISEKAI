using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject option_Window;
    [SerializeField] GameObject uiCanvas;
    void Start()
    {
       option_Window.SetActive(false);
       uiCanvas.SetActive(false);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(uiCanvas != null)
        {

        }
    }

    void Update()
    {
        
    }

    public void ClickButton(int btnNum)
    {
        switch (btnNum)
        {
            case 0:
                QuestTitle.instance.QuestInput("CH_01");
                LoadingSceneController.Instance.LoadScene("L_Midas");
                SceneBroadcaster.AddBroadcast("L_Midas,ToIntro");
                break;
            case 1:
                option_Window.SetActive(true);
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
}
