using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleIngredientItems : MonoBehaviour
{
    [SerializeField] private List<Ingredient_Item> items;
    [SerializeField] private Transform Viewport;
    [SerializeField] private GameObject ItemSlotPrefab;

    [SerializeField] private int ItemsRow;
    [SerializeField] private Vector2 SlotSize;
    
    public void SetItemWindow(Ingredient_Item[] _items)
    {
        for(int i = 0; i < _items.Length; i++)
        {

        }
    }
}
