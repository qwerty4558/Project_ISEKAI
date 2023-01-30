using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

public class ItemManager : MonoBehaviour
{
    public TextAsset itemDatabase; // 재료 아이템
    public TextAsset resultItemDatabase; // 제작완료아이템

    public List<Ingredient_Item> all_Items; 
    public List<Ingredient_Item> my_Items;
    
    public List<Result_Item> result_List;
    

    public Slot[] slot; // 인벤토리의 슬롯

    public Slot[] createSlot; // 초합창의 슬롯
    
    public Slot resultSlot; // 조합의 결과    


    private void Awake()
    {

    }
    void Start()
    {
        InitItemDatabase();
        InitResultItemDatabase();
        Save();
        Load();
    }

    private void InitResultItemDatabase()
    {
        string[] line = resultItemDatabase.text.Substring(0, resultItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            result_List.Add(new Result_Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]));
        }
    }

    private void InitItemDatabase()
    {
        string[] line = itemDatabase.text.Substring(0, itemDatabase.text.Length - 1).Split('\n');
        int length = line.Length;
        for (int i = 0; i < length; ++i)
        {
            string[] row = line[i].Split('\t');
            all_Items.Add(new Ingredient_Item(row[0], row[1], row[2], row[3]));
        }
    }

    public void ViewItem()
    {
        for (int i = 0; i < my_Items.Count; i++)
        {
            slot[i].item_Id = my_Items[i].id;
            slot[i].item_Name = my_Items[i].name;
            slot[i].item_NameKR = my_Items[i].name_KR;
        }

        // 인벤토리 슬롯 출력
        int slot_lenght = slot.Length;
        for (int i = 0; i < slot_lenght; i++)
        {             
            slot[i].text.text = i < my_Items.Count ? my_Items[i].name_KR + slot[i].item_Count : "";
        }

        // 조합 아이템 슬롯 출력
        createSlot[0].text.text = createSlot[0].item_NameKR + createSlot[0].item_Count;
        createSlot[1].text.text = createSlot[1].item_NameKR + createSlot[1].item_Count;

        // 결과 아이템 슬롯 출력
        resultSlot.text.text = resultSlot.item_NameKR + resultSlot.item_Price;
    }

    public void SlotClick(int slotNum)
    {
        if (slot[slotNum].item_Count <= 0) return;

        if (createSlot[0].item_Count == 0 || createSlot[0].item_Id == slot[slotNum].item_Id)
        {
            slot[slotNum].item_Count--;

            createSlot[0].item_Id = slot[slotNum].item_Id;
            createSlot[0].item_NameKR = slot[slotNum].item_NameKR;
            createSlot[0].item_Count++;

            ViewItem();
        }
        else if(createSlot[1].item_Count == 0 || createSlot[1].item_Id == slot[slotNum].item_Id)
        {
            slot[slotNum].item_Count--;

            createSlot[1].item_Id = slot[slotNum].item_Id;
            createSlot[1].item_NameKR = slot[slotNum].item_NameKR;
            createSlot[1].item_Count++;
            ViewItem();            
        }
        CombinationItem(createSlot[0], createSlot[1]);
        ViewItem();
        Save();
    }

    public void CombinationItem(Slot com1, Slot com2)
    {
        int result_List_Lenght = result_List.Count;
        
        for(int i = 0; i < result_List_Lenght; i++)
        {
            if (result_List[i].main_Ingredient_TID ==  com1.item_Id && result_List[i].sub_Ingredient_TID == com2.item_Id)
            {
                if (result_List[i].main_Count == com1.item_Count && result_List[i].sub_Count == com2.item_Count)
                {
                    resultSlot.item_NameKR = result_List[i].result_Item_Name;
                    resultSlot.item_Price = result_List[i].result_Item_Price;
                }                
                ViewItem();
            }
        }
    }

    public void RemoveSlotItem(int resultSlotNum)
    {
        if (createSlot[resultSlotNum].item_Count == 0)
        {
            createSlot[resultSlotNum].ClearSlot();
            ViewItem();
            return;
        }
        createSlot[resultSlotNum].item_Count--;
        ViewItem();
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(all_Items);
        File.WriteAllText(Application.dataPath + "/MainProjectFolder/Item/ItemDatabase/MyItemText.txt", jdata);        
        //TabClick(curType);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/MainProjectFolder/Item/ItemDatabase/MyItemText.txt");
        my_Items = JsonConvert.DeserializeObject<List<Ingredient_Item>>(jdata);

        ViewItem();
    }

    public void AllItemPlus_1()
    {
        int item_Lenght = slot.Length;

        for(int i = 0; i < item_Lenght; i++)
        {
            slot[i].GetComponent<Slot>().item_Count++;
        }
        ViewItem();
    }
}
