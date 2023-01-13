using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "creator item")]
public class ItemCreator : ScriptableObject
{
    public List<Item> items;
}
