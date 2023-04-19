using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot instance;
    public SlotItem dragSlot;


    [SerializeField] private Image itemImage;

    public void Start()
    {
        instance = this;
    }

    public void DragSetImage(Sprite _itemImage)
    {
        itemImage.sprite = _itemImage;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
