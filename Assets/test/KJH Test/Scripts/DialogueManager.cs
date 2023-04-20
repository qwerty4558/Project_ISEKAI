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
        // 대화 카운트와 대화 서브카운트가 다이얼로그 배열의 인덱스 범위를 벗어나지 않았는지 체크
        if (dialogueCount < dialogues.Length && dialogueSubCount < dialogues[dialogueCount].context.Length)
        {
            // 다음 대화 출력
            text_Name.text = dialogues[dialogueCount].name;
            nowDial = dialogues[dialogueCount].context[dialogueSubCount];
            text_Dialogue.text = nowDial;
            TMPpoUGUIDoText.DoText(text_Dialogue, 0.5f);
            // 대화 서브카운트 증가
            dialogueSubCount++;

            // 대화 서브카운트가 현재 다이얼로그의 대화 수와 같아지면 다음 다이얼로그로 이동
            if (dialogueSubCount >= dialogues[dialogueCount].context.Length)
            {
                // 대화 카운트 증가
                dialogueCount++;

                // 대화 서브카운트 초기화
                dialogueSubCount = 0;

                // 대화가 끝나면 대화창 비활성화
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