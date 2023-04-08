using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


public class Puzzle_Board_Creator : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI notPieceText;
    [Header("Piece Prefabs")]
    [SerializeField] Puzzle_Piece start_Piece;
    [SerializeField] Puzzle_Piece empty_Piece;
    [SerializeField] Puzzle_Piece no_Empty_Piece;
    [SerializeField] Puzzle_Piece end_Piece;
    [SerializeField] Ingredient_Item item_Piece;

    [SerializeField] Point pos;
    [SerializeField] Result_Item resultBoard;

    [SerializeField]GameObject canvas;

    [SerializeField] private bool isSame;
    private bool isCreate;
    private bool isRetry;
   
    private Data temp;

    public struct Data
    {
        public int row;
        public int col;
    }


    private Queue<Data> RoadQueue = new Queue<Data>();

    private void Start()
    {
        if(resultBoard != null)
        {
            pos.x = resultBoard.board.GetLength(0);
            pos.y = resultBoard.board.GetLength(1);

            CreateBoard();

            InitRoadMap();
        }
    }

    private void ClearCheck()
    {

    }

    private IEnumerator NotPieceCor()
    {

        notPieceText.color = new Color(0, 0, 0, 255);
        notPieceText.rectTransform.position = new Vector3(0, -130f,0);
        yield return new WaitForSeconds(0.5f);
        notPieceText.rectTransform.DOMoveY(-200f, 2f);
        notPieceText.DOFade(0, 5f);


        yield return null;
    }

    public void CheckPuzzle(Ingredient_Item piece)
    {
        if(!isCreate)
        {
            temp = RoadQueue.Dequeue();
            isCreate = true;
        }

        int[,] checker = new int[3, 3];
        int[,] checking = new int[3, 3];
        
        if(temp.row != 0 && temp.col != 0)
            if (resultBoard.board[temp.row - 1, temp.col - 1] == PUZZLE_STATE.Insert)
                checker[0, 0] = 1;
            else if (resultBoard.board[temp.row - 1, temp.col - 1] != PUZZLE_STATE.Insert)
                checker[0, 0] = 0;

        if (temp.col != 0)
            if (resultBoard.board[temp.row, temp.col - 1] == PUZZLE_STATE.Insert)
                checker[1, 0] = 1;
            else if (resultBoard.board[temp.row, temp.col - 1] != PUZZLE_STATE.Insert)
                checker[1, 0] = 0;

        if (temp.row != 6 && temp.col != 0)
            if (resultBoard.board[temp.row + 1, temp.col - 1] == PUZZLE_STATE.Insert)
                checker[2, 0] = 1;
            else if (resultBoard.board[temp.row + 1, temp.col - 1] != PUZZLE_STATE.Insert)
                checker[2, 0] = 0;

        if (temp.row != 0)
            if (resultBoard.board[temp.row - 1, temp.col] == PUZZLE_STATE.Insert)
                checker[0, 1] = 1;
            else if (resultBoard.board[temp.row - 1, temp.col] != PUZZLE_STATE.Insert)
                checker[0, 1] = 0;

        //
        if (resultBoard.board[temp.row, temp.col] == PUZZLE_STATE.Insert)
            checker[1, 1] = 1;
        else if (resultBoard.board[temp.row, temp.col] != PUZZLE_STATE.Insert)
            checker[1, 1] = 0;
        //

        if (temp.row != 6)
            if (resultBoard.board[temp.row + 1, temp.col] == PUZZLE_STATE.Insert)
                checker[2, 1] = 1;   
            else if (resultBoard.board[temp.row + 1, temp.col] != PUZZLE_STATE.Insert)
                checker[2, 1] = 0;

        if (temp.row != 0 && temp.col != 6)
            if (resultBoard.board[temp.row - 1, temp.col + 1] == PUZZLE_STATE.Insert)
                checker[0, 2] = 1;
            else if (resultBoard.board[temp.row - 1, temp.col + 1] != PUZZLE_STATE.Insert)
                checker[0, 2] = 0;

        if (temp.col != 6)
            if (resultBoard.board[temp.row, temp.col + 1] == PUZZLE_STATE.Insert)
                checker[1, 2] = 1;
            else if (resultBoard.board[temp.row, temp.col + 1] != PUZZLE_STATE.Insert) 
                checker[1, 2] = 0;

        if (temp.row != 6 && temp.col != 6)
            if (resultBoard.board[temp.row + 1, temp.col + 1] == PUZZLE_STATE.Insert)
                checker[2, 2] = 1;
            else if (resultBoard.board[temp.row + 1, temp.col + 1] != PUZZLE_STATE.Insert)
                checker[2, 2] = 0;
            

        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (piece.puzzle[i, j] == 0) checking[i, j] = 0;
                else checking[i, j] = 1;

                if (checker[i,j] != checking[i,j])
                {
                    StartCoroutine(NotPieceCor());
                    return;
                }

            }
        }

        if (temp.row != 0 && temp.col != 0)
            if (resultBoard.board[temp.row - 1, temp.col - 1] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row - 1, temp.col - 1] = PUZZLE_STATE.Start;

        if (temp.col != 0)
            if (resultBoard.board[temp.row, temp.col - 1] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row, temp.col - 1] = PUZZLE_STATE.Start;

        if (temp.row != 6 && temp.col != 0)
            if (resultBoard.board[temp.row + 1, temp.col - 1] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row + 1, temp.col - 1] = PUZZLE_STATE.Start;

        if (temp.row != 0)
            if (resultBoard.board[temp.row - 1, temp.col] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row - 1, temp.col] = PUZZLE_STATE.Start;

        //
        if (resultBoard.board[temp.row, temp.col] == PUZZLE_STATE.Insert)
            resultBoard.board[temp.row, temp.col] = PUZZLE_STATE.Start;
        //

        if (temp.row != 6)
            if (resultBoard.board[temp.row + 1, temp.col] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row + 1, temp.col] = PUZZLE_STATE.Start;

        if (temp.row != 0 && temp.col != 6)
            if (resultBoard.board[temp.row - 1, temp.col + 1] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row - 1, temp.col + 1] = PUZZLE_STATE.Start;

        if (temp.col != 6)
            if (resultBoard.board[temp.row, temp.col + 1] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row, temp.col + 1] = PUZZLE_STATE.Start;

        if (temp.row != 6 && temp.col != 6)
            if (resultBoard.board[temp.row + 1, temp.col + 1] == PUZZLE_STATE.Insert)
                resultBoard.board[temp.row + 1, temp.col + 1] = PUZZLE_STATE.Start;
        isCreate = false;
        CreateBoard();
        RoadQueue.Dequeue();


    }

    private void InitRoadMap()
    {
        Data temp = new Data();

        for(int i = 0; i < resultBoard.road.GetLength(1); i++)
        {
            temp.row = resultBoard.road[0, i];
            temp.col = resultBoard.road[1, i];
            RoadQueue.Enqueue(temp);
        }

    }


    public void CreateBoard()
    {      
        int row = resultBoard.board.GetLength(0);
        int col = resultBoard.board.GetLength(1);

        if(!isRetry)
        {
            isRetry = true;
            for(int i = 0; i < 7; i++)
                for(int j = 0; j < 7; j++)
                    if(resultBoard.board[i, j] == PUZZLE_STATE.Start)
                        resultBoard.board[i, j] = PUZZLE_STATE.Insert;

            if (resultBoard.index == 0) //°¡Á× Àå°©
                resultBoard.board[3, 6] = PUZZLE_STATE.Start;
            else if(resultBoard.index == 1) //Ã¶µµ³¢
                resultBoard.board[0, 6] = PUZZLE_STATE.Start;
            else if(resultBoard.index == 2) //°î±ªÀÌ
                resultBoard.board[0, 5] = PUZZLE_STATE.Start;
            else if(resultBoard.index == 3) //¹æÆÐ
                resultBoard.board[3, 6] = PUZZLE_STATE.Start;
        }

        for(int i = 0; i < row; ++i) // ¿ÞÂÊ¿¡¼­ ¿À¸¥ÂÊ
            for(int j = 0; j < col; ++j) // À§¿¡¼­ ¾Æ·¡·Î
                switch (resultBoard.board[i, j])
                {
                    case PUZZLE_STATE.NoInsert:
                        PrintPiece(no_Empty_Piece, i, j);
                        break;
                    case PUZZLE_STATE.Start:
                        PrintPiece(start_Piece, i, j);
                        break;
                    case PUZZLE_STATE.Insert:
                        PrintPiece(empty_Piece, i, j);
                        break;
                    case PUZZLE_STATE.Finish:
                        PrintPiece(end_Piece, i, j);
                        break;
                    default:
                        PrintPiece(start_Piece, i, j);
                        break;
                }
    }

    void PrintPiece(Puzzle_Piece piece, int _x, int _y)
    {
        GameObject pieces;
        pieces = Instantiate(piece.gameObject, new Vector3((_x*100)-300, (_y*-100)+300), Quaternion.identity);
        pieces.transform.SetParent(canvas.transform, false);

        RectTransform rt = piece.GetComponent<RectTransform>();

        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        piece.name = $"Piece {_x} {_y}";
    }

    public void SelectPuzzle(Result_Item result_item)
    {
        isRetry = false;
        isCreate = false;
        resultBoard = result_item;
        RoadQueue.Clear();

        CreateBoard();
        InitRoadMap();
    }
}
