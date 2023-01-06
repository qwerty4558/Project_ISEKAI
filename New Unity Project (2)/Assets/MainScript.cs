using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject NPCDialog;
    public GameObject NPCButton;
    private Text NPCText;

    void Start()
    {
        NPCButton = GameObject.Find("NPCButton");
        NPCDialog = GameObject.Find("NPCDialog");
        NPCText = GameObject.Find("NPCText").GetComponent<Text>();
        NPCDialog.SetActive(false);
        NPCButton.SetActive(false);
    }

    public void NPCChatEnter(string text)
    {
        NPCText.text = text;
        NPCDialog.SetActive(true);
    }

    public void NPCChatExit()
    {
        NPCText.text = "";
        NPCDialog.SetActive(false);
    }
    
    public void NPCChatButtonShow()
    {
        NPCButton.SetActive(true);
    }
    public void NPCChatButtonNoShow()
    {
        NPCButton.SetActive(false);
    }


}

