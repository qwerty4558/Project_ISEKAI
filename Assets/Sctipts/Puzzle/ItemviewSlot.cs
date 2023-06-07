using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemviewSlot : MonoBehaviour
{
    [SerializeField] protected Ingredient_Item itemData;
    public Ingredient_Item ItemData { get { return itemData; } }
    [SerializeField] protected Image image;
    [SerializeField] protected Image patternImage;
    [SerializeField] protected TextMeshProUGUI quantatyText;

    [ReadOnly] public int itemUsed = 0;


    public virtual void SetItemData(Ingredient_Item item)
    {
        itemUsed = 0;
        itemData = item;
        image.sprite = Resources.Load<Sprite>(item.icon_File_Name);
        patternImage.sprite = item.itemPatternImage;
        quantatyText.text = (item.appraiseCount - itemUsed).ToString();
    }

    public virtual void ResetText()
    {
        quantatyText.text = (itemData.appraiseCount - itemUsed).ToString();
    }

    public virtual void WriteOnPot()
    {
        if (!CraftPuzzleCore.Instance.TryPuzzlePiece(itemData) || (itemData.appraiseCount - itemUsed) <= 0) 
        {
            CraftPuzzleCore.sound.Play_No_Isplay("NoInsertPieces");
        }

        

        else CraftPuzzleCore.Instance.WritePuzzlePiece(itemData);
    }

    public virtual void OnItemButtonEnter()
    {
        CraftPuzzleCore.Instance.VisualizeTile(itemData);
    }
}