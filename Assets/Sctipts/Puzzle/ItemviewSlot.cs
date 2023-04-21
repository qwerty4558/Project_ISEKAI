using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemviewSlot : MonoBehaviour
{
    [SerializeField] private Ingredient_Item ItemData;
    [SerializeField] private Image image;


    public void SetItemData(Ingredient_Item item)
    {
        ItemData = item;
        image.sprite = Resources.Load<Sprite>(item.icon_File_Name);
    }

    public void WriteOnPot()
    {
        if (!CraftPuzzleCore.Instance.itemPot.TryPuzzlePiece(ItemData)) return;

        CraftPuzzleCore.Instance.itemPot.WritePuzzlePiece(ItemData);
    }
}
