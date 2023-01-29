using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int item_Id;
    public int item_Count = 0;
    public string item_Name;
    public string item_NameKR;
    public Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }
}
