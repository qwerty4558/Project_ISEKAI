using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextAsset Quest_Database; // 퀘스트 정보
    public List<Quest> quest_List;

    [SerializeField]
    public GameObject FirstQuestSlot;
    public GameObject SecondQuestSlot;
    public GameObject ThirdQuestSlot;

    public void Awake()
    {
        InitQuestDatabase();
        PrintQuestInfo();
    }

    public void Update()
    {

    }


    private void InitQuestDatabase()
    {
        string[] line = Quest_Database.text.Substring(0, Quest_Database.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            quest_List.Add(new Quest(row[0], row[1], row[2], row[3]));
        }
    }

    public void PrintQuestInfo()
    {
        Debug.Log("Quest Loading");

        int count = 1;

        int first = 0 + (count - 1) * 3;
        int second = first + 1;
        int third = second + 1;

        // i가 1일 경우 0, 1, 2. i가 2일 경우 3, 4, 5... 

        Debug.Log(string.Join(", ", first, second, third));

        Debug.Log(string.Join(" ", quest_List.Count));

        Quest FirstQuest = quest_List[first];
        Quest SecondQuest = quest_List[second];
        Quest ThirdQuest = quest_List[third];
        
        Debug.Log(string.Join(", ", quest_List[first].Quest_Name, quest_List[second].Quest_Name, quest_List[third].Quest_Name));


        Transform FirstParent = FirstQuestSlot.transform;
        Transform SecondParent = SecondQuestSlot.transform;
        Transform ThirdParent = ThirdQuestSlot.transform;


        // 첫번째 퀘스트

        Transform QuestName1 = FirstParent.GetChild(0);
        TMP_Text childQuest1 = QuestName1.GetComponent<TMP_Text>();

        childQuest1.text = FirstQuest.Quest_Name.ToString();

        Debug.Log("퀘스트 이름 : " + FirstQuest.Quest_Name.ToString());

        Transform File_Name1 = FirstParent.GetChild(2);
        TMP_Text childFile_Name1 = File_Name1.GetComponent<TMP_Text>();

        childFile_Name1.text = FirstQuest.NPC_File_Name.ToString();

        Debug.Log("파일 이름 : " + FirstQuest.NPC_File_Name.ToString());

        Transform NPC_Name1 = FirstParent.GetChild(6);
        TMP_Text childName1 = NPC_Name1.GetComponent<TMP_Text>();

        childName1.text = FirstQuest.NPC_Name.ToString();

        Debug.Log("NPC 이름 : " + FirstQuest.NPC_Name.ToString());

        Transform Quest_Price1 = FirstParent.GetChild(7);
        TMP_Text QuestPrice1 = Quest_Price1.GetComponent<TMP_Text>();

        QuestPrice1.text = FirstQuest.Quest_Price.ToString();

        Debug.Log("보상 : " + FirstQuest.Quest_Price.ToString());


        // 두번째 퀘스트

        Transform QuestName2 = SecondParent.GetChild(0);
        TMP_Text childQuest2 = QuestName2.GetComponent<TMP_Text>();

        childQuest2.text = SecondQuest.Quest_Name.ToString();

        Debug.Log("퀘스트 이름 : " + SecondQuest.Quest_Name.ToString());

        Transform File_Name2 = SecondParent.GetChild(5);
        TMP_Text childFile_Name2 = File_Name2.GetComponent<TMP_Text>();

        childFile_Name2.text = SecondQuest.NPC_File_Name.ToString();

        Debug.Log("파일 이름 : " + SecondQuest.NPC_File_Name.ToString());

        Transform NPC_Name2 = SecondParent.GetChild(6);
        TMP_Text childName2 = NPC_Name2.GetComponent<TMP_Text>();

        childFile_Name1.text = SecondQuest.NPC_Name.ToString();

        Debug.Log("NPC 이름 : " + SecondQuest.NPC_Name.ToString());

        Transform Quest_Price2 = SecondParent.GetChild(7);
        TMP_Text QuestPrice2 = Quest_Price2.GetComponent<TMP_Text>();

        QuestPrice2.text = SecondQuest.Quest_Price.ToString();

        Debug.Log("보상 : " + SecondQuest.Quest_Price.ToString());

        // 세번째 퀘스트

        Transform QuestName3 = ThirdParent.GetChild(0);
        TMP_Text childQuest3 = QuestName3.GetComponent<TMP_Text>();

        childQuest3.text = ThirdQuest.Quest_Name.ToString();

        Debug.Log("퀘스트 이름 : " + ThirdQuest.Quest_Name.ToString());


        Transform File_Name3 = ThirdParent.GetChild(5);
        TMP_Text childFile_Name3 = File_Name3.GetComponent<TMP_Text>();

        childFile_Name3.text = ThirdQuest.NPC_File_Name.ToString();

        Debug.Log("파일 이름 : " + ThirdQuest.NPC_File_Name.ToString());


        Transform NPC_Name3 = ThirdParent.GetChild(6);
        TMP_Text childName3 = NPC_Name3.GetComponent<TMP_Text>();

        childName3.text = ThirdQuest.NPC_Name.ToString();

        Debug.Log("NPC 이름 : " + ThirdQuest.NPC_Name.ToString());


        Transform Quest_Price3 = ThirdParent.GetChild(7);
        TMP_Text QuestPrice3 = Quest_Price3.GetComponent<TMP_Text>();

        QuestPrice3.text = ThirdQuest.Quest_Price.ToString();

        Debug.Log("보상 : " + ThirdQuest.Quest_Price.ToString());


    }
}
   
