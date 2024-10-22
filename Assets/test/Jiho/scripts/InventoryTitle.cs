using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTitle : MonoBehaviour
{
    public static InventoryTitle instance;

    [SerializeField] private SlotItem[] slotItems;
    [SerializeField] private GameObject itemStatus;
    [SerializeField] private TextMeshProUGUI[] statusTexts;
    [SerializeField] public Image puzzleImage;
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private GameObject appraiseObj;
    [SerializeField] private CameraFollow cameraFollow;
    public bool isAppraise;

    [ReadOnly]
    public Dictionary<string, Ingredient_Item> itemMap;
    [ReadOnly]
    public Dictionary<string, Ingredient_Item> alchemyItemMap;

    public GameObject Inventory { get => inventoryObj; }
    private void Awake()
    {       
        if(instance == null)
        {
            instance = this;
            itemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);
            alchemyItemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);
            
#if UNITY_EDITOR
            InitializeMaps();
#endif
            InitInventory();
        }
        else
        {
            Destroy(gameObject);
        }
        if(instance != null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
#if UNITY_EDITOR

    public void InitializeMaps(bool resetLists = true)
    {
        if(resetLists)
        {
            itemMap = new Dictionary<string, Ingredient_Item>();
            alchemyItemMap = new Dictionary<string, Ingredient_Item>();
        }

        string[] ingredient_item_guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(Ingredient_Item));

        foreach (string guid in ingredient_item_guids)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            Ingredient_Item item = UnityEditor.AssetDatabase.LoadAssetAtPath<Ingredient_Item>(path);

            if (item.count != 0)
                itemMap.Add(item.name, item);

            if (item.appraiseCount != 0)
                alchemyItemMap.Add(item.name, item);

        }

    }
#endif
    private void InitInventory()
    {
        itemStatus.SetActive(false);
        for (int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].itemStatus = itemStatus;
            slotItems[i].statusTexts = statusTexts;
        }
    }

    private void Update()
    {
        if (UIManager.Instance.isControl == true) 
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryObj.activeSelf)
                {
                    inventoryObj.SetActive(false);
                    itemStatus.SetActive(false);
                    if (cameraFollow != null)
                        cameraFollow.isInteraction = false;
                    CursorManage.instance.HideMouse();
                }
                else
                {
                    cameraFollow = FindObjectOfType<CameraFollow>();
                    PrintInventory();
                    inventoryObj.SetActive(true);
                    if (cameraFollow != null)
                        cameraFollow.isInteraction = true;
                    CursorManage.instance.ShowMouse();
                }
            }
        }
        

        /*if(Input.GetKeyDown(KeyCode.U)) //발표용 임시
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
        }*/
    }

    public void PrintInventory()
    {
        int count = 0;
        for(int i = 0; i < slotItems.Length; i++)
            slotItems[i].itemData = null;

        foreach(KeyValuePair<string, Ingredient_Item> pair in itemMap)
        {
            Ingredient_Item temp = pair.Value;
            if(temp.itemType != ItemType.Equipment)
            {
                slotItems[count].itemData = temp;
                slotItems[count].updateData();
                count++;
            }            
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

        if(QuestTitle.instance != null)
            QuestTitle.instance.QuestItemCheck();
    }
    public void PlusItem2(Ingredient_Item item)
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

    public Ingredient_Item[] InventoryMapReturnOnlyIngredient() // 장비 제외한 다른 타입만 출력
    {
        List<Ingredient_Item> _itemMap = new List<Ingredient_Item> ();
        foreach (KeyValuePair<string, Ingredient_Item> pair in itemMap) 
        {
            if(pair.Value.itemType != ItemType.Quest && pair.Value.itemType != ItemType.Equipment && pair.Value.itemType == ItemType.Ingredient)
            {
                _itemMap.Add(pair.Value);
            }
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
