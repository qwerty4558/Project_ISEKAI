using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTitle : SerializedMonoBehaviour
{
    public static InventoryTitle instance;

    [SerializeField] private SlotItem[] slotItems;
    [SerializeField] private GameObject itemStatus;
    [SerializeField] private TextMeshProUGUI[] statusTexts;
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private GameObject appraiseObj;
    [SerializeField] private CameraFollow cameraFollow;
    public bool isAppraise;

    [ReadOnly]
    public Dictionary<string, Ingredient_Item> itemMap;
    [ReadOnly]
    public Dictionary<string, Ingredient_Item> alchemyItemMap;

    private void Awake()
    {       
        instance = this;
        itemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);
        alchemyItemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);

        InitializeMaps();
        InitInventory();
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitializeMaps(bool resetLists = true)
    {
        if(resetLists)
        {
            itemMap = new Dictionary<string, Ingredient_Item>();
            alchemyItemMap = new Dictionary<string, Ingredient_Item>();
        }

        string[] ingredient_item_guids = AssetDatabase.FindAssets("t:" + typeof(Ingredient_Item));

        foreach (string guid in ingredient_item_guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Ingredient_Item item = AssetDatabase.LoadAssetAtPath<Ingredient_Item>(path);

            if (item.count != 0)
                itemMap.Add(item.name, item);

            if (item.appraiseCount != 0)
                alchemyItemMap.Add(item.name, item);
        }
    }

    private void InitInventory()
    {
        for(int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].itemStatus = itemStatus;
            slotItems[i].statusTexts = statusTexts;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryObj.activeSelf)
            {
                inventoryObj.SetActive(false);
                itemStatus.SetActive(false);
                if(cameraFollow != null)
                    cameraFollow.isInteraction = false;
            }
            else
            {
                cameraFollow = FindObjectOfType<CameraFollow>();
                PrintInventory();
                inventoryObj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.U)) //발표용 임시
        {
            if (inventoryObj.activeSelf)
            {
                isAppraise = false;
                appraiseObj.SetActive(false);
                inventoryObj.SetActive(false);
                itemStatus.SetActive(false);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = false;
            }
            else
            {
                isAppraise = true;
                PrintInventory();
                appraiseObj.SetActive(true);
                inventoryObj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
            }
        }
    }

    public void PrintInventory()
    {
        int count = 0;
        for(int i = 0; i < slotItems.Length; i++)
            slotItems[i].itemData = null;

        foreach(KeyValuePair<string, Ingredient_Item> pair in itemMap)
        {
            Ingredient_Item temp = pair.Value;
            slotItems[count].itemData = temp;
            slotItems[count].updateData();
            count++;
        }
    }

    public void PlusItem(Ingredient_Item item)
    {
        if (itemMap.ContainsKey(item.name))
        {
            itemMap[item.name].count += 1;
        }
        else
        {
            itemMap.Add(item.name, item);
            itemMap[item.name].count += 1;
        }

        QuestTitle.instance.QuestItemCheck();
    }

    public void MinusItem(Ingredient_Item item)
    {
        if(itemMap.ContainsKey(item.name))
        {
            itemMap[item.name].count -= 1;
            if (itemMap[item.name].count == 0)
                itemMap.Remove(item.name);
        }
    }

    public void AlchemyItemMinus(Ingredient_Item item)
    {
        if (alchemyItemMap.ContainsKey(item.name))
        {
            alchemyItemMap[item.name].appraiseCount -= 1;
            if (alchemyItemMap[item.name].appraiseCount == 0)
                alchemyItemMap.Remove(item.name);
        }
    }

    public void AlchemyItemPlus(Ingredient_Item item)
    {
        if (alchemyItemMap.ContainsKey(item.name))
        {
            alchemyItemMap[item.name].appraiseCount += 1;
        }
        else
        {
            alchemyItemMap.Add(item.name, item);
            alchemyItemMap[item.name].appraiseCount += 1;
        }
    }

    public Ingredient_Item[] InvenItemMapReturn()
    {
        List<Ingredient_Item> _itemMap = new List<Ingredient_Item>();

        foreach (KeyValuePair<string, Ingredient_Item> pair in itemMap)
        {
            _itemMap.Add(pair.Value);
        }

        return _itemMap.ToArray();
    }

    public Ingredient_Item[] AlchemyItemMapReturn()
    {
        List<Ingredient_Item> _itemMap = new List<Ingredient_Item>();
        foreach (KeyValuePair<string, Ingredient_Item> pair in alchemyItemMap)
        {
            _itemMap.Add(pair.Value);
        }

        return _itemMap.ToArray();
    }
}
