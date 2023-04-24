using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTitle : MonoBehaviour
{
    public static InventoryTitle instance;

    [SerializeField] private SlotItem[] slotItems;
    [SerializeField] private GameObject itemStatus;
    [SerializeField] private TextMeshProUGUI[] statusTexts;
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private GameObject appraiseObj;
    [SerializeField] private CameraFollow cameraFollow;
    public bool isAppraise;
    public Dictionary<string, Ingredient_Item> itemMap;
    public Dictionary<string, Ingredient_Item> alchemyItemMap;

    private void Awake()
    {
        
        instance = this;
        itemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);
        alchemyItemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);

        InitInventory();
        DontDestroyOnLoad(this.gameObject);
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
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                cameraFollow = FindObjectOfType<CameraFollow>();
                PrintInventory();
                inventoryObj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.T)) //발표용 임시
        {
            if (inventoryObj.activeSelf)
            {
                isAppraise = false;
                appraiseObj.SetActive(false);
                inventoryObj.SetActive(false);
                itemStatus.SetActive(false);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                isAppraise = true;
                PrintInventory();
                appraiseObj.SetActive(true);
                inventoryObj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void PrintInventory()
    {
        int count = 0;
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
}
