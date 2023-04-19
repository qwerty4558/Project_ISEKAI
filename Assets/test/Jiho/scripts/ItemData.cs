using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    //[SerializeField] private Ingredient_Item puzzleItem;
    //[SerializeField] private alchemyItem[] alchemyItems;
    //[SerializeField] private Image myImage;
    //[SerializeField] private Image roadImage;
    //[SerializeField] private bool isInit;

    //private InventoryTitle inven;
    //private int index;

    //private void Awake()
    //{
    //    InitItem();
    //}

    //private void Update()
    //{
    //    if(!isInit)
    //    {
    //        isInit = true;
    //        inven = FindObjectOfType<InventoryTitle>();
    //    }
    //}

    //public void ItemInput()
    //{
    //    int count = 0;
    //    foreach (KeyValuePair<string, Item> pair in inven.itemMap)
    //    {
    //        Item temp = pair.Value;
    //        alchemyItems[count].itemData = temp;
    //        alchemyItems[count].updateData();
    //        count++;
    //    }
    //}

    //private void InitItem()
    //{
    //    if (puzzleItem != null)
    //    {
    //        index = puzzleItem.id;
    //        myImage.enabled = true;

    //        if (index == 0)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
    //        if (index == 1)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
    //        if (index == 2)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
    //        if (index == 3)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
    //        if (index == 4)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //        if (index == 5)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
    //        if (index == 6)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -135));
    //        if (index == 7)
    //            roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
    //        roadImage.enabled = true;
    //    }
    //}

    //public void OnDragBegin(BaseEventData data)
    //{
        
    //}
}
