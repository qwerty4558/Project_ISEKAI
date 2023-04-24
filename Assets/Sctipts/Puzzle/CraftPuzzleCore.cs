using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftPuzzleCore : SingletonMonoBehaviour<CraftPuzzleCore>
{
    [SerializeField] private ItemPot itemPot;
    [SerializeField] private UsageSlot usageSlot;

    [Title("Debug")]
    [SerializeField] private Result_Item test_item;

    public UnityEvent OnPuzzleComplete;

    private Result_Item currentItem;

    private void Start()
    {
        SetResultItem(test_item);
    }

    public void SetResultItem(Result_Item item)
    {
        currentItem = item;

        itemPot.SetItemPot(item);
        usageSlot.SetUsageSlot(item);
    }

    public bool TryPuzzlePiece(Ingredient_Item item)
    {
        return itemPot.TryPuzzlePiece(item);
    }

    public void WritePuzzlePiece(Ingredient_Item item)
    {
        if (usageSlot.SlotsFull()) return;

        itemPot.WritePuzzlePiece(item);
        usageSlot.InsertIngredient(item);
    }

    public void ResetPot()
    {
        SetResultItem(currentItem);
    }

}
