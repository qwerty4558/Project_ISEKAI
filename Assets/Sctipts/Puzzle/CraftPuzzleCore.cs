using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CraftPuzzleCore : SingletonMonoBehaviour<CraftPuzzleCore>
{
    [SerializeField] private ItemPot itemPot;
    [SerializeField] private UsageSlot usageSlot;
    [SerializeField] private PuzzleIngredientItems itemView;

    [Title("Debug")]
    [SerializeField] private Result_Item test_item;

    public UnityEvent OnPuzzleComplete;

    private Result_Item currentItem;
    private bool PuzzleEnabled = false;

    private void OnEnable()
    {
        PuzzleEnabled = true;
        SetResultItem(test_item);
    }

    private void Start()
    {
        OnPuzzleComplete.AddListener(PuzzleComplete);
    }

    public void LoadItemFromInventory()
    {
        if (InventoryTitle.instance != null)
        {
            List<Ingredient_Item> filtered = new List<Ingredient_Item>();

            foreach(Ingredient_Item item in InventoryTitle.instance.alchemyItemMap.Values.ToArray())
            {
                if(item.itemType == ItemType.Ingredient)
                {
                    filtered.Add(item);
                }
            }

            itemView.SetItemWindow(filtered.ToArray());
        }
    }

    public void ProcessToInventory()
    {
        if (InventoryTitle.instance == null) return;

        var itemsUse = itemPot.WritedItems;

        if (currentItem.outputItem == null)
        {
            Debug.LogError(currentItem.result_Item_Name + " : 해당 아이템과 연결된 Outputitem이 없습니다.");
            return;
        }
        else
        {
            InventoryTitle.instance.AlchemyItemPlus(currentItem.outputItem);
        }

        foreach (var item in itemsUse)
        {
            InventoryTitle.instance.AlchemyItemMinus(item);
        }
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