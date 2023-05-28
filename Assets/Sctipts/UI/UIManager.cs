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
    [SerializeField] private GameObject diary_obj;

    private void Start()
    {
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
        settingBoard_obj.SetActive(false);
        option_obj.SetActive(false);
        diary_obj.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoeaded;  
    }
    private void OnDisable()
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
        diary_obj.SetActive(false);
    }

    private void Update()
    {
        GetKeys();

        if (settingBoard_obj.activeSelf || option_obj.activeSelf || diary_obj.activeSelf || InventoryTitle.instance.Inventory.activeSelf)
        {
            CursorManage.instance.ShowdMouse();
        }
        else CursorManage.instance.HideMouse();
    }

    private void GetKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingBoard_obj.activeSelf)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (diary_obj.activeSelf)
            {
                ContinueGame();
            }
            else
            {
                ViewDiary();
            }
        }
    }

    private void ViewDiary()
    {
        diary_obj.SetActive(true);
        if (cameraFollow != null)
            cameraFollow.isInteraction = true;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        settingBoard_obj.SetActive(true);
        if (cameraFollow != null)
            cameraFollow.isInteraction = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        settingBoard_obj.SetActive(false);
        diary_obj.SetActive(false);
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;
        CursorManage.instance.HideMouse();
    }

    public void ToTitle()
    {
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        settingBoard_obj.SetActive(false);
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;
        LoadingSceneController.Instance.LoadScene("Title");
    }

    public void OptionActive()
    {
        option_obj.SetActive(true);
    }


}
