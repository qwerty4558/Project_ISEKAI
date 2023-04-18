using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_Dialogue_Bar;
    //[SerializeField] GameObject go_Dialogue_Name_Bar;

    [SerializeField] TMP_Text text_Dialogue;
    [SerializeField] TMP_Text text_Name;

    [SerializeField] Image player_CG;
    [SerializeField] Image NPC_CG;

    Dialogue[] dialogues;

    bool isDialogue = false;

    private void Start()
    {
        go_Dialogue_Bar.SetActive(false);

    }

    private void Update()
    {
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                text_Dialogue.text = "";
            }
        }


    }

    public void ShowDialogue(Dialogue[] _dialogues)
    {
        text_Dialogue.text = "";
        text_Name.text = "";

        //NPC_CG.sprite = FindObjectOfType<ObjectDataType>().NPC_Panel_Sprite;

        dialogues = _dialogues;

        SettingUI(true);
    }

    private void SettingUI(bool _flag)
    {
        go_Dialogue_Bar.SetActive(_flag);
        //go_Dialogue_Name_Bar.SetActive(_flag);
    }
}