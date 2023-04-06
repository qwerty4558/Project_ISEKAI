using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JH
{
    public class equipmentItem : MonoBehaviour
    {
        [SerializeField] private int itemIndex;
        [SerializeField] private puzzleChecker puzzle;
        private int[,] arr;

        private void Awake()
        {
            int a = 0;
            arr = new int[9, 9];
            
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    arr[i, j] = 2;
                    a++;
                }
            }

            if (itemIndex == 0)
            {
                arr[0, 3] = 0;
                arr[1, 3] = 3;
                arr[2, 2] = 3;
                arr[3, 1] = 3;
                arr[4, 1] = 3;
                arr[5, 2] = 3;
                arr[5, 3] = 3;
                arr[6, 4] = 3;
                arr[7, 4] = 3;
                arr[8, 3] = 1;
                /*
                2 2 2 1 2 2 2 2 2
                2 2 2 2 3 2 2 2 2
                2 2 2 2 3 2 2 2 2
                2 2 3 3 2 2 2 2 2
                2 3 2 2 2 2 2 2 2
                2 3 2 2 2 2 2 2 2
                2 2 3 2 2 2 2 2 2
                2 2 2 3 2 2 2 2 2
                2 2 2 0 2 2 2 2 2
                */
                puzzle.arr1 = arr;
            }
            else if(itemIndex == 1)
            {
                arr[0, 3] = 0;
                arr[1, 4] = 3;
                arr[2, 4] = 3;
                arr[3, 2] = 3;
                arr[3, 3] = 3;
                arr[4, 1] = 3;
                arr[5, 2] = 3;
                arr[6, 3] = 3;
                arr[7, 4] = 3;
                arr[7, 5] = 3;
                arr[8, 6] = 1;
                /*
                2 2 2 2 2 2 1 2 2
                2 2 2 2 3 3 2 2 2
                2 2 2 3 2 2 2 2 2
                2 2 3 2 2 2 2 2 2
                2 3 2 2 2 2 2 2 2
                2 2 3 3 2 2 2 2 2
                2 2 2 2 3 2 2 2 2
                2 2 2 2 3 2 2 2 2
                2 2 2 0 2 2 2 2 2
                */
                puzzle.arr2 = arr;
            }
            else if(itemIndex == 2)
            {
                arr[0, 3] = 0;
                arr[1, 4] = 3;
                arr[2, 5] = 3;
                arr[3, 5] = 3;
                arr[4, 6] = 3;
                arr[5, 5] = 3;
                arr[6, 6] = 3;
                arr[7, 5] = 3;
                arr[8, 4] = 1;
                puzzle.arr3 = arr;
            }
            else if(itemIndex == 3)
            {
                arr[0, 3] = 0;
                arr[1, 4] = 3;
                arr[2, 3] = 3;
                arr[3, 4] = 3;
                arr[3, 5] = 3;
                arr[4, 6] = 3;
                arr[5, 5] = 3;
                arr[6, 4] = 3;
                arr[7, 4] = 3;
                arr[8, 4] = 1;
                puzzle.arr4 = arr;
            }

            
        }

    }

}

