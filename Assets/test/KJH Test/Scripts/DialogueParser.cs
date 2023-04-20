using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    [SerializeField] private TextAsset csvFile = null;
    public static Dictionary<string, Dialogue[]> dialogueDic = new Dictionary<string, Dialogue[]>();


    private void Awake()
    {
        SetTalkDictonary();
        DebugDialogue();
    }

    public void SetTalkDictonary()
    {
        string csv_Text = csvFile.text.Substring(0, csvFile.text.Length - 1);
        string[] rows = csv_Text.Split(new char[] { '\n' });
        int row_Lenth = rows.Length;

        for (int i = 1; i < row_Lenth; i++)
        {
            string[] rowVal = rows[i].Split(new char[] { ',' });

            if (string.IsNullOrEmpty(rowVal[0]) || string.IsNullOrEmpty(rowVal[1]) || rowVal[1].Trim() == "end" || dialogueDic.ContainsKey(rowVal[0].Trim())) continue;

            List<Dialogue> dial_List = new List<Dialogue>();
            string eventName = rowVal[0].Trim();

            while (rowVal[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();
                Dialogue dial;
                dial.name = rowVal[1];
                do
                {
                    contextList.Add(rowVal[2].ToString());

                    if (++i < row_Lenth) rowVal = rows[i].Split(new char[] { ',' });
                    else break;

                } while (string.IsNullOrEmpty(rowVal[1]) && rowVal[0] != "end");

                dial.context = contextList.ToArray();
                dial_List.Add(dial);
            }

            dialogueDic.Add(eventName, dial_List.ToArray());
        }
    }


    public static Dialogue[] GetDialogues(string _eventName)
    {
        return dialogueDic[_eventName];
    }
    void DebugDialogue()
    {

    }
}
