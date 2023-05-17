using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("Setting Board Game OBJ")]
    public GameObject mainOpion;
    public GameObject graphicSetting;
    public GameObject soundSetting;
    public GameObject gamePlaySetting;

    [Header("����")]
    public AudioMixer masterMixer;

    public Slider masterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public Toggle masterSoundToggle;
    public Toggle BGMSoundToggle;
    public Toggle SFXSoundToggle;

    [Header("�׷���")]
    public List<Resolution> resolutions = new List<Resolution>();
    public Dropdown resolutionDropdown;
    public Toggle fullScreen_Toggle;
    FullScreenMode fullScreenMode;
    int resolutionNum;

    [Header("���� �÷���")]
    public CinemachineFreeLook freelook;
    public Slider cameraSetRotateSlider;


    public void Start()
    {
        AllBoardClosed();
        InitSoundSetting();
        InitCameraRotateSetting();
        InitGraphicSetting();
    }

    

    private void Update()
    {
        if(!UIManager.Instance.cameraFollow.isInteraction)
        {
            AllBoardClosed();
        }
    }

    #region InitSetting
    public void InitGraphicSetting()
    {
        int optionNum = 0;
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();
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

    #region SettingBoardChange
    private void AllBoardClosed()
    {
        mainOpion.SetActive(false);
        graphicSetting.SetActive(false);
        soundSetting.SetActive(false);
        gamePlaySetting.SetActive(false);
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

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
    #endregion

    #region GamePlay
    public void SettingCameraRotate()
    {
        freelook.m_XAxis.m_MaxSpeed = cameraSetRotateSlider.value;
        freelook.m_YAxis.m_MaxSpeed = cameraSetRotateSlider.value / 100;
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
}
