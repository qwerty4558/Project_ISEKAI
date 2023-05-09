using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBoardScript : MonoBehaviour
{
    public GameObject SettingBoard;//옵션창
    public GameObject SettingBoard2;//설정창

    // Start is called before the first frame update
    void Start()
    {
        SettingBoard.SetActive(false);
        SettingBoard2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingBoard.SetActive(true);
        }
    }
    void Replay()
    {
        SettingBoard.SetActive(false);
    }

    void Option()
    {
        SettingBoard.SetActive(false);
        SettingBoard.SetActive(true);
    }
}
