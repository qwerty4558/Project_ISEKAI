using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QuestTitle : SerializedMonoBehaviour
{
    public static QuestTitle instance;

    [SerializeField] private Dictionary<string, QuestInfo> questMap = new Dictionary<string, QuestInfo>();

    [SerializeField] private QuestUI questUI;
    [SerializeField] private DOTweenAnimation titleDotweenAni;
    [SerializeField] private DOTweenAnimation clearDotweenAni;
    [SerializeField] private UnityEngine.UI.Outline questOutline;

    private bool isQuestActive = false;
    
    public QuestInfo currentQuest;
    public QuestInfo tempQuest;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            currentQuest = null;
            tempQuest = null; 
            DontDestroyOnLoad(gameObject);
            titleDotweenAni = GetComponentInChildren<DOTweenAnimation>();
        }
        else
        {
            Destroy(gameObject);
        }
        //QuestInput("��������");
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) QuestActive();

        if(currentQuest == null && tempQuest != null)
        {
            QuestInput(tempQuest.questInfoDatas[0].title);
            tempQuest = null;
        }
    }

    private void QuestActive()
    {
        if(isQuestActive)
        {
            titleDotweenAni.DOPauseAllById("false");
            titleDotweenAni.DORestartById("true");
            isQuestActive = !isQuestActive;
        }
        else if(!isQuestActive)
        {
            titleDotweenAni.DOPauseAllById("true");
            titleDotweenAni.DORestartById("false");
            isQuestActive = !isQuestActive;
        }
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
        StartCoroutine(QuestClearAni());
        Debug.Log("����Ʈ �Ϸ�");
    }

    private IEnumerator QuestClearAni()
    {
        clearDotweenAni.DORestartById("in");
        yield return new WaitForSeconds(1f);
        clearDotweenAni.DORestartById("out");
    }

    public void QuestInput(string id)
    {
        if (currentQuest != null)
        {
            tempQuest = questMap[id];
            tempQuest.questInfoDatas[0].title = id;
            return;
        }

        currentQuest = questMap[id];
        questUI.ReRoadUI(currentQuest);
        isQuestActive = false;
        QuestActive();
        StartCoroutine(QuestInptAnim());
    }

    private IEnumerator QuestInptAnim()
    {
        for(int i = 0; i < 3; i++)
        {
            questOutline.enabled = true;
            yield return new WaitForSeconds(0.3f);
            questOutline.enabled = false;
            yield return new WaitForSeconds(0.3f);
        }
        if (isQuestActive) QuestActive();
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
                    Debug.Log("������ ����Ʈ Ȯ��");
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
            //        Debug.Log("������ ����Ʈ Ȯ��");
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
                    Debug.Log("������ ����Ʈ Ȯ��");
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
        //            Debug.Log("������ ����Ʈ Ȯ��");
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
                Debug.Log("��ȭ ����Ʈ Ȯ��");
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
        //        Debug.Log("��ȭ ����Ʈ Ȯ��");
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
