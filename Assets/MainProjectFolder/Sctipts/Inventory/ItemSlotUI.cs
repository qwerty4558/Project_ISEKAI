using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ItemSlotUI : MonoBehaviour
{
    [Tooltip("������ ������ �̹���")]
    [SerializeField] private Image iconImage;

    [Tooltip("������ ���� �ؽ�Ʈ")]
    [SerializeField] private Text amountText;

    [Tooltip("������ ��Ŀ���� �� ��Ÿ���� ���̶���Ʈ �̹���")]
    [SerializeField] private Image highlightImage;

    [Space]
    [Tooltip("���̶���Ʈ �̹��� ���� ��")]
    [SerializeField] private float highlightAlpha = 0.5f;

    [Tooltip("���̶���Ʈ �ҿ� �ð�")]
    [SerializeField] private float highlightFadeDuration = 0.2f;


    /// <summary> ������ �ε��� </summary>
    public int Index { get; private set; }

    /// <summary> ������ �������� �����ϰ� �ִ��� ���� </summary>
    public bool HasItem => iconImage.sprite != null;

    /// <summary> ���� ������ �������� ���� </summary>
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

    // ���� ���̶���Ʈ ���İ�
    private float currentHLAlpha = 0f;

    private bool isAccessibleSlot = true; // ���� ���ٰ��� ����
    private bool isAccessibleItem = true; // ������ ���ٰ��� ����

    /// <summary> ��Ȱ��ȭ�� ������ ���� </summary>
    private static readonly Color InaccessibleSlotColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
    /// <summary> ��Ȱ��ȭ�� ������ ���� </summary>
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
