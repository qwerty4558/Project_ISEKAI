using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextAsset Quest_Database; // Äù½ºÆ® Á¤º¸
    public List<Quest> quest_List;

    [SerializeField]
    public GameObject FirstQuestSlot;
    public GameObject SecondQuestSlot;
    public GameObject ThirdQuestSlot;

    public void Awake()
    {

    }

    public void Update()
    {

    }


    
}
   
