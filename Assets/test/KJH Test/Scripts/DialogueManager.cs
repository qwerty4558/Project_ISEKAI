using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_Dialogue_Bar;
    //[SerializeField] GameObject go_Dialogue_Name_Bar;

    [SerializeField] TextMeshProUGUI text_Dialogue;
    [SerializeField] TextMeshProUGUI text_Name;

    [SerializeField] string event_Text;
    [SerializeField] string nowDial;
    //[SerializeField] TMP_Text text_Name;

    [SerializeField] Image left_Char_Panel;
    [SerializeField] Image roght_Char_Panel;

    [SerializeField] Dialogue[] dialogues;

    int dialogueCount;
    int dialogueSubCount;

    bool isDialogue = false;

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
        
        if (isDialogue == true)
        {                         
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Go_Next_Text();
            }                            
        }
        else
        {
            isDialogue = false;
            SettingUI(false);
        }
    }

    void Go_Next_Text()
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
        }
    }


    private void OnTriggerStay(Collider other)
    {
      
        if (other.CompareTag("NPC"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                roght_Char_Panel.sprite = other.GetComponent<ObjectDataType>().NPC_Panel_Sprite;
                event_Text = other.GetComponent<DialogueEvent>().eventName;            
                dialogues = DialogueParser.GetDialogues(event_Text);           
                isDialogue = true;
                SettingUI(true);                
            }       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isDialogue = false;
            dialogueCount = 0;
            dialogueSubCount = 0;
        }
        
    }

    public void ShowDialogue(Dialogue[] _dialogues)
    {
        text_Dialogue.text = "";
       // text_Name.text = "";

        
    }

    private void SettingUI(bool _flag)
    {
        go_Dialogue_Bar.SetActive(_flag);
    }
}