using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] string eventName;
    [SerializeField] Dialogue[] dialogues;

    private void Start()
    {
        GetDialogues();
        DebugDialogue(GetDialogues());
    }

    public Dialogue[] GetDialogues()
    {
        dialogues = DialogueParser.GetDialogues(eventName);
        return dialogues;
    }

    void DebugDialogue(Dialogue[] talkDatas)
    {
        for (int i = 0; i < talkDatas.Length; i++)
        {
            // ĳ���� �̸� ���
            Debug.Log(talkDatas[i].name);
            // ���� ���
            foreach (string context in talkDatas[i].context)
                Debug.Log(context);
        }
    }
}