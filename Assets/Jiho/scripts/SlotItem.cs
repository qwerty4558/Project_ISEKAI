using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private int id;
    [SerializeField] private int count;
    [SerializeField] private Image slotImage;
    [SerializeField] private string status;
    [SerializeField] private string route;
    private bool isCheck;

    public Sprite itemImage;
    public string ItemName { get => itemName; set => itemName = value;  }
    public string Status { get => status; set => status = value; }
    public string Route { get => route; set => route = value; }
    public int ID { get => id; set => id = value; }
    public int Count { get => count; set => count = value;  }
    public bool IsCheck { get => isCheck; set => isCheck = value; }

    private void Update()
    {
        if(!isCheck && this.gameObject.CompareTag("SlotItem"))
        {
            if(id != 0)
            {
                isCheck = true;
                slotImage.enabled = true;
                slotImage.sprite = itemImage;
            }
            else
            {
                isCheck = false;
                slotImage.enabled = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
