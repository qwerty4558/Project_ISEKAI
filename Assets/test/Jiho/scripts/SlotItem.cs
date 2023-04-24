using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int id;

    [SerializeField] private int count;
    [SerializeField] private string itemName;
    [SerializeField] private string status;
    [SerializeField] private string route;
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI countText;

    private Vector3 orgPos;
    private bool isAlchemy;

    public TextMeshProUGUI[] statusTexts;
    public GameObject itemStatus;
    public Sprite itemImage;

    public Ingredient_Item itemData;

    public string ItemName { get => itemName; set => itemName = value;  }
    public string Status { get => status; set => status = value; }
    public string Route { get => route; set => route = value; }
    public int ID { get => id; set => id = value; }
    public int Count { get => count; set => count = value;  }

    private void Awake()
    {
        orgPos = transform.position;
        itemData = new Ingredient_Item(itemImage, itemName, status, route, count,count);
        updateData();
    }

    public void updateData()
    {
        itemImage = itemData.itemImage;
        count = itemData.count;
        status = itemData.status;
        route = itemData.route;
        itemName = itemData.name;
    }
    
    private void Update()
    {
        if(itemData != null)
        {
            if (itemData.count != 0)
            {
                slotImage.enabled = true;
                slotImage.sprite = itemImage;
                countText.enabled = true;
                countText.text = count.ToString();
            }
            else
            {
                itemData = null;
                slotImage.enabled = false;
                countText.enabled = false;
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        statusTexts[0].text = itemName;
        statusTexts[1].text = status;
        statusTexts[2].text = route;

        itemStatus.transform.position = this.transform.position;
        itemStatus.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemStatus.SetActive(false);
    }

    public void OutItem()
    {
        if (InventoryTitle.instance.isAppraise)
        {
            if (this.itemData != null)
            {
                InventoryTitle.instance.MinusItem(this.itemData);
                AppraiseTitle.instance.GetItem(this.itemData);
                itemName = null;
                status = null;
                route = null;
                InventoryTitle.instance.PrintInventory();
            }
        }
    }
}
