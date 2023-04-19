using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTitle : MonoBehaviour
{
    [SerializeField] private SlotItem[] slotItems;
    [SerializeField] private GameObject itemStatus;
    [SerializeField] private TextMeshProUGUI[] statusTexts;
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private CameraFollow cameraFollow;
    
    public Dictionary<string, Ingredient_Item> itemMap;

    private void Awake()
    {
        itemMap = new Dictionary<string, Ingredient_Item>(slotItems.Length);
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
                cameraFollow.isInteraction = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                PrintInventory();
                inventoryObj.SetActive(true);
                
                cameraFollow.isInteraction = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void PrintInventory()
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
            itemMap[item.name].count += item.count;
        }
        else
        {
            itemMap.Add(item.name, item);
        }
    }
}
