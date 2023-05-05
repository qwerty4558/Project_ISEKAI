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
        if (dialogueUI == null) { Debug.LogError("UI_Dialogue�� �����ϴ� ���ӿ�����Ʈ�� ã�� �� �����ϴ�. �ش� ��ũ��Ʈ�� ���� ������Ʈ�� �Բ� ����� �ּ���."); return; }

        dialogueUI.PlayDialogue(instance.dialogues[key]);
    }

    public static void StartLargeDialogue(string key)
    {
        UI_LargeDialogue dialogueUI = GameObject.FindObjectOfType<UI_LargeDialogue>();
        if (dialogueUI == null) { Debug.LogError("UI_LargeDialogue�� �����ϴ� ���ӿ�����Ʈ�� ã�� �� �����ϴ�. �ش� ��ũ��Ʈ�� ���� ������Ʈ�� �Բ� ����� �ּ���."); return; }

        dialogueUI.PlayDialogue(instance.cutscene_dialogues[key]);
    }
}
