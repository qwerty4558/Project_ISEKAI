using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemviewSlot : MonoBehaviour
{
    [SerializeField] private Ingredient_Item itemData;
    public Ingredient_Item ItemData { get { return itemData; } }
    [SerializeField] private Image image;
    [SerializeField] private Image patternImage;
    [SerializeField] private TextMeshProUGUI quantatyText;

    [ReadOnly] public int itemUsed = 0;


    public void SetItemData(Ingredient_Item item)
    {
        itemData = item;
        image.sprite = Resources.Load<Sprite>(item.icon_File_Name);
        patternImage.sprite = item.itemPatternImage;
        quantatyText.text = (item.appraiseCount - itemUsed).ToString();
    }

    public void ResetText()
    {
        quantatyText.text = (itemData.appraiseCount - itemUsed).ToString();
    }

    public void WriteOnPot()
    {
        if (!CraftPuzzleCore.Instance.TryPuzzlePiece(itemData) || (itemData.appraiseCount - itemUsed) <= 0) return;

        CraftPuzzleCore.Instance.WritePuzzlePiece(itemData);
    }

    public void OnItemButtonEnter()
    {
        CraftPuzzleCore.Instance.VisualizeTile(itemData);
    }
}