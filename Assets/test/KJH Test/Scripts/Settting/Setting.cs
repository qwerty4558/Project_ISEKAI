using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("볼륨 조절")]
    public AudioMixer masterMixer;

    public Slider masterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    [Header("카메라 조절")]
    public CinemachineFreeLook freelook;
    public Slider cameraSetRotateSlider;
    public Slider cameraSetPOVSlider;

    public void Start()
    {
        freelook = FindObjectOfType<CinemachineFreeLook>();
        masterSlider.value = 1f;
        BGMSlider.value = -10f;
        SFXSlider.value = -10f;

        cameraSetRotateSlider.value = 50f;
        cameraSetPOVSlider.value = 50f;
    }
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

    #region CameraSetting

    public void SettingCameraRotate()
    {
        freelook.m_XAxis.m_MaxSpeed = cameraSetRotateSlider.value;
        freelook.m_YAxis.m_MaxSpeed = cameraSetRotateSlider.value/100;
    }
    public void SettingCameraPOV()
    {

    }

    #endregion
}
