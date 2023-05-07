using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : SingletonMonoBehaviour<DialogueManager>
{
    [SerializeField] public GameObject go_Dialogue_Bar;
    //[SerializeField] GameObject go_Dialogue_Name_Bar;

    [SerializeField] public TextMeshProUGUI text_Dialogue;
    [SerializeField] public TextMeshProUGUI text_Name;

    [SerializeField] public string event_Text;
    [SerializeField] public string nowDial;
    //[SerializeField] TMP_Text text_Name;

    [SerializeField] public Image left_Char_Panel;
    [SerializeField] public Image left_Char_Expression_Image;
    [SerializeField] Sprite[] left_Char_Expression;
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
            FacialExpression facial = new FacialExpression();
            facial = (FacialExpression)System.Enum.Parse(typeof(FacialExpression), dialogues[dialogueCount].expression, true);
            SetFacialExpression(facial, left_Char_Expression_Image);


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

    private void SetFacialExpression(FacialExpression _expression, Image _image)
    {
        if(_expression == FacialExpression.nomal)
        {
            left_Char_Expression_Image.gameObject.SetActive(false);            
        }
        else
        {
            left_Char_Expression_Image.gameObject.SetActive(true);
            left_Char_Expression_Image.sprite = left_Char_Expression[((int)_expression) - 1];
        }
    }

    public void SettingUI(bool _flag)
    {
        go_Dialogue_Bar.SetActive(_flag);
    }
}