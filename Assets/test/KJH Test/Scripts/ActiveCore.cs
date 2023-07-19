using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCore : MonoBehaviour
{
    [SerializeField] GameObject puzzleCore;
    [SerializeField] float puzzle_Time = 10f;
    [SerializeField] float player_Damage = 10f;
    [SerializeField] float boss_Damage = 33f;

    void Start()
    {
        puzzleCore.SetActive(false);
    }

    private void Update()
    {
        if (puzzleCore.activeSelf)
        {
            puzzle_Time -= Time.deltaTime;

            if (puzzle_Time < 0f)
            {
                PlayerController.instance.GetDamage(10f);
                puzzle_Time = 10f;
            }
        }
    }

    public void OpenCore()
    {
        puzzleCore.SetActive(true);
    }

    public void CloseCore()
    {
        puzzleCore.SetActive(false);
    }

    public void ClearPuzzle()
    {

    }
}
