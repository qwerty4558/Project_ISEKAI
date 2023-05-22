using System.Collections.Generic;
using UnityEngine;

public class PuzzleIngredientItems : MonoBehaviour
{
    [SerializeField] private RectTransform Viewport;
    [SerializeField] private GameObject ItemSlotPrefab;

    [SerializeField] private int ItemsRow;
    [SerializeField] private Vector2 SlotSize;
    private GameObject ItemSlots;

    private List<ItemviewSlot> ItemButtonObjects;
    private Stack<ItemviewSlot> UndoStack;

    private void Awake()
    {
        UndoStack = new Stack<ItemviewSlot>();
    }

    public void SetItemWindow(Ingredient_Item[] _items)
    {
        if (ItemButtonObjects != null)
        {
            foreach (var i in ItemButtonObjects)
                Destroy(i.gameObject);

            ItemButtonObjects.Clear();
        }
        else
        {
            ItemButtonObjects = new List<ItemviewSlot>();
        }

        if (UndoStack != null)
            UndoStack.Clear();

        Viewport.sizeDelta = new Vector2(Viewport.sizeDelta.x, SlotSize.y * (_items.Length + 1) / 3);

        for (int i = 0; i < _items.Length; i++)
        {
            GameObject newSlot = Instantiate(ItemSlotPrefab, Viewport, false);
            newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2((i % 3) * SlotSize.x, i / 3 * -SlotSize.y);
            newSlot.GetComponent<ItemviewSlot>().SetItemData(_items[i]);
            ItemButtonObjects.Add(newSlot.GetComponent<ItemviewSlot>());
        }
    }

    public bool UseOneItem(Ingredient_Item _item)
    {
        if (ItemButtonObjects == null) return false;

        ItemviewSlot item = ItemButtonObjects.Find(slot=> slot.ItemData == _item);

        if (_item.appraiseCount - item.itemUsed <= 0) return false;

        if(item != null)
        {
            item.itemUsed++;
            item.ResetText();
            UndoStack.Push(item);
        }
        return true;
    }

    public bool TryOneItem(Ingredient_Item _item)
    {
        if (ItemButtonObjects == null) return false;

        ItemviewSlot item = ItemButtonObjects.Find(slot => slot.ItemData == _item);

        if (_item.appraiseCount - item.itemUsed <= 0) return false;
        else return true;
    }

    public void UndoUsedItem()
    {
        if (UndoStack.Count == 0) return;
        ItemviewSlot slot = UndoStack.Pop();
        slot.itemUsed--;
        slot.ResetText();
    }
}
