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
    [SerializeField] private GameObject ingame_obj;
    [SerializeField] private GameObject quest_obj;
    [SerializeField] private UnityEngine.UI.Outline diaryOutLine;
    [SerializeField] public bool checkingDiary = true;

    [Header("Diary")]
    [SerializeField] int latest_Page;
    private void Start()
    {
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
        settingBoard_obj.SetActive(false);
        option_obj.SetActive(false);
        diary_obj.GetComponent<DiaryController>().InitDiary();
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
            if (cameraFollow != null)
                cameraFollow.isInteraction = true;
            CursorManage.instance.ShowMouse();
        }
        else
        {
            if (cameraFollow != null)
                cameraFollow.isInteraction = false;
        }

        if (checkingDiary) return;
        else
        {
            StartCoroutine(IBlink_Icon());
        }

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (diary_obj.activeSelf)
            {
                ContinueGame();
            }
            else
            {
                if (checkingDiary == false)
                {
                    checkingDiary = true;
                    ViewDiary();
                    diary_obj.GetComponent<DiaryController>().UnLockPage();
                }
                else ViewDiary();
            }
        }
    }

    public void QuestComplateUnlockCheckDiary()
    {
        checkingDiary = false;
    }

    IEnumerator IBlink_Icon()
    {
        while (!checkingDiary)
        {
            float duration = .5f;
            float elapsedTime = 0f;

            Color startColor = new Color(255, 0, 255, 0);
            Color endColor = new Color(255, 0, 255, 1);

            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration) / duration;
                diaryOutLine.effectColor = Color.Lerp(startColor, endColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;


            }
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration) / duration;
                diaryOutLine.effectColor = Color.Lerp(endColor, startColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
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
        diary_obj.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;        
    }

    public void ToTitle()
    {
        
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        settingBoard_obj.SetActive(false);
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;
        if (!ingame_obj.activeSelf || !quest_obj.activeSelf)
        {
            quest_obj.SetActive(true);
            ingame_obj.SetActive(true);
        }
        LoadingSceneController.Instance.LoadScene("Title");
    }

    public void OptionActive()
    {
        option_obj.SetActive(true);
    }


}
