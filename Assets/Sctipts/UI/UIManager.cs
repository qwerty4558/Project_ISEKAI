using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    
    [SerializeField] private GameObject settingBoard_obj;
    [SerializeField] private GameObject option_obj;
    [SerializeField] public CameraFollow cameraFollow;

    private void Start()
    {
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
        settingBoard_obj.SetActive(false);
        option_obj.SetActive(false);
    }

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
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
        settingBoard_obj.SetActive(false);
        option_obj.SetActive(false);
    }

    private void Update()
    {
        //SetActivedUI();
        //CheckScene(SceneManager.GetActiveScene());

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingBoard_obj.activeSelf)
            {
                Time.timeScale = 1.0f;
                option_obj.SetActive(false);
                settingBoard_obj.SetActive(false);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = false;                 
            }
            else
            {
                Time.timeScale = 0f;
                settingBoard_obj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
            }
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        settingBoard_obj.SetActive(false);
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;
    }

    public void ToTitle()
    {
        SceneLoaderExcess sceneLoad = new SceneLoaderExcess();
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        settingBoard_obj.SetActive(false);
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;
        sceneLoad.LoadNewScene("Title");
    }

    public void OptionActive()
    {
        option_obj.SetActive(true);
    }


}
