using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcID;

    public QuestData() { }
    public QuestData(string questName, int[] npcID)
    {
        this.questName = questName;
        this.npcID = npcID;
    }
}

public class Quest
{
    public string questTitle;
    [TextArea(3, 10)]
    public string descriptrion;
}
