using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    
    [SerializeField] private GameObject settingBoard_obj;
    [SerializeField] private GameObject option_obj;
    [SerializeField] private GameObject option_Ghrapic;
    [SerializeField] private GameObject option_Sound;
    [SerializeField] private GameObject option_Gameplay;
    [SerializeField] public CameraFollow cameraFollow;
    [SerializeField] private GameObject diary_obj;
    [SerializeField] private GameObject ingame_obj;
    [SerializeField] private GameObject quest_obj;
    [SerializeField] private GameObject inventorys;
    [SerializeField] private UnityEngine.UI.Outline diaryOutLine;
    [SerializeField] public bool checkingDiary = true;
    [SerializeField] public bool isControl = true;
    SoundModule sound;
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
        sound = GetComponent<SoundModule>();


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
        isControl = true;
    }

    private void Update()
    {
        if(isControl == true)
        {
            GetKeys();
        }
        

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

        if (checkingDiary)
        {
            diaryOutLine.effectColor = new Color(255, 255, 0, 0);
        }
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
                sound.Play("OnOff");
            }
            else
            {
                if (diary_obj.activeSelf || inventorys.activeSelf)
                    return;
                PauseGame();
                sound.Play("OnOff");
            }
        }
        if (!settingBoard_obj.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {

                if (diary_obj.activeSelf)
                {
                    ContinueGame();
                    sound.Play("OnOff");
                }
                else
                {

                    if (checkingDiary == false)
                    {
                        checkingDiary = true;
                        sound.Play("OpenBookSound");
                        ViewDiary();
                    }
                    else
                    {
                        sound.Play("OpenBookSound");
                        ViewDiary();
                    }
                }
            }
        }
    }

    IEnumerator IBlink_Icon()
    {
        while (!checkingDiary)
        {
            float duration = .5f;
            float elapsedTime = 0f;

            Color startColor = new Color(255, 255, 0, 0);
            Color endColor = new Color(255, 255, 0, 1);

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
        if (!diary_obj.activeSelf)
        {
            diary_obj.SetActive(true);
            if (cameraFollow != null)
                cameraFollow.isInteraction = true;
            diary_obj.GetComponent<DiaryController>().OpenPage();
        }
    }

    public void StopAllcoroutines()
    {
        if (settingBoard_obj.activeSelf)
        {
            StopAllCoroutines();
        }
    }
    public IEnumerator PausegameDellay()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0f;
        StopAllcoroutines();
    }
    private void PauseGame()
    {
        PausegameDellay();
        settingBoard_obj.SetActive(true);
        settingBoard_obj.GetComponent<DOTweenAnimation>().DORestart();
        if (cameraFollow != null)
            cameraFollow.isInteraction = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        option_Ghrapic.SetActive(false);
        option_Sound.SetActive(false);
        option_Gameplay.SetActive(false);
        settingBoard_obj.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        diary_obj.GetComponent<DOTweenAnimation>().DOPlayBackwards();
        CursorManage.instance.HideMouse();
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;        
    }

    public void ToTitle()
    {
        
        Time.timeScale = 1.0f;
        option_obj.SetActive(false);
        option_Ghrapic.SetActive(false);
        option_Sound.SetActive(false);
        option_Gameplay.SetActive(false);
        settingBoard_obj.SetActive(false);
        if (cameraFollow != null)
            cameraFollow.isInteraction = false;
        if (!ingame_obj.activeSelf || !quest_obj.activeSelf)
        {
            quest_obj.SetActive(true);
            ingame_obj.SetActive(true);
        }
        //string savePath = Application.persistentDataPath + "Quest.Sav";
        MultisceneDatapass.Instance.ResetPortalDestinationID();
        DiaryController.instance.InitDiary();
        SceneBroadcaster.RemoveAllBroadcast();
        QuestTitle.instance.CurrentAndTempQuestClear();
        LoadingSceneController.Instance.LoadScene("Title");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
    public void OptionActive()
    {
        option_obj.SetActive(true);
    }


}
