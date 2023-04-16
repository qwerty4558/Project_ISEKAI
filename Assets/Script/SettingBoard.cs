using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBoard : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Option;
    private bool Settingstate;
    private bool Optionstate;

    void Start()
    {
        Settingstate = false;
        Setting.SetActive(Settingstate);
        Optionstate = false;
        Option.SetActive(Optionstate);
    }

    void Update()
    {
        Debug.Log(Settingstate);
        if (Input.GetKeyDown(KeyCode.Escape)) Settingstate = !Settingstate;
        ViewUI();
    }

    void ViewUI()
    {
        Setting.SetActive(Settingstate);
    }

    public void ClickReplayButton()
    {
        Settingstate = false;
    }

    public void ClickOptionButton()
    {
        Optionstate = true;
    }
}
