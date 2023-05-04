using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestInfo : MonoBehaviour
{
    //��ġ ����Ʈ ����
    public Transform position;

    //������ ����Ʈ ����
    public Ingredient_Item[] items;
    public int questIndex;
    public int itemCompleteCount;

    //��ȭ ����Ʈ ����
    public bool[] isChats;

    public QuestType questType;
    public string title; //����Ʈ �̸�
    public string description; //����Ʈ ����
    public string trigger; //����Ʈ ����
    public bool isClear;
    public bool isProgress;
}
