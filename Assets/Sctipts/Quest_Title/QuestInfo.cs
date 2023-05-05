using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestInfo : MonoBehaviour
{
    //위치 퀘스트 전용
    public Transform position;

    //아이템 퀘스트 전용
    public Ingredient_Item[] items;
    public int questIndex;
    public int itemCompleteCount;

    //대화 퀘스트 전용
    public bool[] isChats;

    public QuestType questType;
    public string title; //퀘스트 이름
    public string description; //퀘스트 설명
    public string trigger; //퀘스트 조건
    public bool isClear;
    public bool isProgress;
}
