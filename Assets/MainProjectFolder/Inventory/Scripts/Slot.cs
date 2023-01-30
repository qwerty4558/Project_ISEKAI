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

    public int item_Price;
    public Text text;
    public Image item_Image;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        item_Image = GetComponentInChildren<Image>();
    }

    public void ClearSlot()
    {
        item_Id = 0;
        item_Count = 0;
        item_Name = null;
        item_NameKR = null;
    }
}
