using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ItemSlotUI : MonoBehaviour
{
    [Tooltip("아이템 아이콘 이미지")]
    [SerializeField] private Image iconImage;

    [Tooltip("아이템 개수 텍스트")]
    [SerializeField] private Text amountText;

    [Tooltip("슬롯이 포커스될 때 나타나는 하이라이트 이미지")]
    [SerializeField] private Image highlightImage;

    [Space]
    [Tooltip("하이라이트 이미지 알파 값")]
    [SerializeField] private float highlightAlpha = 0.5f;

    [Tooltip("하이라이트 소요 시간")]
    [SerializeField] private float highlightFadeDuration = 0.2f;


    /// <summary> 슬롯의 인덱스 </summary>
    public int Index { get; private set; }

    /// <summary> 슬롯이 아이템을 보유하고 있는지 여부 </summary>
    public bool HasItem => iconImage.sprite != null;

    /// <summary> 접근 가능한 슬롯인지 여부 </summary>
    public bool IsAccessible => isAccessibleSlot && isAccessibleItem;

    public RectTransform SlotRect => slotRect;
    public RectTransform IconRect => iconRect;


    private InventoryUI inventoryUI;

    private RectTransform slotRect;
    private RectTransform iconRect;
    private RectTransform highlightRect;

    private GameObject iconGo;
    private GameObject textGo;
    private GameObject highlightGo;

    private Image slotImage;

    // 현재 하이라이트 알파값
    private float currentHLAlpha = 0f;

    private bool isAccessibleSlot = true; // 슬롯 접근가능 여부
    private bool isAccessibleItem = true; // 아이템 접근가능 여부

    /// <summary> 비활성화된 슬롯의 색상 </summary>
    private static readonly Color InaccessibleSlotColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
    /// <summary> 비활성화된 아이콘 색상 </summary>
    private static readonly Color InaccessibleIconColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    private void ShowIcon() => iconGo.SetActive(true);

    private void HideIcon() => iconGo.SetActive(false);

    private void ShowText() => textGo.SetActive(true);

    private void HideText() => textGo.SetActive(false);

    public void SetSlotIndex(int index) => Index = index;

    public void SetSlotAccessiableState(bool value)
    {
        if (isAccessibleSlot == value)
        {
            return;
        }

        if (value)
        {
            slotImage.color = Color.black;
        }
        else
        {
            slotImage.color = InaccessibleSlotColor;
            HideIcon();
            HideText();
        }

        isAccessibleSlot = value;
    }

    public void SetItemAccessibleState(bool value)
    {
        if (isAccessibleItem == value)
        {
            return;
        }

        if (value)
        {
            iconImage.color = Color.white;
            amountText.color = Color.white;
        }
        else
        {
            iconImage.color = InaccessibleSlotColor;
            amountText.color = InaccessibleSlotColor;
        }

        isAccessibleItem = value;
    }

    public void SwapOrMoveIcon(ItemSlotUI other)
    {
        if(other == null) return;
        if(other == this) return;
        if(!this.IsAccessible) return;
        if(!other.IsAccessible) return;

        var temp = iconImage.sprite;

        if (other.HasItem)
        {
            SetItem(other.iconImage.sprite);
        }
        else ReMoveItem();

        other.SetItem(temp);
    }

    public void SetItem(Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            iconImage.sprite = itemSprite;
            ShowIcon();
        }
        else ReMoveItem();
    }

    public void ReMoveItem()
    {
        iconImage.sprite = null;
        HideIcon();
        HideText();
    }

    public void SetIconAlpha(float alpha)
    {
        iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, alpha);
    }

    public void SetItemAmount(int amount)
    {
        if(HasItem&&amount > 1)
        {
            ShowText();
        }
        else HideText();

        amountText.text = amount.ToString();
    }
}
