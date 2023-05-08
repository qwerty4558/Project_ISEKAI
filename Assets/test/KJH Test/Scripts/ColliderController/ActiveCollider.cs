using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCollider : MonoBehaviour
{
    [SerializeField] private Ingredient_Item _item;
    [SerializeField] private int _item_Value;
    private InventoryTitle _inventory;

    private void Start()
    {
        _inventory = FindObjectOfType<InventoryTitle>();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
