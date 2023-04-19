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
            // 캐릭터 이름 출력
            Debug.Log(talkDatas[i].name);
            // 대사들 출력
            foreach (string context in talkDatas[i].context)
                Debug.Log(context);
        }
    }
}