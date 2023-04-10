using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public Image itemImage;
    public int id;
    public string itemName;
    public int count;
}

public class InventoryTitle : MonoBehaviour
{
    [SerializeField] private SlotItem[] slotItems;

    private List<Item> itemList;

    private void Awake()
    {
        itemList = new List<Item>(slotItems.Length);

    }

    private void ResetInven()
    {
        for(int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].ItemImage = null;
            slotItems[i].ID = 0;
            slotItems[i].Count = 0;
            slotItems[i].ItemName = "";
        }
    }

    private void LinkItems()
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            slotItems[i].ItemImage = itemList[i].itemImage;
            slotItems[i].ItemName = itemList[i].itemName;
            slotItems[i].Count = itemList[i].count;
            slotItems[i].ID = itemList[i].id;
        }
    }

    private void PlusItem(Item item)
    {
        itemList.Add(item);

    }
}
