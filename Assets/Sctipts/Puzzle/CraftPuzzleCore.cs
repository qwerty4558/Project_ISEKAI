using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftPuzzleCore : MonoBehaviour
{
    private static CraftPuzzleCore instance;
    public static CraftPuzzleCore Instance { get { return instance; } }

    [SerializeField] private ItemPot itemPot;
    [SerializeField] private GameObject potFrame;
    [SerializeField] private UsageSlot usageSlot;
    [SerializeField] private PuzzleIngredientItems itemView;
    [SerializeField] private Image img;

     public Ingredient_Item[] testIngredientInventory;

    public UnityEvent OnPuzzleComplete;

    private Result_Item currentItem;
    private bool PuzzleEnabled = false;

    public static SoundModule sound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 중복 생성된 인스턴스를 파괴
            return;
        }

        if (OnPuzzleComplete == null)
        {
            OnPuzzleComplete = new UnityEvent();
        }

        // 이하 생략
    }


    private void OnEnable()
    {
        PuzzleEnabled = true;
        sound = GetComponent<SoundModule>();

        PlayerController player = PlayerController.instance;
        if (player != null)
            player.ControlEnabled = false;
        sound.Play("Close");
        LoadItemFromInventory();
    }

    private void OnDisable()
    {
        PlayerController player = PlayerController.instance;
        if (player != null)
            player.ControlEnabled = true;
        CursorManage.instance.HideMouse();
    }

    private void Start()
    {
        OnPuzzleComplete.AddListener(PuzzleComplete);
        OnPuzzleComplete.AddListener(ProcessToInventory);
    }

    private void Update()
    {
        if (gameObject.activeSelf) CursorManage.instance.ShowMouse();
    } 

    public void LoadItemFromInventory()
    {
        if (InventoryTitle.instance != null)
        {
            itemView.SetItemWindow(InventoryTitle.instance.AlchemyItemMapReturn());
        }
    }

    public void ProcessToInventory()
    {
        if (InventoryTitle.instance == null || currentItem == null) return;

        var itemsUse = itemPot.WritedItems;

        if (currentItem.outputItem == null)
        {
            Debug.LogError(currentItem.result_Item_Name + " :  Outputitem이 없습니다! 인벤토리에 아이템이 추가되려면 해당 값이 필요합니다!");
            return;
        }
        else
        {
            InventoryTitle.instance.PlusItem(currentItem.outputItem);

            foreach (var item in itemsUse)
            {
                InventoryTitle.instance.AlchemyItemMinus(item);
            }

            if (!currentItem.ReCraftable) MultisceneDatapass.Instance.craftableItems.Remove(currentItem);
        }
    }

    public void SetResultItem(Result_Item item)
    {
        if (PuzzleEnabled == false) return;

        currentItem = item;

        itemPot.SetItemPot(item);
        usageSlot.SetUsageSlot(item);
        LoadItemFromInventory();
    }

    public void SetResultItemNone()
    {
        if (PuzzleEnabled == false) return;

        currentItem = null;

        itemPot.SetItempotNone();
        usageSlot.SetUsageSlotNone();
    }

    public bool TryPuzzlePiece(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return false;

        bool tryOnItemview = itemView.TryOneItem(item);

        return itemPot.TryPuzzlePiece(item) && tryOnItemview;
    }

    public void WritePuzzlePiece(Ingredient_Item item)
    {
        if (PuzzleEnabled == false) return;

        if (usageSlot.SlotsFull()) return;

        sound.Play_No_Isplay("InputPieces");

        itemPot.WritePuzzlePiece(item);
        usageSlot.InsertIngredient(item);
        itemView.UseOneItem(item);
    }

    public void ResetPot()
    {
        if (PuzzleEnabled == false) return;

        sound.Play_No_Isplay("UndoOrReset");

        itemPot.DisablePuzzlePieceVisualizer();
        SetResultItem(currentItem);
    }

    public void UndoPiece()
    {
        if (PuzzleEnabled == false) return;

        sound.Play_No_Isplay("UndoOrReset");

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
        img.sprite = currentItem.outputItem.itemImage;
        PuzzleEnabled = false;
        potFrame.GetComponent<DOTweenAnimation>().DORestartById("ClosePot");
    }

    public void Debug_ForceComplete()
    {
        if (PuzzleEnabled == false) return;
        OnPuzzleComplete.Invoke();
    }
}