using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum QuestType
{
    Chat, Position, item
}

public class QuestTitle : MonoBehaviour
{
    public static QuestTitle instance;

    public Dictionary<string, QuestInfo> questDictionary;
    public PlayerController player;

    private void Awake()
    {
        questDictionary = new Dictionary<string, QuestInfo>();
        instance = this;
    }

    public void InputQuest(QuestInfo quest)
    {
        if (quest.isProgress) return;
        if (quest.isClear) return;

        if(questDictionary.Count == 0)
        {
            quest.isProgress = true;
            questDictionary.Add(quest.title, quest);
        }
        else
        {
            foreach (KeyValuePair<string, QuestInfo> info in questDictionary)
            {
                if (quest.questIndex == 0)
                {
                    quest.isProgress = true;
                    questDictionary.Add(quest.title, quest);
                    break;
                }

                QuestInfo test = info.Value;

                if (test.questIndex == quest.questIndex - 1)
                {
                    if (test.isClear)
                    {
                        quest.isProgress = true;
                        questDictionary.Add(quest.title, quest);
                        return;
                    }
                }
            }
        }
    }

    private void ClearCheck(string _key)
    {
        questDictionary[_key].isProgress = false;
        questDictionary[_key].isClear = true;        
    }


    public void QuestCheck()
    {
        if (questDictionary.Count == 0) return;
        foreach (KeyValuePair<string, QuestInfo> info in questDictionary)
        {
            QuestInfo temp = info.Value;

            if (!questDictionary.ContainsKey(temp.title)) return;
            if (questDictionary[temp.title].isClear) continue;
            if (!questDictionary[temp.title].isProgress) continue;

            if (temp.questType == QuestType.Chat)
            {
                Debug.Log("퀘스트 체크");
                for (int i = 0; i < temp.isChats.Length; i++)
                    if (!temp.isChats[i])
                    {
                        temp.isChats[i] = true;
                        return;
                    }
                Debug.Log("상호작용 퀘스트 클리어");
                ClearCheck(temp.title);
            }
            else if (temp.questType == QuestType.Position)
            {
                Debug.Log("퀘스트 체크");
                if (Vector3.Distance(player.transform.position, temp.position.position) < 10f)
                {
                    ClearCheck(temp.title);
                    Debug.Log("포지션 퀘스트 클리어");
                }
            }
            else if (temp.questType == QuestType.item)
            {
                Debug.Log("퀘스트 체크");
                for (int i = 0; i < temp.items.Length; i++)
                {
                    if (!InventoryTitle.instance.itemMap.ContainsKey(temp.items[i].name)) return;
                    if (InventoryTitle.instance.itemMap[temp.items[i].name].count < temp.itemCompleteCount) return;
                }
                Debug.Log("아이템 획득 퀘스트 클리어");
                ClearCheck(temp.title);
            }
        }           
    }
}
