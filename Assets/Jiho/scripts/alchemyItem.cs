using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class alchemyItem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int count;
    [SerializeField] private string itemName;
    [SerializeField] private string status;
    [SerializeField] private string route;
    [SerializeField] private Image alchemyItemImage;
    [SerializeField] private Image roadImage;
    [SerializeField] private TextMeshProUGUI countText;

    public Item itemData;
    public Sprite itemImage;

    public string ItemName { get => itemName; set => itemName = value; }
    public string Status { get => status; set => status = value; }
    public string Route { get => route; set => route = value; }
    public int ID { get => id; set => id = value; }
    public int Count { get => count; set => count = value; }

    private void Awake()
    {
        itemData = new Item(itemImage, itemName, status, route, count);
        updateData();
    }

    public void updateData()
    {
        itemImage = itemData.itemImage;
        count = itemData.itemCount;
        status = itemData.itemStatus;
        route = itemData.itemRoute;
        itemName = itemData.itemName;   
    }

    private void Update()
    {
        if (count != 0)
        {
            alchemyItemImage.enabled = true;
            alchemyItemImage.sprite = itemImage;
            countText.enabled = true;
            countText.text = count.ToString();
        }
        else
        {
            alchemyItemImage.enabled = false;
            countText.enabled = false;
        }
    }
}
