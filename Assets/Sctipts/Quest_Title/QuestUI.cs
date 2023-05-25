using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject title_obj;
    [SerializeField] private TextMeshProUGUI[] quest_Texts;
    private QuestInfo currentQuest;

    private void Awake()
    {
        //currentQuest = QuestTitle.instance.currentQuest;
    }

    public void SetActiveQuest(int index, bool _bool)
    {
        for (int i = 0; i < index; i++)
            quest_Texts[i].gameObject.SetActive(_bool);
    }

    public void ReRoadUI(QuestInfo quest)
    {
        currentQuest = quest;
        SetActiveQuest(quest_Texts.Length, false);
        SetActiveQuest(currentQuest.questInfoDatas.Length, true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        for(int i = 0; i < currentQuest.questInfoDatas.Length; i++)
        {
            if (currentQuest.questInfoDatas[i].questType == QuestType.item)
            {
                quest_Texts[i].text = currentQuest.questInfoDatas[i].description + " ("
                                    + currentQuest.questInfoDatas[i].item.count.ToString() + "/"
                                    + currentQuest.questInfoDatas[i].itemCompleteCount.ToString() + ")";
            }
            else
            {
                if (currentQuest.questInfoDatas[i].isClear) quest_Texts[i].text = currentQuest.questInfoDatas[i].description + " (1/1)";
                else quest_Texts[i].text = currentQuest.questInfoDatas[i].description + " (0/1)";
            }
        }
    }
}
