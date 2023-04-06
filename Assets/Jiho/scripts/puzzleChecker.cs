using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JH
{
    public class puzzleChecker : MonoBehaviour
    {
        private GameObject[,] puzzle; //[단,번째] 아래부터 0단, 왼쪽부터 1번째
        [SerializeField] private GameObject[] initPuzzle;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Image mainObj;
        private int count = 0;
        public int[,] arr1;
        public int[,] arr2;
        public int[,] arr3;
        public int[,] arr4;
        //0 시작, 1 골인, 2 벽, 3가능한 길, 4 간 곳

        private void Awake()
        {
            PuzzleInit();
        }


        private void PuzzleInit()
        {
            puzzle = new GameObject[9, 9];
            int a = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    puzzle[i, j] = initPuzzle[a];
                    a++;
                }
            }
        }

        public void PuzzleChange()
        {
            if(count == 0)
            {
                count++;
                GetPuzzle(arr1);
                mainObj.sprite = sprites[0];
            }
            else if(count == 1)
            {
                count++;
                GetPuzzle(arr2);
                mainObj.sprite = sprites[1];
            } 
            else if(count == 2)
            {
                count++;
                GetPuzzle(arr3);
                mainObj.sprite = sprites[2];
            }
            else if(count == 3)
            {
                count = 0;
                GetPuzzle(arr4);
                mainObj.sprite = sprites[3];
            }
        }

        private void GetPuzzle(int[,] _data)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    puzzle[i, j].GetComponent<pieceData>().Index = _data[i,j];
                }
            }
            SetPuzzle();
        }

        private void SetPuzzle()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puzzle[i, j].GetComponent<pieceData>().Index == 0)
                        puzzle[i, j].GetComponent<Image>().color = Color.red;
                    else if(puzzle[i, j].GetComponent<pieceData>().Index == 1)
                        puzzle[i, j].GetComponent<Image>().color = Color.blue;
                    else if (puzzle[i, j].GetComponent<pieceData>().Index == 2)
                        puzzle[i, j].GetComponent<Image>().color = Color.white;
                    else if (puzzle[i, j].GetComponent<pieceData>().Index == 3)
                        puzzle[i, j].GetComponent<Image>().color = Color.black;
                    else if (puzzle[i, j].GetComponent<pieceData>().Index == 4)
                        puzzle[i, j].GetComponent<Image>().color = Color.yellow;
                }
            }
        }
    }

}

