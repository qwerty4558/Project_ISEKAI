using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class Combination_Item : MonoBehaviour
{
    public  TextAsset resultItemDatabase; // 제작완료아이템
    public  List<Result_Item> result_List;
    public  Slot[] createSlot; // 조합창의 슬롯
    public  Slot resultSlot; // 조합의 결과    
    public Text resultText; // 랜덤으로 3개 선택된 결과템의 정보

    [SerializeField]
    public GameObject parentObject;


    public EWorkTable workTable = EWorkTable.None;

    private float ingredient_Gauge = 0f;

    int _main_Ingrident;
    int _sub_Ingrident;

    public void Awake()
    {
        InitResultItemDatabase();
        RemoveSlotItem();
    }

    public void Update()
    {
        ProcessCirculator();
        ViewItem();
    }

    private void ProcessCirculator()
    {
        ingredient_Gauge = (float)(_main_Ingrident + _sub_Ingrident);
       // prograss_Circular.fillAmount = Mathf.Lerp(prograss_Circular.fillAmount, gauge / max_Gauge, Time.deltaTime);

    }

    private void InitResultItemDatabase()
    {
        string[] line = resultItemDatabase.text.Substring(0, resultItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split("\t");
            result_List.Add(new Result_Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]));
        }
    }

    public void SlotClick(int slotNum)
    {
        if (Inventory.Instance.slot[slotNum].item_Id == 0) return;
        if (Inventory.Instance.slot[slotNum].item_Count <= 0) return;

        if (createSlot[0].item_Count == 0 || createSlot[0].item_Id == Inventory.Instance.slot[slotNum].item_Id)
        {
            Inventory.Instance.slot[slotNum].item_Count--;

            createSlot[0].item_Id                               = Inventory.Instance.slot[slotNum].item_Id;
            createSlot[0].item_NameKR                    = Inventory.Instance.slot[slotNum].item_NameKR;
            createSlot[0].item_Image_FileName      = Inventory.Instance.slot[slotNum].item_Image_FileName;
            createSlot[0].ImageRead();  
            createSlot[0].item_Count++;

            ViewItem();
        }
        else if (createSlot[1].item_Count == 0 || createSlot[1].item_Id == Inventory.Instance.slot[slotNum].item_Id)
        {
            Inventory.Instance.slot[slotNum].item_Count--;

            createSlot[1].item_Id                              = Inventory.Instance.slot[slotNum].item_Id;
            createSlot[1].item_NameKR                   = Inventory.Instance.slot[slotNum].item_NameKR;
            createSlot[1].item_Image_FileName     = Inventory.Instance.slot[slotNum].item_Image_FileName;
            createSlot[1].ImageRead();
            createSlot[1].item_Count++;
            ViewItem();
        }
        CombinationItem(createSlot[0], createSlot[1]);
        ViewItem();        
    }
    private void CombinationItem(Slot com1, Slot com2)
    {
        int result_List_Lenght = result_List.Count;


        for (int i = 0; i < result_List_Lenght; i++)
        {
            if (result_List[i].main_Ingredient_TID == com1.item_Id && result_List[i].sub_Ingredient_TID == com2.item_Id)
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

                if (result_List[i].main_Count == _main_Ingrident && result_List[i].sub_Count == _sub_Ingrident)
                {
                    resultSlot.item_Id      = result_List[i].result_ID;
                    resultSlot.item_NameKR  = result_List[i].result_Item_Name;
                    resultSlot.item_Price   = result_List[i].result_Item_Price;
                }
                ViewItem();
            }
        }
    }

    public void SellItem()
    {
        if (resultSlot.item_Id != 0)
        {
            //GameManager.Instance.player_Money += resultSlot.item_Price;
            //GameManager.Instance.selling_Count--;
            createSlot[0].ClearSlot();
            createSlot[1].ClearSlot();
            resultSlot.ClearSlot();
            _main_Ingrident = 0;
            _sub_Ingrident = 0;
            ViewItem();
        }
    }
    public void RemoveSlotItem()
    {
        createSlot[0].item_Count = 0;
        createSlot[1].item_Count = 0;

        createSlot[0].ClearSlot();
        createSlot[1].ClearSlot();

        resultSlot.ClearSlot();
        resultSlot.count_Text.text = " ";
        _main_Ingrident = 0;
        _sub_Ingrident = 0;
        ViewItem();
    }

    public void ViewItem()
    {
        // 조합 아이템 슬롯 출력
        createSlot[0].count_Text.text =  createSlot[0].item_Count.ToString();
        createSlot[1].count_Text.text =  createSlot[1].item_Count.ToString();

        // 결과 아이템 슬롯 출력
        resultSlot.count_Text.text = resultSlot.item_NameKR + "\n" + resultSlot.item_Price;
    }




    public void PrintRandomResultItemInfo()
    {
        List<int> randomIndexes = GenerateRandomList(3); // 중복 없이 결과템 3개 랜덤 선출

        Debug.Log(string.Join(", ", randomIndexes));


        Transform parentTransform = parentObject.transform;

        for (int i = 0; i < 3; i++)
        {
            int ItemNameNum =       ((i + 1) * 3) - 3;
            int result_Item_Price = ((i + 1) * 3) - 2;
            int icon_File_Name =    ((i + 1) * 3) - 1;

            Result_Item item = result_List[randomIndexes[i]];

            /*
            Debug.Log("제작장소: " + item.grup_ID);
            Debug.Log("주재료: " + item.main_Ingredient_TID);
            Debug.Log("주재료 요구량: " + item.main_Count);
            Debug.Log("부재료: " + item.sub_Ingredient_TID);
            Debug.Log("부재료 요구량: " + item.sub_Count);
            Debug.Log("결과템 ID: " + item.result_ID);
            Debug.Log("결과템 한글 이름: " + item.result_Item_Name);
            Debug.Log("가격: " + item.result_Item_Price);
            Debug.Log("결과템 아이콘 파일 이름: " + item.icon_File_Name);
            Debug.Log("------------------------");
            */

            Transform Item = parentTransform.GetChild(ItemNameNum);
            Text childItem = Item.GetComponent<Text>();
                // childItem 값을 변경한다
                childItem.text = item.result_ID.ToString();

            Transform Price = parentTransform.GetChild(result_Item_Price);
            Text childPrice = Price.GetComponent<Text>();
                // childPrice 값을 변경한다
                childPrice.text = item.result_Item_Price.ToString();

            Transform File_Name = parentTransform.GetChild(icon_File_Name);
            Text childFile_Name = File_Name.GetComponent<Text>();
            // childFile_Name 값을 변경한다
            childFile_Name.text = item.icon_File_Name.ToString();

        }
    }

    List<int> GenerateRandomList(int count)
    {
        int List_Lenght = result_List.Count;

        List<int> resultList = new List<int>();

        while (resultList.Count < count)
        {
            int randomNumber = UnityEngine.Random.Range(List_Lenght / 2, List_Lenght);
            if (!resultList.Contains(randomNumber))
            {
                resultList.Add(randomNumber);
            }
        }
        return resultList;
    }
}
