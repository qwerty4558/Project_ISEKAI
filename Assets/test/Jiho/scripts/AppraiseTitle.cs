using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AppraiseTitle : MonoBehaviour
{
    public static AppraiseTitle instance;
    [SerializeField] private Image itemImage;
    [SerializeField] private Ingredient_Item itemData;
    [SerializeField] private TextMeshProUGUI appraiseText;
    [SerializeField] private TextMeshProUGUI notAppraiseText;

    public bool isGet;

    private void Awake()
    {
        instance = this;
    }

    public void GetItem(Ingredient_Item item)
    {
        isGet = true;
        itemData = item;
        itemImage.sprite = itemData.itemImage;
        itemImage.enabled = true;
    }


    public void SendItem()
    {
        itemImage.enabled = false;
        itemImage.sprite = null;
        InventoryTitle.instance.AlchemyItemPlus(itemData);
        itemData = null;
        appraiseText.color = new Color(0, 0, 0, 255);
        appraiseText.DOFade(0, 1);
        InventoryTitle.instance.PrintInventory();
        isGet = false;
    }

    public void NotAppraise()
    {
        notAppraiseText.color = new Color(0, 0, 0, 255);
        notAppraiseText.DOFade(0, 1);
    }

    public void CancelItem()
    {
        itemImage.enabled = false;
        itemImage.sprite = null;
        InventoryTitle.instance.PlusItem(itemData);
        itemData = null;
        InventoryTitle.instance.PrintInventory();
        isGet = false;
    }
}
