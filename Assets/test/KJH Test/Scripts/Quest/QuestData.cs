using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public int npcID;
    public string questName;
    public string questType;
    public string questInfomation;
    public bool isClear;
    

    public QuestData() { }
    public QuestData(string questName, int npcID, string questInfomation)
    {
        this.questName = questName;
        this.npcID = npcID;
        this.questInfomation = questInfomation;
    }
}

