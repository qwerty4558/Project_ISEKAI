using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Setting : MonoBehaviour 
{ 

    [Header("Setting Board Game OBJ")]
    public GameObject mainOpion;
    public GameObject graphicSetting;
    public GameObject soundSetting;
    public GameObject gamePlaySetting;

    [Header("사운드")]
    public AudioMixer masterMixer;

    public Slider masterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public Toggle masterSoundToggle;


    [Header("그래픽")]
    public List<Resolution> resolutions = new List<Resolution>();
    public Dropdown resolutionDropdown;
    public Toggle fullScreen_Toggle;
    FullScreenMode fullScreenMode;
    int resolutionNum;

    [Header("게임 플레이")]
    public CinemachineFreeLook freelook;
    public Slider cameraSetRotateSlider;
    public float _cameraRotateSpeed;

    [Header("세팅 저장")]
    public GameObject isSavePoup;


    [Header("UIManager")]
    [SerializeField] UIManager uiManager;

    public void Start()
    {
        AllBoardClosed();
        InitSoundSetting();
        InitCameraRotateSetting();
        InitGraphicSetting();
        LoadSettings();
    }

    

    private void Update()
    {
        if (SceneManager.GetSceneByName("Title") != SceneManager.GetActiveScene() || SceneManager.GetSceneByName("L_shop") != SceneManager.GetActiveScene())
        {
            if (SceneManager.GetSceneByName("L_shop") != SceneManager.GetActiveScene())
            {
                if (!uiManager.cameraFollow.isInteraction)
                {
                    AllBoardClosed();
                }
            }
            else AllBoardClosed();
        }
        
    }

    #region InitSetting
    public void InitGraphicSetting()
    {
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 144)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;      
        
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData op = new Dropdown.OptionData();
            op.text = item.width + " x " + item.height + " " + item.refreshRate + " hz ";
            resolutionDropdown.options.Add(op);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();
        fullScreen_Toggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void InitCameraRotateSetting()
    {
        cameraSetRotateSlider.value = 5.5f;
    }

    public void InitSoundSetting()
    {
        masterSlider.value = 1f;
        BGMSlider.value = -10f;
        SFXSlider.value = -10f;
    }
    #endregion

    #region SaveAndLoad

    public void SaveSettings()
    {
        GameSetting settings = new GameSetting();
        settings.isFullScreen = fullScreen_Toggle.isOn;
        settings.isSoundOn = masterSoundToggle.isOn;
        settings.resolutionIndex = resolutionDropdown.value;
        settings.masterVolume = masterSlider.value;
        settings.bgmVolume = BGMSlider.value;
        settings.sfxVolume = SFXSlider.value;
        settings.cameraRotateSpeed = cameraSetRotateSlider.value;

        Serializer.Save<GameSetting>("settings.json", settings);
    }

    public void LoadSettings()
    {
        GameSetting settings = Serializer.Load<GameSetting>("settings.json");

        if (settings != null)
        {
            fullScreen_Toggle.isOn = settings.isFullScreen;
            masterSoundToggle.isOn = settings.isSoundOn;
            resolutionDropdown.value = settings.resolutionIndex;
            SetResolution(settings.resolutionIndex);
            masterSlider.value = settings.masterVolume;
            BGMSlider.value = settings.bgmVolume;
            SFXSlider.value = settings.sfxVolume;
            cameraSetRotateSlider.value = settings.cameraRotateSpeed;

            SettingFullScreenMode(settings.isFullScreen);
            SettingCameraRotate();
        }
    }

    #endregion

    #region SettingBoardChange
    private void AllBoardClosed()
    {
        mainOpion.SetActive(false);
        graphicSetting.SetActive(false);
        soundSetting.SetActive(false);
        gamePlaySetting.SetActive(false);
        isSavePoup.SetActive(false);
    }
    public void SettingBoardChange(string _page)
    {
        AllBoardClosed();
        switch (_page)
        {
            case "Main":
                mainOpion.SetActive(true);
                break;
            case "Graphic":
                graphicSetting.SetActive(true);
                break;
            case "Sound":
                soundSetting.SetActive(true);
                break;
            case "GamePlay":
                gamePlaySetting.SetActive(true);
                break;
        }
    }
    public void PopupBoard()
    {
        isSavePoup.SetActive(true);
    }
    public void ClosePopup()
    {
        isSavePoup.SetActive(false);
    }
    public void CloseSettingBoard()
    {
        mainOpion.SetActive(false);        
    }
    #endregion

    #region SoundSetting
    public void AudioMasterControll()
    {
        float soundVolume = masterSlider.value;
        if (soundVolume == -40f)
        {
            masterMixer.SetFloat("Master", -80f);
        }
        else masterMixer.SetFloat("Master", soundVolume);
    }

    public void AudioSFXControll()
    {
        float soundVolume = SFXSlider.value;
        if (soundVolume == -40f)
        {
            masterMixer.SetFloat("SFX", -80f);
        }
        else masterMixer.SetFloat("SFX", soundVolume);
    }

    public void AudioBGMControll()
    {
        float soundVolume = BGMSlider.value;
        if (soundVolume == -40f)
        {
            masterMixer.SetFloat("BGM", -80f);
        }
        else masterMixer.SetFloat("BGM", soundVolume);
    }

    public void ToggleAudioVolume(bool _isPlaySound)
    {
        AudioListener.volume = _isPlaySound ? 1 : 0; 
    }
    #endregion

    #region GamePlay
    public void SettingCameraRotate()
    {
        //freelook.m_XAxis.m_MaxSpeed = cameraSetRotateSlider.value;
        _cameraRotateSpeed = cameraSetRotateSlider.value / 100;

    }
    #endregion

    #region Ghraphic

    public void SetResolution(int _resolution)
    {
        resolutionNum = _resolution;
        
    }
    public void ApplyResolution()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, fullScreenMode, resolutions[resolutionNum].refreshRate);
    }
    public void SettingFullScreenMode(bool isfull)
    {
        fullScreenMode = isfull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    #endregion

    #region InTitle
    
    #endregion
}
