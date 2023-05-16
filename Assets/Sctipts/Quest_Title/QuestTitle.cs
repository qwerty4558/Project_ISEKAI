using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class QuestTitle : SerializedMonoBehaviour
{
    public static QuestTitle instance;

    [SerializeField] private Dictionary<string, QuestInfo> questMap = new Dictionary<string, QuestInfo>();

    [SerializeField] private QuestUI questUI;

    [HideInInspector]
    public QuestInfo currentQuest;


    private void Awake()
    {
        instance = this;
        currentQuest = null;
        //QuestInput("고블린귀");
    }

    private void QuestClearCheck()
    {
        questUI.UpdateUI();

        for(int i = 0; i < currentQuest.questInfoDatas.Length; i++)
            if (!currentQuest.questInfoDatas[i].isClear) return;

        if(currentQuest.action != null)
            currentQuest.action.Invoke();

        currentQuest = null;
        questUI.SetActiveQuest(6, false);
        Debug.Log("퀘스트 완료");
    }

    public void QuestInput(string id)
    {
        if (currentQuest != null) return;

        currentQuest = questMap[id];
        questUI.ReRoadUI(currentQuest);
    }

    public void QuestItemCheck()
    {
        if (currentQuest == null) return;

        int i = 0;
        while (currentQuest != null && i < currentQuest.questInfoDatas.Length)
        {
            
            if (currentQuest.questInfoDatas[i].questType == QuestType.item)
                if (currentQuest.questInfoDatas[i].item.count >= currentQuest.questInfoDatas[i].itemCompleteCount)
                {
                    if (currentQuest.questInfoDatas[i].isClear)
                    {
                        i++;
                        continue;
                    }
                    currentQuest.questInfoDatas[i].isClear = true;
                    Debug.Log("아이템 퀘스트 확인");
                    if (currentQuest.questInfoDatas[i].action != null)
                        currentQuest.questInfoDatas[i].action.Invoke();
                }
            i++;
        }

            //for (int i = 0; i < currentQuest.questInfoDatas.Length; i++)
            //if (currentQuest.questInfoDatas[i].questType == QuestType.item)
            //    if (currentQuest.questInfoDatas[i].item.count >= currentQuest.questInfoDatas[i].itemCompleteCount)
            //    {
            //        if (currentQuest.questInfoDatas[i].isClear) continue;
            //        currentQuest.questInfoDatas[i].isClear = true;
            //        Debug.Log("아이템 퀘스트 확인");
            //        if (currentQuest.questInfoDatas[i].action != null)
            //            currentQuest.questInfoDatas[i].action.Invoke();
            //    }
        
        QuestClearCheck();
    }

    public void QuestPositionCheck(string name)
    {
        if (currentQuest == null) return;

        int i = 0;
        while(currentQuest != null && i < currentQuest.questInfoDatas.Length)
        {
            if (currentQuest.questInfoDatas[i].questType == QuestType.Position)
                if (currentQuest.questInfoDatas[i].position == name)
                {
                    if (currentQuest.questInfoDatas[i].isClear)
                    { 
                        i++;
                        continue; 
                    }
                    currentQuest.questInfoDatas[i].isClear = true;
                    Debug.Log("포지션 퀘스트 확인");
                    if (currentQuest.questInfoDatas[i].action != null)
                        currentQuest.questInfoDatas[i].action.Invoke();
                }
            i++;
        }

        //for (int i = 0; i < currentQuest.questInfoDatas.Length; i++)
        //    if (currentQuest.questInfoDatas[i].questType == QuestType.Position)
        //        if (currentQuest.questInfoDatas[i].position == name)
        //        {
        //            if (currentQuest.questInfoDatas[i].isClear) continue;
        //            currentQuest.questInfoDatas[i].isClear = true;
        //            Debug.Log("포지션 퀘스트 확인");
        //            if (currentQuest.questInfoDatas[i].action != null)
        //                currentQuest.questInfoDatas[i].action.Invoke();
        //        }
        
        QuestClearCheck();
    }

    public void QuestChatCheck(string key)
    {
        if (currentQuest == null) return;

        int i = 0;
        while(currentQuest != null && i < currentQuest.questInfoDatas.Length)
        {
            if (currentQuest.questInfoDatas[i].chatKey == key)
            {
                if (currentQuest.questInfoDatas[i].isClear)
                {
                    i++;
                    continue;
                }
                currentQuest.questInfoDatas[i].isClear = true;
                Debug.Log("대화 퀘스트 확인");
                if (currentQuest.questInfoDatas[i].action != null)
                    currentQuest.questInfoDatas[i].action.Invoke();
                QuestClearCheck();
            }
            i++;
        }


        //for (int i = 0; i < currentQuest.questInfoDatas.Length; i++)
        //    if (currentQuest.questInfoDatas[i].chatKey == key)
        //    {
        //        if (currentQuest.questInfoDatas[i].isClear) continue;
        //        currentQuest.questInfoDatas[i].isClear = true;
        //        Debug.Log("대화 퀘스트 확인");
        //        if (currentQuest.questInfoDatas[i].action != null)
        //            currentQuest.questInfoDatas[i].action.Invoke();
        //        QuestClearCheck();
        //    }
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
        //            Debug.Log("포지션 퀘스트 클리어");
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
        //            Debug.Log("대화 퀘스트 체크");
        //            if (temp.isChat)
        //            {
        //                ClearCheck(temp.title);
        //                Debug.Log("상호작용 퀘스트 클리어");
        //            }
        //        }
        //        else if (temp.questType == QuestType.Position)
        //        {
        //            Debug.Log("포지션 퀘스트 체크");
        //            StartCoroutine(CheckPosition(temp));
        //        }
        //        else if (temp.questType == QuestType.item)
        //        {
        //            Debug.Log("아이템 퀘스트 체크");
        //            if (!InventoryTitle.instance.itemMap.ContainsKey(temp.item.name)) return;
        //            temp.itemCurrentCount = InventoryTitle.instance.itemMap[temp.item.name].count;
        //            if (temp.itemCurrentCount < temp.itemCompleteCount) return;

        //            Debug.Log("아이템 획득 퀘스트 클리어");
        //            ClearCheck(temp.title);
        //        }
        //    }
        //}


    }
