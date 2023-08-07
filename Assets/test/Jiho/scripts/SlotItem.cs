using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SHOW_NUM
{
    ITEMCOUNT = 1,
    APPRAISECOUNT
}

public class SlotItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int id;

    [SerializeField] private int count;
    [SerializeField] private int appraiseCount;
    [SerializeField] private string itemName;
    [SerializeField] private string status;
    [SerializeField] private string route;
    [SerializeField] private Image slotImage;
    [SerializeField] private Image puzzleImage;
    [SerializeField] private TextMeshProUGUI countText;

    public SHOW_NUM show = SHOW_NUM.ITEMCOUNT;

    private Vector3 orgPos;
    private bool isAlchemy;

    public TextMeshProUGUI[] statusTexts;
    public GameObject itemStatus;
    public Sprite itemImage;
    public Sprite appraiseImage;

    public Ingredient_Item itemData;
    public Ingredient_Item appraiseItemData;

    public string ItemName { get => itemName; set => itemName = value; }
    public string Status { get => status; set => status = value; }
    public string Route { get => route; set => route = value; }
    public int ID { get => id; set => id = value; }
    public int Count { get => count; set => count = value; }
    public int AppraiseCount { get => appraiseCount; set => appraiseCount = value; }

    private void Awake()
    {
        orgPos = transform.position;
        updateData();
    }

    public void updateData()
    {
        if(show == SHOW_NUM.ITEMCOUNT)
        {
            if (itemData != null)
            {
                itemImage = itemData.itemImage;
                appraiseImage = itemData.itemPatternImage;
                count = itemData.count;
                status = itemData.status;
                route = itemData.route;
                itemName = itemData.name_KR;
                appraiseCount = itemData.appraiseCount;
            }
        }
        
        else if(show == SHOW_NUM.APPRAISECOUNT)
        {
            if (appraiseItemData != null)
            {
                itemImage = appraiseItemData.itemImage;
                appraiseImage = appraiseItemData.itemPatternImage;
                count = appraiseItemData.count;
                status = appraiseItemData.status;
                route = appraiseItemData.route;
                itemName = appraiseItemData.name_KR;
                appraiseCount = appraiseItemData.appraiseCount;
            }
        }
    }
    
    private void Update()
    {
        if(show == SHOW_NUM.ITEMCOUNT)
        {
            if (itemData != null)
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
            else
            {
                slotImage.enabled = false;
                countText.enabled = false;
            }
        }
        else if (show == SHOW_NUM.APPRAISECOUNT)
        {
            if (appraiseItemData != null)
            {
                if (appraiseItemData.appraiseCount != 0)
                {
                    slotImage.enabled = true;
                    slotImage.sprite = itemImage;
                    countText.enabled = true;
                    countText.text = appraiseCount.ToString();
                }
                else
                {
                    itemData = null;
                    slotImage.enabled = false;
                    countText.enabled = false;
                }
            }
            else
            {
                slotImage.enabled = false;
                countText.enabled = false;
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if(show != SHOW_NUM.APPRAISECOUNT)
        {
            statusTexts[0].text = itemName;
            statusTexts[1].text = status;
            statusTexts[2].text = route;
            InventoryTitle.instance.puzzleImage.sprite = appraiseImage;
            itemStatus.transform.position = this.transform.position;
            if (count > 0)
                itemStatus.SetActive(true);
            if (appraiseCount > 0)
                InventoryTitle.instance.puzzleImage.enabled = true;
            else InventoryTitle.instance.puzzleImage.enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemStatus.SetActive(false);
    }

    public void OutItem()
    {
        if (InventoryTitle.instance.isAppraise)
        {
            if (itemData != null)
            {
                if(itemData.isAppraise)
                {
                    if(!AppraiseTitle.instance.isGet)
                    {
                        InventoryTitle.instance.MinusItem(itemData);
                        AppraiseTitle.instance.GetItem(itemData);
                        itemName = null;
                        status = null;
                        route = null;
                        InventoryTitle.instance.PrintInventory();
                    }
                }
                else if(!AppraiseTitle.instance.isGet)
                {
                    AppraiseTitle.instance.NotAppraise();
                }
            }
        }
    }
}
