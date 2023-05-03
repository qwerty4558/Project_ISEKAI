using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : GameManager
{
    [SerializeField] public GameObject go_Dialogue_Bar;
    //[SerializeField] GameObject go_Dialogue_Name_Bar;

    [SerializeField] public TextMeshProUGUI text_Dialogue;
    [SerializeField] public TextMeshProUGUI text_Name;

    [SerializeField] public string event_Text;
    [SerializeField] public string nowDial;
    //[SerializeField] TMP_Text text_Name;

    [SerializeField] public Image left_Char_Panel;
    [SerializeField] public Image right_Char_Panel;

    [SerializeField] public Dialogue[] dialogues;

    [SerializeField] public ObjectDataType dataType;

    int dialogueCount;
    int dialogueSubCount;

    public bool isDialogue = false;

    private void Start()
    {
        go_Dialogue_Bar.SetActive(false);
        dialogueCount = 0;
        dialogueSubCount = 0;
    }

    private void OnEnable()
    {
        Go_Next_Text();
    }

    private void Update()
    {       
        
        
    }

    public void Go_Next_Text()
    {
        // ��ȭ ī��Ʈ�� ��ȭ ����ī��Ʈ�� ���̾�α� �迭�� �ε��� ������ ����� �ʾҴ��� üũ
        if (dialogueCount < dialogues.Length && dialogueSubCount < dialogues[dialogueCount].context.Length)
        {
            // ���� ��ȭ ���
            text_Name.text = dialogues[dialogueCount].name;
            nowDial = dialogues[dialogueCount].context[dialogueSubCount];
            text_Dialogue.text = nowDial;
            TMPpoUGUIDoText.DoText(text_Dialogue, 0.5f);
            // ��ȭ ����ī��Ʈ ����
            dialogueSubCount++;

            // ��ȭ ����ī��Ʈ�� ���� ���̾�α��� ��ȭ ���� �������� ���� ���̾�α׷� �̵�
            if (dialogueSubCount >= dialogues[dialogueCount].context.Length)
            {
                // ��ȭ ī��Ʈ ����
                dialogueCount++;

                // ��ȭ ����ī��Ʈ �ʱ�ȭ
                dialogueSubCount = 0;

                // ��ȭ�� ������ ��ȭâ ��Ȱ��ȭ
                if (dialogueCount >= dialogues.Length)
                {
                    dialogueCount = 0;
                    dialogueSubCount = 0;
                    isDialogue = false;
                    dialogues = null;
                    SettingUI(false);
                }
            }

            if (dialogues[dialogueCount].quest != null)
            {
                
            }
        }
    }


    public void SettingUI(bool _flag)
    {
        go_Dialogue_Bar.SetActive(_flag);
    }
}