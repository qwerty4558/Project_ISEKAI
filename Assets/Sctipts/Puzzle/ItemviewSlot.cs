using UnityEngine;
using UnityEngine.UI;

public class ItemviewSlot : MonoBehaviour
{
    [SerializeField] private Ingredient_Item ItemData;
    [SerializeField] private Image image;
<<<<<<< HEAD
=======
    [SerializeField] private Image patternImage;
<<<<<<< HEAD
>>>>>>> parent of c16f176c (Craftpuzzle Work 5)
=======
>>>>>>> parent of c16f176c (Craftpuzzle Work 5)


    public void SetItemData(Ingredient_Item item)
    {
        ItemData = item;
        image.sprite = Resources.Load<Sprite>(item.icon_File_Name);
<<<<<<< HEAD
=======
        patternImage.sprite = item.itemPatternImage;
<<<<<<< HEAD
>>>>>>> parent of c16f176c (Craftpuzzle Work 5)
=======
>>>>>>> parent of c16f176c (Craftpuzzle Work 5)
    }

    public void WriteOnPot()
    {
        if (!CraftPuzzleCore.Instance.TryPuzzlePiece(ItemData)) return;


        CraftPuzzleCore.Instance.WritePuzzlePiece(ItemData);
    }
}
