using DG.Tweening;
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

    [Title("References")]
    [SerializeField] private PotVisualizer potVisulaizer;
    [SerializeField] private Transform slotMatrixTF;

    private PotSlot[,] slot_matrix;
    private List<Ingredient_Item> writedItems;
    public List<Ingredient_Item> WritedItems { get { return writedItems; } }

    private Vector2Int startPoint;
    private Vector2Int finishPoint;
    private Vector2Int activePoint;

    private GameObject[,] slotObjects;

    float slotSize = 100f;

    private void Start()
    {
        writedItems = new List<Ingredient_Item>();
    }

    private void OnEnable()
    {
        CraftPuzzleCore.Instance.OnPuzzleComplete.AddListener(OnPuzzleEnd);
    }

    private void OnDisable()
    {
        CraftPuzzleCore.Instance.OnPuzzleComplete.RemoveListener(OnPuzzleEnd);
        Clear();
        potVisulaizer.gameObject.SetActive(false);
    }

    public void SetItemPot(Result_Item targetItem)
    {
        Vector2Int slotLength = new Vector2Int(targetItem.board.GetLength(0), targetItem.board.GetLength(1));

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(slotLength.x * slotSize, slotLength.y * slotSize);
        this.GetComponent<RectTransform>().localScale = new Vector3(700f / (slotLength.x * slotSize), 700f / (slotLength.y * slotSize));

        Clear();

        slotObjects = new GameObject[slotLength.x, slotLength.y];
        slot_matrix = new PotSlot[slotLength.x, slotLength.y];
        finishPoint = new Vector2Int(-1, -1);

        for (int y = 0; y < slotLength.y; y++)
        {
            for (int x = 0; x < slotLength.x; x++)
            {
                slotObjects[x, y] = Instantiate(slotPrefab, slotMatrixTF);
                slotObjects[x, y].GetComponent<RectTransform>().anchoredPosition = new Vector3(slotSize * x, slotSize * -y);
                Image slotImage = slotObjects[x, y].GetComponent<Image>();

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

        activePoint = new Vector2Int(startPoint.x, startPoint.y);

        if (writedItems != null)
            writedItems.Clear();
    }

    public void UndoSetItemPot(Result_Item targetItem)
    {
        Vector2Int slotLength = new Vector2Int(targetItem.board.GetLength(0), targetItem.board.GetLength(1));

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(slotLength.x * slotSize, slotLength.y * slotSize);
        this.GetComponent<RectTransform>().localScale = new Vector3(700f / (slotLength.x * slotSize), 700f / (slotLength.y * slotSize));

        Clear();

        slotObjects = new GameObject[slotLength.x, slotLength.y];
        slot_matrix = new PotSlot[slotLength.x, slotLength.y];
        finishPoint = new Vector2Int(-1, -1);

        for (int y = 0; y < slotLength.y; y++)
        {
            for (int x = 0; x < slotLength.x; x++)
            {
                slotObjects[x, y] = Instantiate(slotPrefab, slotMatrixTF);
                slotObjects[x, y].GetComponent<RectTransform>().anchoredPosition = new Vector3(slotSize * x, slotSize * -y);
                Image slotImage = slotObjects[x, y].GetComponent<Image>();

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

        activePoint = new Vector2Int(startPoint.x, startPoint.y);
    }

    public void SetItempotNone()
    {
        Vector2Int slotLength = new Vector2Int(7,7);

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(slotLength.x * slotSize, slotLength.y * slotSize);
        this.GetComponent<RectTransform>().localScale = new Vector3(700f / (slotLength.x * slotSize), 700f / (slotLength.y * slotSize));

        Clear();

        slotObjects = new GameObject[slotLength.x, slotLength.y];
        slot_matrix = new PotSlot[slotLength.x, slotLength.y];
        finishPoint = new Vector2Int(-1, -1);

        for (int y = 0; y < slotLength.y; y++)
        {
            for (int x = 0; x < slotLength.x; x++)
            {
                slotObjects[x, y] = Instantiate(slotPrefab, slotMatrixTF);
                slotObjects[x, y].GetComponent<RectTransform>().anchoredPosition = new Vector3(slotSize * x, slotSize * -y);
                Image slotImage = slotObjects[x, y].GetComponent<Image>();

                slot_matrix[x, y] = PotSlot.Blocked;
                slotImage.sprite = slot_blocked;
            }
        }

        activePoint = new Vector2Int(startPoint.x, startPoint.y);
    }

    public void PuzzlePieceVisualize(Ingredient_Item insertItem)
    {
        potVisulaizer.gameObject.SetActive(true);
        bool positive = CraftPuzzleCore.Instance.TryPuzzlePiece(insertItem);

        potVisulaizer.Visualize(new Vector2((activePoint.x - slot_matrix.GetLength(0) / 2) * slotSize, -(activePoint.y - slot_matrix.GetLength(1) / 2) * slotSize), insertItem, positive);
    }
    public void DisablePuzzlePieceVisualizer()
    {
        potVisulaizer.gameObject.SetActive(false);
    }

    public bool TryPuzzlePiece(Ingredient_Item insertItem)
    {
        if (insertItem.puzzle == null)
        {
            Debug.LogError(insertItem.name_KR + " :??? Ingredient_Item?? ???? ???? ?????? ???????!");
            return false;
        }

        bool result = true;

        Vector2Int insertPieceSize = new Vector2Int(insertItem.puzzle.GetLength(0), insertItem.puzzle.GetLength(1));
        Vector2Int pivot = new Vector2Int(insertPieceSize.x / 2, insertPieceSize.y / 2);

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
                    slot_matrix[currentPoint.x, currentPoint.y] = PotSlot.Inserted;
                    slotObjects[currentPoint.x, currentPoint.y].GetComponent<Image>().sprite = slot_Inserted;
                }

                if (insertItem.puzzle[x, y] == PUZZLE_PIECE.END)
                {
                    slot_matrix[currentPoint.x, currentPoint.y] = PotSlot.Inserted;
                    slotObjects[currentPoint.x, currentPoint.y].GetComponent<Image>().sprite = slot_Inserted;
                    EndPoint = new Vector2Int(currentPoint.x, currentPoint.y);
                }
            }
        }

        activePoint = EndPoint;
        slotObjects[activePoint.x, activePoint.y].GetComponent<Image>().sprite = slot_active;
        writedItems.Add(insertItem);

        if (activePoint == finishPoint)
        {
            if (CraftPuzzleCore.Instance.OnPuzzleComplete != null)
            {
                CraftPuzzleCore.Instance.OnPuzzleComplete.Invoke();
            }
        }
    }

    public void UndoWritePuzzlePiece(Ingredient_Item insertItem)
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
                    slot_matrix[currentPoint.x, currentPoint.y] = PotSlot.Inserted;
                    slotObjects[currentPoint.x, currentPoint.y].GetComponent<Image>().sprite = slot_Inserted;
                }

                if (insertItem.puzzle[x, y] == PUZZLE_PIECE.END)
                {
                    slot_matrix[currentPoint.x, currentPoint.y] = PotSlot.Inserted;
                    slotObjects[currentPoint.x, currentPoint.y].GetComponent<Image>().sprite = slot_Inserted;
                    EndPoint = new Vector2Int(currentPoint.x, currentPoint.y);
                }
            }
        }

        activePoint = EndPoint;
        slotObjects[activePoint.x, activePoint.y].GetComponent<Image>().sprite = slot_active;
    }

    public void UndoPiece()
    {
        if (writedItems.Count == 0) return;

        writedItems.RemoveAt(writedItems.Count - 1);

        for (int i = 0; i < writedItems.Count; i++)
        {
            UndoWritePuzzlePiece(writedItems[i]);
        }

    }

    private bool IsSlotVacant(int x, int y)
    {
        if (slot_matrix.GetLength(0) <= x || x < 0) return false;
        else if (slot_matrix.GetLength(1) <= y || y < 0) return false;

        if (slot_matrix[x, y] == PotSlot.Vacant || slot_matrix[x, y] == PotSlot.Finish) return true;
        else return false;
    }

    public void OnPuzzleEnd()
    {
        GetComponent<DOTweenAnimation>().DORestartById("ClosePot");

    }

    private void Clear()
    {
        if (slotObjects == null) return;

        foreach (var obj in slotObjects)
        {
            Destroy(obj.gameObject);
        }
    }
}