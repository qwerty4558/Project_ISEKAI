using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UsageSlotSingle
{
    public Image slot_itemImage;
    public Image slot_back;
    [HideInInspector] public bool SlotOpened = false;
    [HideInInspector] public bool Filled = false;
}

public class UsageSlot : MonoBehaviour
{
    [SerializeField] private Sprite spr_Opened;
    [SerializeField] private Sprite spr_Closed;
    
    [SerializeField] private UsageSlotSingle[] SlotObjects;

    public void SetUsageSlot(Result_Item item)
    {
        for(int i = 0; i < item.puzzle_usage; i++)
        {
            SlotObjects[i].slot_back.sprite = spr_Opened;
            SlotObjects[i].SlotOpened = true;
            SlotObjects[i].Filled = false;
            SlotObjects[i].slot_itemImage.enabled = false;
        }
        for(int i = item.puzzle_usage; i < SlotObjects.Length; i++)
        {
            SlotObjects[i].slot_back.sprite = spr_Closed;
            SlotObjects[i].SlotOpened = false;
            SlotObjects[i].Filled = false;
            SlotObjects[i].slot_itemImage.enabled = false;
        }
    }

    public void InsertIngredient(Ingredient_Item item)
    {
        if (SlotsFull()) return;

        for(int i = 0; i < SlotObjects.Length; i++)
        {
            if(SlotObjects[i].SlotOpened)
            {
                if (!SlotObjects[i].Filled)
                {
                    SlotObjects[i].slot_itemImage.enabled = true;
                    SlotObjects[i].slot_itemImage.sprite = Resources.Load<Sprite>(item.icon_File_Name);
                    SlotObjects[i].Filled = true;
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    public bool SlotsFull()
    {
        for(int i = 0; i < SlotObjects.Length; i++)
        {
            if (!SlotObjects[i].SlotOpened) continue;

            if (SlotObjects[i].SlotOpened != SlotObjects[i].Filled)
                return false;
        }

        return true;
    }


}
