using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemviewSlot : MonoBehaviour
{
    [SerializeField] private Ingredient_Item ItemData;
    [SerializeField] private Image image;
    [SerializeField] private Image patternImage;
    [SerializeField] private TextMeshProUGUI quantatyText;


    public void SetItemData(Ingredient_Item item)
    {
        ItemData = item;
        image.sprite = Resources.Load<Sprite>(item.icon_File_Name);
        patternImage.sprite = item.itemPatternImage;
        quantatyText.text = item.count.ToString();
    }

    public void WriteOnPot()
    {
        if (!CraftPuzzleCore.Instance.TryPuzzlePiece(ItemData)) return;


        CraftPuzzleCore.Instance.WritePuzzlePiece(ItemData);
    }

    public void OnItemButtonEnter()
    {
        CraftPuzzleCore.Instance.VisualizeTile(ItemData);
    }
}
