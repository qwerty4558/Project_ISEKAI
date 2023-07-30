using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCore : MonoBehaviour
{
    [SerializeField] GameObject puzzleCore;
    [SerializeField] GameObject magicStone;
    [SerializeField] float puzzle_Time = 10f;
    [SerializeField] float player_Damage = 10f;
    [SerializeField] float boss_Damage = 33f;
    [SerializeField] int index;
    [SerializeField] BOSS_Witch _witch;
    [SerializeField] PlayerController _player;

    void Start()
    {
        puzzleCore.SetActive(false);
        _witch = FindObjectOfType<BOSS_Witch>();
        _player = PlayerController.instance;
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

    public void OpenCore(GameObject _object, int _index)
    {
        _player.ControlEnabled = false;
        puzzleCore.SetActive(true);
        index = _index;
        magicStone = _object;
    }

    public void CloseCore()
    {
        puzzleCore.SetActive(false);
        _player.ControlEnabled = true;
    }

    public void ClearPuzzle()
    {
        _player.ControlEnabled = true;
        GameObject destroyObj = magicStone;
        _witch.StoneBreakCheck(index);
        _witch.GetDamage(boss_Damage);
        Destroy(destroyObj);
        puzzleCore.SetActive(false);
    }
}
