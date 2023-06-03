using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Discrimination_ItemSlot : ItemviewSlot
{
    public override void SetItemData(Ingredient_Item item)
    {
        itemData = item;
        image.sprite = Resources.Load<Sprite>(item.icon_File_Name);
        patternImage.gameObject.SetActive(false);
        quantatyText.text = item.count.ToString();
    }

    public override void ResetText()
    {
        quantatyText.text = itemData.count.ToString();
    }

    public void DoDiscrimination()
    {
            Discrimination.Instance.DoDiscrimination();
    }

    public override void OnItemButtonEnter()
    {
        Discrimination.Instance.TryDiscrimination(ItemData);
    }
}
