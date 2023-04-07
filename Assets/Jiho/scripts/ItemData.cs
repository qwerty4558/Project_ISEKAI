using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    [SerializeField] private Ingredient_Item itemData;
    [SerializeField] private Result_Item mainItemData;
    [SerializeField] private Sprite[] itemImages;
    [SerializeField] private Sprite[] mainItemImages;
    [SerializeField] private Image myImage;
    private int index;

    private void Awake()
    {
        InitItem();
    }

    private void InitItem()
    {
        if (itemData != null)
        {
            index = itemData.id;
            myImage.sprite = itemImages[index];
            myImage.enabled = true;
        }
        else if(mainItemData != null)
        {
            index = mainItemData.index;
            myImage.sprite = mainItemImages[index];
            myImage.enabled = true;
        }
        else
        {
            myImage.enabled = false;
        }
    }
}
