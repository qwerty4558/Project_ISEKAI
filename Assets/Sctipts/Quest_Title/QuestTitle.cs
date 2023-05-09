using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class QuestTitle : MonoBehaviour
{
    public static QuestTitle instance;

    private Queue<QuestInfo> questRoute;

    [SerializeField] private QuestUI questUI;
    
    [SerializeField] private QuestInfo[] questBox;
    public QuestInfo currentQuest;
    public PlayerController player;


    private void Awake()
    {
        instance = this; 
        questRoute = new Queue<QuestInfo>();
        QuestInit();
    }

    private void QuestClearCheck()
    {
        questUI.UpdateUI();

        for(int i = 0; i < currentQuest.questInfoDatas.Length; i++)
            if (!currentQuest.questInfoDatas[i].isClear) return;

        if(questRoute.Count > 0)
            currentQuest = questRoute.Dequeue();

        questUI.ReRoadUI();
    }

    private void QuestInit()
    {
        for (int i = 0; i < questBox.Length; i++) questRoute.Enqueue(questBox[i]);

        currentQuest = questRoute.Dequeue();
    }

    public void QuestItemCheck()
    {
        for(int i = 0; i < currentQuest.questInfoDatas.Length; i++)
            if (currentQuest.questInfoDatas[i].questType == QuestType.item)
                if (currentQuest.questInfoDatas[i].item.count >= currentQuest.questInfoDatas[i].itemCompleteCount)
                    currentQuest.questInfoDatas[i].isClear = true;
        QuestClearCheck();
    }

    public void QuestPositionCheck(string name)
    {
        for(int i = 0; i < currentQuest.questInfoDatas.Length; i++)
            if (currentQuest.questInfoDatas[i].questType == QuestType.Position)
                if (currentQuest.questInfoDatas[i].position == name)
                    currentQuest.questInfoDatas[i].isClear = true;
        QuestClearCheck();
    }

    public void QuestChatCheck(string key)
    {
        for (int i = 0; i < currentQuest.questInfoDatas.Length; i++)
            if (currentQuest.questInfoDatas[i].chatKey == key)
            {
                currentQuest.questInfoDatas[i].isClear = true;
                QuestClearCheck();
            }
    }








        //public void InputQuest(QuestInfo quest)
        //{
        //    if (quest.isProgress) return;
        //    if (quest.isClear) return;

        //    quest.isProgress = true;
        //    questDictionary.Add(quest.title, quest);


        //    if (questDictionary.Count == 0)
        //    {
        //        quest.isProgress = true;
        //        questDictionary.Add(quest.title, quest);
        //    }
        //    else
        //    {
        //        foreach (KeyValuePair<string, QuestInfo> info in questDictionary)
        //        {
        //            if (quest.questIndex == 0)
        //            {
        //                quest.isProgress = true;
        //                questDictionary.Add(quest.title, quest);
        //                break;
        //            }

        //            QuestInfo test = info.Value;

        //            if (test.questIndex == quest.questIndex - 1)
        //            {
        //                if (test.isClear)
        //                {
        //                    quest.isProgress = true;
        //                    questDictionary.Add(quest.title, quest);
        //                    return;
        //                }
        //            }
        //        }
        //    }


        //}

        //private void ClearCheck(string _key)
        //{
        //    questDictionary[_key].isProgress = false;
        //    questDictionary[_key].isClear = true;

        //}

        //private IEnumerator CheckPosition(QuestInfo temp)
        //{
        //    yield return null;
        //    while(true)
        //    {
        //        if (Vector3.Distance(player.transform.position, temp.position.position) < 10f)
        //        {
        //            ClearCheck(temp.title);
        //            Debug.Log("������ ����Ʈ Ŭ����");
        //            break;
        //        }
        //    }
        //}

        //public void QuestCheck()
        //{
        //    if (questDictionary.Count == 0) return;
        //    foreach (KeyValuePair<string, QuestInfo> info in questDictionary)
        //    {
        //        QuestInfo temp = info.Value;

        //        if (!questDictionary.ContainsKey(temp.title)) return;
        //        if (questDictionary[temp.title].isClear) continue;
        //        if (!questDictionary[temp.title].isProgress) continue;

        //        if (temp.questType == QuestType.Chat)
        //        {
        //            Debug.Log("��ȭ ����Ʈ üũ");
        //            if (temp.isChat)
        //            {
        //                ClearCheck(temp.title);
        //                Debug.Log("��ȣ�ۿ� ����Ʈ Ŭ����");
        //            }
        //        }
        //        else if (temp.questType == QuestType.Position)
        //        {
        //            Debug.Log("������ ����Ʈ üũ");
        //            StartCoroutine(CheckPosition(temp));
        //        }
        //        else if (temp.questType == QuestType.item)
        //        {
        //            Debug.Log("������ ����Ʈ üũ");
        //            if (!InventoryTitle.instance.itemMap.ContainsKey(temp.item.name)) return;
        //            temp.itemCurrentCount = InventoryTitle.instance.itemMap[temp.item.name].count;
        //            if (temp.itemCurrentCount < temp.itemCompleteCount) return;

        //            Debug.Log("������ ȹ�� ����Ʈ Ŭ����");
        //            ClearCheck(temp.title);
        //        }
        //    }
        //}


    }
