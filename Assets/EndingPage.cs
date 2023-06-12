using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPage : MonoBehaviour
{
    private void Update()
    {
        CursorManage.instance.ShowMouse();
    }
    private void OnEnable()
    {
        PlayerController.instance.gameObject.SetActive(false);
        UIManager.Instance.gameObject.SetActive(false);
        QuestTitle.instance.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
