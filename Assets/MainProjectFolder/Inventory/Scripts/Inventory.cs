using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : SingletonMonoBehaviour<Inventory>
{
    public List<Ingredient_Item> my_Items;

    public Slot[] slot; // 인벤토리의 슬롯

    public void Start()
    {
        if(my_Items == null)
        {
            for (int i = 0; i < slot.Length; ++i)
            {

            }
        }        
    }

    public void ViewItem()
    {
        for (int i = 0; i < my_Items.Count; i++) // 저장되어 있는 아이템 리스트 들을 각 슬롯에 저장
        {
            slot[i].item_Id                          = my_Items[i].id;
            slot[i].item_Name                    = my_Items[i].name;
            slot[i].item_NameKR                = my_Items[i].name_KR;
            slot[i].item_Image_FileName = my_Items[i].icon_File_Name;
        }

        // 인벤토리 슬롯 출력
        int slot_lenght = slot.Length;
        for (int i = 0; i < slot_lenght; i++)
        {
            if (slot[i].item_Id != 0)
            {
                slot[i].count_Text.text = i < my_Items.Count ? slot[i].item_Count.ToString() : "";
                slot[i].ImageRead();
            }
            else
            {
                slot[i].SetDefaultSprite();
                slot[i].count_Text.text = null;
            }
        }
    }

    public void AllPlusItem() 
    {
        int slotLenght = slot.Length;

        for(int i = 0; i < slotLenght; ++i)
        {
            slot[i].item_Count++;
        }
    }

    public void GetItem()
    {

    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/MainProjectFolder/Item/ItemDatabase/MyItemText.txt");
        my_Items = JsonConvert.DeserializeObject<List<Ingredient_Item>>(jdata);

        ViewItem();
    }
}
