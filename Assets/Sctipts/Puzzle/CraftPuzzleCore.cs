using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class CraftPuzzleCore : MonoBehaviour
{
    private static CraftPuzzleCore instance;
    public static CraftPuzzleCore Instance { get { return instance; } }

    [SerializeField] private ItemPot itemPot;
    [SerializeField] private GameObject potFrame;
    [SerializeField] private UsageSlot usageSlot;
    [SerializeField] private PuzzleIngredientItems itemView;

    [Title("Debug")]
    [SerializeField] private Result_Item test_item;
     public Ingredient_Item[] testIngredientInventory;
    public bool DebugMode = false;

    public UnityEvent OnPuzzleComplete;

    private Result_Item currentItem;
    private bool PuzzleEnabled = false;


    private void Awake()
    {
        if (instance == null) 
            instance = this;

        if (OnPuzzleComplete == null)
            OnPuzzleComplete = new UnityEvent();
    }

    private void OnEnable()
    {
        PuzzleEnabled = true;

        PlayerController player = PlayerController.instance;
        if (player != null)
            player.ControlEnabled = false;

        if (DebugMode)
            SetResultItem(test_item);
        else
            LoadItemFromInventory();
    }

    private void OnDisable()
    {
        PlayerController player = PlayerController.instance;
        if (player != null)
            player.ControlEnabled = true;
    }

    private void Start()
    {
        OnPuzzleComplete.AddListener(PuzzleComplete);
        OnPuzzleComplete.AddListener(ProcessToInventory);
    }

    public void LoadItemFromInventory()
    {
        if (InventoryTitle.instance != null)
        {
            List<Ingredient_Item> filtered = new List<Ingredient_Item>();

            foreach (Ingredient_Item item in InventoryTitle.instance.alchemyItemMap.Values.ToArray())
            {
                if (item.itemType == ItemType.Ingredient && item.appraiseCount != 0)
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
            Debug.LogError(currentItem.result_Item_Name + " :  Outputitem이 없습니다! 인벤토리에 아이템이 추가되려면 해당 값이 필요합니다!");
            return;
        }
        else
        {
            InventoryTitle.instance.AlchemyItemPlus(currentItem.outputItem);

            foreach (var item in itemsUse)
            {
                InventoryTitle.instance.AlchemyItemMinus(item);
            }
        }
    }

    public void SetResultItem(Result_Item item)
    {
        if (PuzzleEnabled == false) return;

        currentItem = item;

        itemPot.SetItemPot(item);
        usageSlot.SetUsageSlot(item);
        if (DebugMode)
            itemView.SetItemWindow(testIngredientInventory);
        else
        {
            LoadItemFromInventory();
        }
    }

    public bool TryPuzzlePiece(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return false;

        bool tryOnItemview = DebugMode || itemView.TryOneItem(item);

        return itemPot.TryPuzzlePiece(item) && tryOnItemview;
    }

    public void WritePuzzlePiece(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return;

        if (usageSlot.SlotsFull()) return;

        itemPot.WritePuzzlePiece(item);
        usageSlot.InsertIngredient(item);
        itemView.UseOneItem(item);
    }

    public void ResetPot()
    {
        if (PuzzleEnabled == false) return;

        itemPot.DisablePuzzlePieceVisualizer();
        SetResultItem(currentItem);
    }

    public void UndoPiece()
    {
        if (PuzzleEnabled == false) return;

        itemPot.DisablePuzzlePieceVisualizer();
        itemPot.UndoSetItemPot(currentItem);
        itemPot.UndoPiece();
        usageSlot.UndoSlot();
        itemView.UndoUsedItem();
    }

    public void VisualizeTile(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return;

        itemPot.PuzzlePieceVisualize(item);
    }

    public void PuzzleComplete()
    {
        PuzzleEnabled = false;
        potFrame.GetComponent<DOTweenAnimation>().DORestartById("ClosePot");
    }

    public void Debug_ForceComplete()
    {
        if (PuzzleEnabled == false) return;
        OnPuzzleComplete.Invoke();
    }
}