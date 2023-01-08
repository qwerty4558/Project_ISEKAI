using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("인벤토리의 기본 정보 입니다.")]
    [Range(0, 10)]
    [SerializeField] int horSlotCount = 8;
    [Range(0, 10)]
    [SerializeField] int vertSlotCount = 8;
    [SerializeField] float slotMargin = 8f;
    [SerializeField] float contentAreaPadding = 20f;

    [Range(32, 64)]
    [SerializeField] float slotSize = 64f;

    [Header("여기에 오브젝트를 연결해 주세요")]
    [SerializeField] RectTransform contentAreaRect;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] List<ItemSlotUI> slotUIList;

    void InitSlot()
    {
        slotPrefab.TryGetComponent(out RectTransform slotRect);
        slotRect.sizeDelta = new Vector2(slotSize, slotSize);

        slotPrefab.TryGetComponent(out ItemSlotUI itemslot);

        if(itemslot == null)
        {
            slotPrefab.AddComponent<ItemSlotUI>();            
        }
        slotPrefab.SetActive(false);

        Vector2 beginPos = new Vector2(contentAreaPadding, -contentAreaPadding);
        Vector2 curPos = beginPos;


        slotUIList = new List<ItemSlotUI>(vertSlotCount * horSlotCount);

        for(int j=0;j<vertSlotCount;j++) 
        {
            for(int i=0;i<horSlotCount;i++)
            {
                int slotIndex = (horSlotCount * j) + i;

                var slotRt = CloneSlot();
                slotRt.pivot = new Vector2(0f, 1f);
                slotRt.anchoredPosition = curPos;
                slotRt.gameObject.SetActive(true);
                slotRt.gameObject.name = $"Item Slot [{slotIndex}]";

                var slotUI = slotRt.GetComponent<ItemSlotUI>();
                slotUI.SetSlotIndex(slotIndex);
                slotUIList.Add(slotUI);

                curPos.x += (slotMargin + slotSize);
            }
            curPos.x = beginPos.x;
            curPos.y += (slotMargin + slotSize);
        }

        RectTransform CloneSlot()
        {
            GameObject slotGo = Instantiate(slotPrefab);
            RectTransform rt = slotGo.GetComponent<RectTransform>();
            return rt;
        }
    }
}
