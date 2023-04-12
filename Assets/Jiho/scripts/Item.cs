using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public Sprite itemImage;
    public string itemName;
    public string itemStatus;
    public string itemRoute;
    public int itemCount;

    public Item()
    {

    }

    public Item(Sprite itemImage, string itemName, string itemStatus, string itemRoute, int itemCount)
    {
        this.itemImage = itemImage;
        this.itemName = itemName;
        this.itemStatus = itemStatus;
        this.itemRoute = itemRoute;
        this.itemCount = itemCount;
    }
}
