using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject option_Window;
    [SerializeField] UIManager uiCanvas;
    void Start()
    {
       if(uiCanvas == null) uiCanvas = FindObjectOfType<UIManager>();
       option_Window.SetActive(false);
       uiCanvas.gameObject.SetActive(false);
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
        if(IsActivedScene())
        {
            uiCanvas = FindObjectOfType<UIManager>();
            uiCanvas.gameObject.SetActive(false);
        }
    }

    bool IsActivedScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name == "Title";
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClickButton(int btnNum)
    {
        switch (btnNum)
        {
            case 0:
                ItemManager.instance.ItemDataInitailize();
                uiCanvas.gameObject.SetActive(true);
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
