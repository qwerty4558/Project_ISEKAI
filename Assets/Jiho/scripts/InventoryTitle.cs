using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    private Dictionary<string, Item> itemMap;


    private void Awake()
    {
        itemMap = new Dictionary<string, Item>(slotItems.Length);
        InitInventory();
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
        foreach(KeyValuePair<string, Item> pair in itemMap)
        {
            Item temp = pair.Value;
            slotItems[count].itemData = temp;
            slotItems[count].updateData();
            count++;
        }
    }

    public void PlusItem(Item item)
    {
        if (itemMap.ContainsKey(item.itemName))
        {
            itemMap[item.itemName].itemCount += item.itemCount;
        }
        else
        {
            itemMap.Add(item.itemName, item);
        }
    }

    
}
