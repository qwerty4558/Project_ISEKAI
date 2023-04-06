using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Puzzle_Board_Creator : MonoBehaviour
{
    [Header("Piece Prefabs")]
    [SerializeField] Puzzle_Piece start_Piece;
    [SerializeField] Puzzle_Piece empty_Piece;
    [SerializeField] Puzzle_Piece no_Empty_Piece;
    [SerializeField] Puzzle_Piece end_Piece;

    [SerializeField] Point pos;
    [SerializeField] Result_Item resultBoard;

    [SerializeField]GameObject canvas;

    public struct Data
    {
        public int row;
        public int col;
    }


    private Queue<Data> RoadQueue = new Queue<Data>();

    private void Start()
    {
        pos.x = resultBoard.board.GetLength(0);
        pos.y = resultBoard.board.GetLength(1);

        CreateBoard();
    }
    

    public void CreateBoard()
    {      
        int row = resultBoard.board.GetLength(0);
        int col = resultBoard.board.GetLength(1);

        Debug.Log(resultBoard.board.GetLength(0));
        Debug.Log(resultBoard.board.GetLength(1));

        Data temp = new Data();

        for(int i = 0; i < row; ++i) // 왼쪽에서 오른쪽
        {
            for(int j = 0; j < col; ++j) // 위에서 아래로
            {
                switch (resultBoard.board[i, j])
                {
                    case PUZZLE_STATE.NoInsert:
                        PrintPiece(no_Empty_Piece, i, j);
                        break;
                    case PUZZLE_STATE.Insert:
                        PrintPiece(empty_Piece, i, j);
                        temp.row = i;
                        temp.col = j;
                        RoadQueue.Enqueue(temp);
                        break;
                    case PUZZLE_STATE.Finish:
                        PrintPiece(end_Piece, i, j);
                        break;
                    default:
                        PrintPiece(start_Piece, i, j);
                        break;
                }
            }
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
}
