using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    private static ItemInfoUI instance;
    public static ItemInfoUI Instance
    {
        get { return instance; }
    }

    [SerializeField] private GameObject ItemInfoPrefab;
    [SerializeField] private float slotSpace = 200.0f;
    private RectTransform rectTransform;

    private float slotOffset = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, Vector3.up * slotOffset, 0.2f);
    }

    public void InsertSlot(Ingredient_Item item)
    {
        GameObject newSlot = Instantiate(ItemInfoPrefab, transform);
        newSlot.GetComponent<ItemSlotUI_Single>().SetUIInfo(item.name, Resources.Load<Sprite>(item.icon_File_Name));
        newSlot.GetComponent<RectTransform>().localPosition += Vector3.down * slotOffset;
        slotOffset += slotSpace;
    }
}
