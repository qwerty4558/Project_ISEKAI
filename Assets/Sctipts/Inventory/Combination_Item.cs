using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;


public class Combination_Item : MonoBehaviour
{
    //public  TextAsset resultItemDatabase; // ���ۿϷ������
    //public  List<Result_Item> result_List;
    //public  Slot[] createSlot; // ����â�� ����
    //public  Slot resultSlot; // ������ ���    
    //public Text resultText; // �������� 3�� ���õ� ������� ����

    //[SerializeField]
    //public GameObject FirstQuestSlot;
    //public GameObject SecondQuestSlot;
    //public GameObject ThirdQuestSlot;



    //private float ingredient_Gauge = 0f;

    //int _main_Ingrident;
    //int _sub_Ingrident;

    //public void Awake()
    //{
    //    InitResultItemDatabase();
    //    RemoveSlotItem();
    //}

    //public void Update()
    //{
    //    ProcessCirculator();
    //    ViewItem();
    //}

    //private void ProcessCirculator()
    //{
    //    ingredient_Gauge = (float)(_main_Ingrident + _sub_Ingrident);
    //   // prograss_Circular.fillAmount = Mathf.Lerp(prograss_Circular.fillAmount, gauge / max_Gauge, Time.deltaTime);

    //}

    //private void InitResultItemDatabase()
    //{
    //    string[] line = resultItemDatabase.text.Substring(0, resultItemDatabase.text.Length - 1).Split('\n');
    //    for (int i = 0; i < line.Length; i++)
    //    {
    //        string[] row = line[i].Split("\t");
    //        result_List.Add(new Result_Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]));
    //    }
    //}

    //public void SlotClick(int slotNum)
    //{
    //    if (Inventory.Instance.slot[slotNum].item_Id == 0) return;
    //    if (Inventory.Instance.slot[slotNum].item_Count <= 0) return;

    //    if (createSlot[0].item_Count == 0 || createSlot[0].item_Id == Inventory.Instance.slot[slotNum].item_Id)
    //    {
    //        Inventory.Instance.slot[slotNum].item_Count--;

    //        createSlot[0].item_Id                               = Inventory.Instance.slot[slotNum].item_Id;
    //        createSlot[0].item_NameKR                    = Inventory.Instance.slot[slotNum].item_NameKR;
    //        createSlot[0].item_Image_FileName      = Inventory.Instance.slot[slotNum].item_Image_FileName;
    //        createSlot[0].ImageRead();  
    //        createSlot[0].item_Count++;

    //        ViewItem();
    //    }
    //    else if (createSlot[1].item_Count == 0 || createSlot[1].item_Id == Inventory.Instance.slot[slotNum].item_Id)
    //    {
    //        Inventory.Instance.slot[slotNum].item_Count--;

    //        createSlot[1].item_Id                              = Inventory.Instance.slot[slotNum].item_Id;
    //        createSlot[1].item_NameKR                   = Inventory.Instance.slot[slotNum].item_NameKR;
    //        createSlot[1].item_Image_FileName     = Inventory.Instance.slot[slotNum].item_Image_FileName;
    //        createSlot[1].ImageRead();
    //        createSlot[1].item_Count++;
    //        ViewItem();
    //    }
    //    CombinationItem(createSlot[0], createSlot[1]);
    //    ViewItem();        
    //}
    //private void CombinationItem(Slot com1, Slot com2)
    //{
    //    int result_List_Lenght = result_List.Count;


    //    for (int i = 0; i < result_List_Lenght; i++)
    //    {
    //        if (result_List[i].main_Ingredient_TID == com1.item_Id && result_List[i].sub_Ingredient_TID == com2.item_Id)
    //        {
    //            if (com1.item_Count <= result_List[i].main_Count)
    //            {
    //                _main_Ingrident = com1.item_Count;
    //            }
    //            else return;

    //            if (com2.item_Count <= result_List[i].sub_Count)
    //            {
    //                _sub_Ingrident = com2.item_Count;
    //            }
    //            else return;

    //            if (result_List[i].main_Count == _main_Ingrident && result_List[i].sub_Count == _sub_Ingrident)
    //            {
    //                resultSlot.item_Id      = result_List[i].result_ID;
    //                resultSlot.item_NameKR  = result_List[i].result_Item_Name;
    //                resultSlot.item_Price   = result_List[i].result_Item_Price;
    //            }
    //            ViewItem();
    //        }
    //    }
    //}

    //public void SellItem()
    //{
    //    if (resultSlot.item_Id != 0)
    //    {
    //        //GameManager.Instance.player_Money += resultSlot.item_Price;
    //        //GameManager.Instance.selling_Count--;
    //        createSlot[0].ClearSlot();
    //        createSlot[1].ClearSlot();
    //        resultSlot.ClearSlot();
    //        _main_Ingrident = 0;
    //        _sub_Ingrident = 0;
    //        ViewItem();
    //    }
    //}
    //public void RemoveSlotItem()
    //{
    //    createSlot[0].item_Count = 0;
    //    createSlot[1].item_Count = 0;

    //    createSlot[0].ClearSlot();
    //    createSlot[1].ClearSlot();

    //    resultSlot.ClearSlot();
    //    resultSlot.count_Text.text = " ";
    //    _main_Ingrident = 0;
    //    _sub_Ingrident = 0;
    //    ViewItem();
    //}

    //public void ViewItem()
    //{
    //    // ���� ������ ���� ���
    //    createSlot[0].count_Text.text =  createSlot[0].item_Count.ToString();
    //    createSlot[1].count_Text.text =  createSlot[1].item_Count.ToString();

    //    // ��� ������ ���� ���
    //    resultSlot.count_Text.text = resultSlot.item_NameKR + "\n" + resultSlot.item_Price;
    //}



    ///*
    //public void PrintRandomResultItemInfo()
    //{
    //    List<int> randomIndexes = GenerateRandomList(3); // �ߺ� ���� ����� 3�� ���� ����

    //    Debug.Log(string.Join(", ", randomIndexes));


    //    Transform parentTransform = parentObject.transform;

    //    for (int i = 0; i < 3; i++)
    //    {
    //        int ItemNameNum =       ((i + 1) * 3) - 3;
    //        int result_Item_Price = ((i + 1) * 3) - 2;
    //        int icon_File_Name =    ((i + 1) * 3) - 1;

    //        Result_Item item = result_List[randomIndexes[i]];

            
    //        Debug.Log("�������: " + item.grup_ID);
    //        Debug.Log("�����: " + item.main_Ingredient_TID);
    //        Debug.Log("����� �䱸��: " + item.main_Count);
    //        Debug.Log("�����: " + item.sub_Ingredient_TID);
    //        Debug.Log("����� �䱸��: " + item.sub_Count);
    //        Debug.Log("����� ID: " + item.result_ID);
    //        Debug.Log("����� �ѱ� �̸�: " + item.result_Item_Name);
    //        Debug.Log("����: " + item.result_Item_Price);
    //        Debug.Log("����� ������ ���� �̸�: " + item.icon_File_Name);
    //        Debug.Log("------------------------");
            

    //        Transform Item = parentTransform.GetChild(ItemNameNum);
    //        Text childItem = Item.GetComponent<Text>();
    //            // childItem ���� �����Ѵ�
    //            childItem.text = item.result_ID.ToString();

    //        Transform Price = parentTransform.GetChild(result_Item_Price);
    //        Text childPrice = Price.GetComponent<Text>();
    //            // childPrice ���� �����Ѵ�
    //            childPrice.text = item.result_Item_Price.ToString();

    //        Transform File_Name = parentTransform.GetChild(icon_File_Name);
    //        Text childFile_Name = File_Name.GetComponent<Text>();
    //        // childFile_Name ���� �����Ѵ�
    //        childFile_Name.text = item.icon_File_Name.ToString();

    //    }
    //} */

    //List<int> GenerateRandomList(int count)
    //{
    //    int List_Lenght = result_List.Count;

    //    List<int> resultList = new List<int>();

    //    while (resultList.Count < count)
    //    {
    //        int randomNumber = UnityEngine.Random.Range(List_Lenght / 2, List_Lenght);
    //        if (!resultList.Contains(randomNumber))
    //        {
    //            resultList.Add(randomNumber);
    //        }
    //    }
    //    return resultList;
    //}




    //// ����Ʈ�� �ҷ����� �Լ� �Դϴ�. �ϴ� �ӽ÷� �ҷ����� ��ư�� ���������, ���߿� �Խ��� ��ȣ�ۿ�� ������� ������
    //public void PrintResultItemInfo()
    //{
    //    Debug.Log("Quest Loading");

    //    int count = 1;

    //    // count �� ����Ʈ ��ƾ Ƚ���� ���ϸ� ó�� ������ �� 1, ����Ʈ ������ ++i �Ǿ� 2 �̷������� 1�� �þ�� �� �̴ϴ�.
    //    // ������ ����Ʈ �ý����� ����� ���� �ʾ����� 1�� ������ �ӽô�. 

    //    int first = 0 + (count - 1) * 3;
    //    int second = first + 1;
    //    int third = second + 1;

    //    // i�� 1�� ��� 0, 1, 2. i�� 2�� ��� 3, 4, 5... 

    //    Debug.Log(string.Join(", ", first, second, third));

    //    Debug.Log(string.Join(" ", result_List.Count));

    //    Result_Item Firstitem = result_List[first];
    //    Result_Item Seconditem = result_List[second];
    //    Result_Item Thirditem = result_List[third];

    //    Debug.Log(string.Join(", ", result_List[first].result_Item_Name, result_List[second].result_Item_Name, result_List[third].result_Item_Name));

    //    // ResultItemDatabase�� �ִ� ��� ������ 3��(i�� 1�̴� 18��, 19��, 20�� ������)�� ������� ���� �ɴϴ�.

    //    Transform FirstParent = FirstQuestSlot.transform;
    //    Transform SecondParent = SecondQuestSlot.transform;
    //    Transform ThirdParent = ThirdQuestSlot.transform;

    //    //����Ʈ �� �Դϴ�. ���� Quest1, Quest1 (1), Quest1 (2) �� ���� �մϴ�.

    //    // ù��° ����Ʈ

    //    Transform ItemName1 = FirstParent.GetChild(0);
    //    TMP_Text childItem1 = ItemName1.GetComponent<TMP_Text>();
    //    // childItem1 ���� �����Ѵ�
    //    childItem1.text = Firstitem.result_Item_Name.ToString();

    //    Debug.Log("������ �̸� : " + Firstitem.result_Item_Name.ToString());

    //    Transform Price1 = FirstParent.GetChild(5);
    //    TMP_Text childPrice1 = Price1.GetComponent<TMP_Text>();
    //    // childPrice1 ���� �����Ѵ�
    //    childPrice1.text = Firstitem.result_Item_Price.ToString();

    //    Debug.Log("���� : " + Firstitem.result_Item_Price.ToString());

    //    Transform File_Name1 = FirstParent.GetChild(6);
    //    TMP_Text childFile_Name1 = File_Name1.GetComponent<TMP_Text>();
    //    // childFile_Name1 ���� �����Ѵ�
    //    childFile_Name1.text = Firstitem.icon_File_Name.ToString();

    //    Debug.Log("�����̸� : " + Firstitem.icon_File_Name.ToString());
    //    // ������ ���� �̸��� �˰ڴµ� ���� ��ġ�� ���� �ϴ� ���� �̸��� �߰� �س����ϴ�.


    //    // �ι�° ����Ʈ

    //    Transform ItemName2 = SecondParent.GetChild(0);
    //    TMP_Text childItem2 = ItemName2.GetComponent<TMP_Text>();
    //    // childItem2 ���� �����Ѵ�
    //    childItem2.text = Seconditem.result_Item_Name.ToString();

    //    Transform Price2 = SecondParent.GetChild(5);
    //    TMP_Text childPrice2 = Price2.GetComponent<TMP_Text>();
    //    // childPrice2 ���� �����Ѵ�
    //    childPrice2.text = Seconditem.result_Item_Price.ToString();

    //    Transform File_Name2 = SecondParent.GetChild(6);
    //    TMP_Text childFile_Name2 = File_Name2.GetComponent<TMP_Text>();
    //    // childFile_Name2 ���� �����Ѵ�
    //    childFile_Name2.text = Seconditem.icon_File_Name.ToString();
    //    // ������ ���� �̸��� �˰ڴµ� ���� ��ġ�� ���� �ϴ� ���� �̸��� �߰� �س����ϴ�.

    //    // ����° ����Ʈ

    //    Transform ItemName3 = ThirdParent.GetChild(0);
    //    TMP_Text childItem3 = ItemName3.GetComponent<TMP_Text>();
    //    // childItem3 ���� �����Ѵ�
    //    childItem3.text = Thirditem.result_Item_Name.ToString();

    //    Transform Price3 = ThirdParent.GetChild(5);
    //    TMP_Text childPrice3 = Price3.GetComponent<TMP_Text>();
    //    // childPrice3 ���� �����Ѵ�
    //    childPrice3.text = Thirditem.result_Item_Price.ToString();

    //    Transform File_Name3 = ThirdParent.GetChild(6);
    //    TMP_Text childFile_Name3 = File_Name3.GetComponent<TMP_Text>();
    //    // childFile_Name3 ���� �����Ѵ�
    //    childFile_Name3.text = Thirditem.icon_File_Name.ToString();
    //    // ������ ���� �̸��� �˰ڴµ� ���� ��ġ�� ���� �ϴ� ���� �̸��� �߰� �س����ϴ�.


    //}
}
