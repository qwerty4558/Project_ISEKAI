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
    private bool PuzzleEnabled = false;

    private void OnEnable()
    {
        PuzzleEnabled = true;
    }

    private void Start()
    {
        OnPuzzleComplete.AddListener(PuzzleComplete);
        SetResultItem(test_item);
        
    }

    public void SetResultItem(Result_Item item)
    {
        if (PuzzleEnabled == false) return;

        currentItem = item;

        itemPot.SetItemPot(item);
        usageSlot.SetUsageSlot(item);
    }

    public bool TryPuzzlePiece(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return false;

        return itemPot.TryPuzzlePiece(item);
    }

    public void WritePuzzlePiece(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return;

        if (usageSlot.SlotsFull()) return;

        itemPot.WritePuzzlePiece(item);
        usageSlot.InsertIngredient(item);
    }

    public void ResetPot()
    {
        if (PuzzleEnabled == false) return;

        itemPot.DisablePuzzlePieceVisualizer();
        SetResultItem(currentItem);
    }

    public void VisualizeTile(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return;

        itemPot.PuzzlePieceVisualize(item);
    }

    public void PuzzleComplete()
    {
        PuzzleEnabled = false;
    }

    public void Debug_ForceComplete()
    {
        if (PuzzleEnabled == false) return;
        OnPuzzleComplete.Invoke();
    }
}
