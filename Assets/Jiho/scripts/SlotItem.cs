using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private string itemName;
    [SerializeField] private int id;
    [SerializeField] private int count;

    public Image ItemImage { get => ItemImage; set => ItemImage = value; }
    public string ItemName { get => itemName; set => itemName = value;  }
    public int ID { get => id; set => id = value; }
    public int Count { get => count; set => count = value;  }




}
