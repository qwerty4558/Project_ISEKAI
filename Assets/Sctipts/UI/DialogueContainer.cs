using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : SerializedMonoBehaviour
{
    static private DialogueContainer instance;
    static public DialogueContainer Instance { get { return instance; } }

    private void OnEnable()
    {
        instance = this;
    }

    [SerializeField] private Dictionary<string, DialogueSequence[]> dialogues;
    [SerializeField] private Dictionary<string, LargeDialogueData[]> cutscene_dialogues;

    public static void StartDialogue(string key)
    {
        UI_Dialogue dialogueUI = GameObject.FindObjectOfType<UI_Dialogue>();
        if (dialogueUI == null) { Debug.LogError("UI_Dialogue를 포함하는 게임오브젝트를 찾을 수 없습니다. 해당 스크립트를 가진 오브젝트와 함께 사용해 주세요."); return; }

        dialogueUI.PlayDialogue(instance.dialogues[key]);
    }

    public static void StartLargeDialogue(string key)
    {
        UI_LargeDialogue dialogueUI = GameObject.FindObjectOfType<UI_LargeDialogue>();
        if (dialogueUI == null) { Debug.LogError("UI_LargeDialogue를 포함하는 게임오브젝트를 찾을 수 없습니다. 해당 스크립트를 가진 오브젝트와 함께 사용해 주세요."); return; }

        dialogueUI.PlayDialogue(instance.cutscene_dialogues[key]);
    }
}
