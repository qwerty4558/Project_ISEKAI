using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PotSlot
{
    Vacant,
    Blocked,
    Inserted,
    Start,
    Finish
}

public class ItemPot : SerializedMonoBehaviour
{
    [Title("Prefab")]
    [SerializeField] private GameObject slotPrefab;

    [Title("Sprites")]
    [SerializeField] private Sprite slot_vacant;
    [SerializeField] private Sprite slot_blocked;
    [SerializeField] private Sprite slot_Inserted;
    [SerializeField] private Sprite slot_start;
    [SerializeField] private Sprite slot_finish;
    [SerializeField] private Sprite slot_active;

    private PotSlot[,] slot_matrix;
    private List<Ingredient_Item> writedItems;

    private Vector2Int startPoint;
    private Vector2Int finishPoint;
    private Vector2Int activePoint;

    private GameObject[,] slotObjects;

    float slotSize = 100f;

    private void Start()
    {
        writedItems = new List<Ingredient_Item>();
    }

    public void SetItemPot(Result_Item targetItem)
    {
        Vector2Int slotLength = new Vector2Int(targetItem.board.GetLength(0), targetItem.board.GetLength(1));

        this.GetComponent<RectTransform>().sizeDelta = new Vector2( slotLength.x * slotSize, slotLength.y * slotSize);

        slotObjects = new GameObject[slotLength.x, slotLength.y];
        slot_matrix = new PotSlot[slotLength.x, slotLength.y];

        for (int y = 0; y < slotLength.y; y++)
        {
            for (int x = 0; x < slotLength.x; x++)
            {
                slotObjects[x, y] = Instantiate(slotPrefab, transform);
                slotObjects[x, y].GetComponent<RectTransform>().anchoredPosition = new Vector3(slotSize * x, slotSize * -y);
                Image slotImage = slotObjects[x,y].GetComponent<Image>();

                if (targetItem.board[x, y] == PUZZLE_STATE.NoInsert)
                {
                    slot_matrix[x, y] = PotSlot.Blocked;
                    slotImage.sprite = slot_blocked;
                }
                else if (targetItem.board[x, y] == PUZZLE_STATE.Insert)
                {
                    slot_matrix[x, y] = PotSlot.Vacant;
                    slotImage.sprite = slot_vacant;
                }
                else if (targetItem.board[x, y] == PUZZLE_STATE.Start)
                {
                    slot_matrix[x, y] = PotSlot.Start;
                    slotImage.sprite = slot_active;
                    startPoint = new Vector2Int(x, y);
                }
                else if (targetItem.board[x, y] == PUZZLE_STATE.Finish)
                {
                    slot_matrix[x, y] = PotSlot.Finish;
                    slotImage.sprite = slot_finish;
                    finishPoint = new Vector2Int(x, y);
                }
                else
                {
                    slot_matrix[x, y] = PotSlot.Blocked;
                    slotImage.sprite = slot_blocked;
                }
            }
        }

        activePoint = new Vector2Int(startPoint.x,startPoint.y);
    }

    public void PuzzlePieceVisualize(Ingredient_Item insertItem)
    {
    }

    public bool TryPuzzlePiece(Ingredient_Item insertItem)
    {
        if (insertItem.puzzle == null)
        {
            Debug.LogError(insertItem.name_KR + " : 해당 Ingredient_Item은 퍼즐 조각 정보가 없습니다!");
            return false;
        }

        bool result = true;

        Vector2Int insertPieceSize = new Vector2Int(insertItem.puzzle.GetLength(0),insertItem.puzzle.GetLength(1));
        Vector2Int pivot = new Vector2Int(insertPieceSize.x/2, insertPieceSize.y/2);

        for (int y = 0; y < insertPieceSize.y; y++)
        {
            for (int x = 0; x < insertPieceSize.x; x++)
            {
                if (x == pivot.x && y == pivot.y) continue;

                Vector2Int currentPoint = new Vector2Int(activePoint.x + x - pivot.x, activePoint.y + y - pivot.y);

                if (insertItem.puzzle[x, y] == PUZZLE_PIECE.PIECE || insertItem.puzzle[x, y] == PUZZLE_PIECE.END)
                {
                    if (!IsSlotVacant(currentPoint.x, currentPoint.y)) result = false;
                }
            }
        }

        return result;
    }

    public void WritePuzzlePiece(Ingredient_Item insertItem)
    {
        Vector2Int insertPieceSize = new Vector2Int(insertItem.puzzle.GetLength(0), insertItem.puzzle.GetLength(1));
        Vector2Int pivot = new Vector2Int(insertPieceSize.x / 2, insertPieceSize.y / 2);
        Vector2Int EndPoint = Vector2Int.zero;

        for (int y = 0; y < insertPieceSize.y; y++)
        {
            for (int x = 0; x < insertPieceSize.x; x++)
            {
                Vector2Int currentPoint = new Vector2Int(activePoint.x + x - pivot.x, activePoint.y + y - pivot.y);

                if (insertItem.puzzle[x, y] == PUZZLE_PIECE.PIECE)
                {
                    slot_matrix[currentPoint.x,currentPoint.y] = PotSlot.Inserted;
                    slotObjects[currentPoint.x, currentPoint.y].GetComponent<Image>().sprite = slot_Inserted;
                }

                if(insertItem.puzzle[x,y] == PUZZLE_PIECE.END)
                {
                    slot_matrix[currentPoint.x, currentPoint.y] = PotSlot.Inserted;
                    slotObjects[currentPoint.x, currentPoint.y].GetComponent<Image>().sprite = slot_Inserted;
                    EndPoint = new Vector2Int(currentPoint.x,currentPoint.y);
                }
            }
        }

        activePoint = EndPoint;
        slotObjects[activePoint.x, activePoint.y].GetComponent<Image>().sprite = slot_active;
        writedItems.Add(insertItem);

        if(slot_matrix[activePoint.x,activePoint.y] == PotSlot.Finish)
        {
            if (CraftPuzzleCore.Instance.OnPuzzleComplete != null)
            {
                CraftPuzzleCore.Instance.OnPuzzleComplete.Invoke();
            }
        }
    }

    private bool IsSlotVacant(int x, int y)
    {
        if (slot_matrix.GetLength(0) <= x || x < 0) return false;
        else if (slot_matrix.GetLength(1) <= y || y < 0 ) return false;

        if (slot_matrix[x, y] == PotSlot.Vacant) return true;
        else return false;
    }

    public void ResetPot()
    {

    }
}
