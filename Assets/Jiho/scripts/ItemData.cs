using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    [SerializeField] private Ingredient_Item itemData;
    [SerializeField] private Result_Item mainItemData;
    [SerializeField] private Sprite[] itemImages;
    [SerializeField] private Sprite[] mainItemImages;
    [SerializeField] private Image myImage;
    [SerializeField] private Image roadImage;
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

            if (index == 0)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
            if (index == 1)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
            if (index == 2)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
            if (index == 3)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            if (index == 4)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (index == 5)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            if (index == 6)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -135));
            if (index == 7)
                roadImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            roadImage.enabled = true;
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
            roadImage.enabled = false;
        }
    }

    public void OnDragBegin(BaseEventData data)
    {
        
    }
}
