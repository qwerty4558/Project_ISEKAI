using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using System;

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
    [Space]
    public Image prograss_Circular;
    private float max_Gauge = 1f;
    private float gauge = 0f;
    private float ingredient_Gauge = 0f;

    int _main_Ingrident;
    int _sub_Ingrident;

    private static ItemManager _instance = null;
    


    private void Awake()
    {
        if(null == _instance)
        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public static ItemManager Instance
    {
        get
        {
            if(null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    private void Start()
    {
        InitItemDatabase();
        InitResultItemDatabase();
        Save();
        Load();
    }

    private void Update()
    {
        ProcessCirculator();
    }

    private void ProcessCirculator()
    {
        ingredient_Gauge = (float)(_main_Ingrident + _sub_Ingrident);
        gauge = ingredient_Gauge;
        prograss_Circular.fillAmount = Mathf.Lerp(prograss_Circular.fillAmount, gauge / max_Gauge, Time.deltaTime);

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
            row[3] = row[3].Replace("\r", "");
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
            slot[i].item_Image_FileName = my_Items[i].icon_File_Name;
        }

        // 인벤토리 슬롯 출력
        int slot_lenght = slot.Length;
        for (int i = 0; i < slot_lenght; i++)
        {
            if (slot[i].item_Id != 0)
            {
                slot[i].text.text = i < my_Items.Count ? my_Items[i].name_KR + slot[i].item_Count : "";
                slot[i].ImageRead();
            }
            else
            {
                slot[i].SetDefaultSprite();
                slot[i].text.text = null;
            }
        }

        // 조합 아이템 슬롯 출력
        createSlot[0].text.text = createSlot[0].item_NameKR + createSlot[0].item_Count;
        createSlot[1].text.text = createSlot[1].item_NameKR + createSlot[1].item_Count;

        // 결과 아이템 슬롯 출력
        resultSlot.text.text = resultSlot.item_NameKR + "\n" +resultSlot.item_Price;
    }

    public void SlotClick(int slotNum)
    {
        if (slot[slotNum].item_Id == 0) return;
        if (slot[slotNum].item_Count <= 0) return;

        if (createSlot[0].item_Count == 0 || createSlot[0].item_Id == slot[slotNum].item_Id)
        {
            slot[slotNum].item_Count--;

            createSlot[0].item_Id = slot[slotNum].item_Id;
            createSlot[0].item_NameKR = slot[slotNum].item_NameKR;
            createSlot[0].item_Image_FileName = slot[slotNum].item_Image_FileName;
            createSlot[0].ImageRead();
            createSlot[0].item_Count++;

            ViewItem();
        }
        else if(createSlot[1].item_Count == 0 || createSlot[1].item_Id == slot[slotNum].item_Id)
        {
            slot[slotNum].item_Count--;

            createSlot[1].item_Id = slot[slotNum].item_Id;
            createSlot[1].item_NameKR = slot[slotNum].item_NameKR;
            createSlot[1].item_Image_FileName = slot[slotNum].item_Image_FileName;
            createSlot[1].ImageRead();
            createSlot[1].item_Count++;
            ViewItem();            
        }
        CombinationItem(createSlot[0], createSlot[1]);
        ViewItem();
        Save();
    }

    private void CombinationItem(Slot com1, Slot com2)
    {
        int result_List_Lenght = result_List.Count;

        
        for (int i = 0; i < result_List_Lenght; i++)
        {
            if (result_List[i].main_Ingredient_TID ==  com1.item_Id && result_List[i].sub_Ingredient_TID == com2.item_Id)
            {


                if (com1.item_Count <= result_List[i].main_Count)
                {
                    _main_Ingrident = com1.item_Count;
                }
                else return;
                if (com2.item_Count <= result_List[i].sub_Count)
                {
                    _sub_Ingrident = com2.item_Count;
                }
                else return;
                
                max_Gauge = (float)(result_List[i].main_Count + result_List[i].sub_Count);
                if (result_List[i].main_Count == _main_Ingrident && result_List[i].sub_Count == _sub_Ingrident)
                {
                    resultSlot.item_Id = result_List[i].result_ID;
                    resultSlot.item_NameKR = result_List[i].result_Item_Name;
                    resultSlot.item_Price = result_List[i].result_Item_Price;
                }                       
                ViewItem();
            }            
        }
    }

    public void SellItem()
    {
        GameManager playerInfo = FindObjectOfType<GameManager>();
        if(resultSlot.item_Id != 0)
        {
            playerInfo.player_Money += resultSlot.item_Price;
            playerInfo.selling_Count--;
            createSlot[0].ClearSlot();
            createSlot[1].ClearSlot();
            resultSlot.ClearSlot();
            _main_Ingrident = 0;
            _sub_Ingrident = 0;
            
        }
    }
    

    public void RemoveSlotItem()
    {
        createSlot[0].item_Count = 0;
        createSlot[1].item_Count = 0;

        createSlot[0].ClearSlot();
        createSlot[1].ClearSlot();

        _main_Ingrident = 0;
        _sub_Ingrident = 0;

        ProcessCirculator();
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
            slot[i].item_Count++;
        }
        ViewItem();
    }
}