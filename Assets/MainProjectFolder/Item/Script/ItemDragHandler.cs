using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
[System.Serializable]
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
 
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
