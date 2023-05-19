using System.Collections.Generic;
using UnityEngine;

public class PuzzleIngredientItems : MonoBehaviour
{
    [SerializeField] private Ingredient_Item[] items;
    [SerializeField] private Transform Viewport;
    [SerializeField] private GameObject ItemSlotPrefab;

    [SerializeField] private int ItemsRow;
    [SerializeField] private Vector2 SlotSize;
    private GameObject ItemSlots;

    private List<ItemviewSlot> ItemButtonObjects;

    private void Awake()
    {
        if (ItemButtonObjects == null) ItemButtonObjects = new List<ItemviewSlot>();
    }

    public void Start()
    {
        if (CraftPuzzleCore.Instance.DebugMode)
            SetItemWindow(items);
    }

    public void SetItemWindow(Ingredient_Item[] _items)
    {
        foreach (var i in ItemButtonObjects)
        {
            var current = i;
            ItemButtonObjects.Remove(i);
            Destroy(current);
        }

        for(int i = 0; i < _items.Length; i++)
        {
            GameObject newSlot = Instantiate(ItemSlotPrefab, Viewport, false);
            newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2((i % 3) * SlotSize.x, i/3 * -SlotSize.y);
            newSlot.GetComponent<ItemviewSlot>().SetItemData(_items[i]);
            ItemButtonObjects.Add(newSlot.GetComponent<ItemviewSlot>());
        }
    }
}
