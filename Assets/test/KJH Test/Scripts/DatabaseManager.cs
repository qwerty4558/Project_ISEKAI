using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

    public static DatabaseManager instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();
    List<Dictionary<string, object>> dialogues;


    public static bool isFinish = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            dialogues = DialogueParser.Read(csv_FileName);
            ParseData(); //         ¥ê 
        }

    }
    public void ParseData() // public         
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        if (dialogues != null)
        {
            for (int i = 0; i < dialogues.Count; i++)
            {
                Dialogue _dialogues = new Dialogue();
                _dialogues.name = dialogues[i]["C_Name"].ToString();

                List<string> contextList = new List<string>();

                string script;

                script = dialogues[i]["Script"].ToString();
                contextList.Add(script);



                _dialogues.context = contextList.ToArray();
                dialogueList.Add(_dialogues);
                dialogueDic.Add(i + 1, _dialogues);
            }
            isFinish = true;
        }
    }
    public Dialogue[] GetDialogues(int _start, int _end)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 0; i <= _end - _start; ++i)
        {
            dialogueList.Add(dialogueDic[_start + i]);
        }
        return dialogueList.ToArray();
    }
}