using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int item_Id;
    public int item_Count = 0;
    public string item_Name;
    public string item_NameKR;
    public string item_Image_FileName;
    public int item_Price;
    public TMP_Text count_Text;
    public Image item_Image;
    public Sprite default_Sprite;
    

    private void Awake()
    {
        SetDefaultSprite();
    }

    public void ImageRead()
    {
        item_Image.sprite = Resources.Load<Sprite>(item_Image_FileName);
    }

    public void SetDefaultSprite()
    {
        item_Image.sprite = default_Sprite;
    }

    public void ClearSlot()
    {
        item_Id = 0;
        item_Count = 0;
        item_Name = null;
        item_NameKR = null;
        SetDefaultSprite();
    }
}
