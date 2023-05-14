using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void Replay()
    {
        SettingBoard.SetActive(false);
    }

    public void Option()
    {
        SettingBoard.SetActive(false);
        SettingBoard2.SetActive(true);
    }

    public void GotoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
