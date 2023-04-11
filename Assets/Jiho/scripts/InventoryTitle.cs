using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryTitle : MonoBehaviour
{
    [SerializeField] private SlotItem[] slotItems;
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private CameraFollow cameraFollow;

    private List<SlotItem> itemList;

    private void Awake()
    {
        itemList = new List<SlotItem>(slotItems.Length);
        ResetInven();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryObj.activeSelf)
            {
                inventoryObj.SetActive(false);
                cameraFollow.isInteraction = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                inventoryObj.SetActive(true);
                cameraFollow.isInteraction = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void ResetInven()
    {
        for(int i = 0; i < slotItems.Length; i++)
            itemList.Add(slotItems[i]);
    }

    private void LinkItems()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            slotItems[i].itemImage = itemList[i].itemImage;
            slotItems[i].ItemName = itemList[i].ItemName;
            slotItems[i].Count = itemList[i].Count;
            slotItems[i].ID = itemList[i].ID;
            slotItems[i].IsCheck = false;
        }
    }

    public void PlusItem(SlotItem item)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ID == item.ID)
            {
                itemList[i].Count++;
                LinkItems();
                return;
            }
        }

        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ID == 0)
            {
                itemList[i] = item;
                LinkItems();
                return;
            }
        }
    }
}
