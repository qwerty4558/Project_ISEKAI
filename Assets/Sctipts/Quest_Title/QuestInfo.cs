using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QuestType
{
    Chat, Position, item
}



[System.Serializable]
public class QuestInfo
{
    
    [System.Serializable]
    public class QuestInfoData
    {
        [Header("해당되지 않은 타입의 정보는 입력 필요 없음")]
        [Header("퀘스트 필수 정보 (타입, 이름, 설명)")]
        public QuestType questType;
        public string title;
        public string description;
        public bool isClear;

        [Header("위치퀘스트 전용[Position] (도달해야하는 위치의 이름)")]
        public string position;

        [Header("아이템퀘스트 전용[Item] (목표 아이템, 목표갯수, 현재갯수는 x)")]
        public Ingredient_Item item;
        public int itemCompleteCount;
        public int itemCurrentCount;

        [Header("대화퀘스트 전용[Chat] (대화 키값)")]
        public string chatKey;

        public UnityEvent action;
    }
    [Header("------------------------퀘스트 단위------------------------")]
    public QuestInfoData[] questInfoDatas = new QuestInfoData[1];
    public UnityEvent action;
}
