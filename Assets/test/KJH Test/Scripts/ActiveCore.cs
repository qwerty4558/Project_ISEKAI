using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCore : MonoBehaviour
{
    [SerializeField] GameObject puzzleCore;
    [SerializeField] GameObject magicStone;

    [SerializeField] int index;
    [SerializeField] BOSS_Witch _witch;
    [SerializeField] PlayerController _player;
    [SerializeField] UIManager _manager;
    [SerializeField] GameObject questUI;

    void Start()
    {
        questUI = GameObject.FindGameObjectWithTag("QuestUI");
        puzzleCore.SetActive(false);
        _witch = FindObjectOfType<BOSS_Witch>();
        _player = PlayerController.instance;
        _manager = UIManager.Instance;
    }

    private void Update()
    {
        if (puzzleCore.activeSelf)
        {
            _manager.cameraFollow.isInteraction = true;
        }
        else _manager.cameraFollow.isInteraction = false;
    }

    public void OpenCore(GameObject _object, int _index)
    {
        _manager.gameObject.SetActive(false);
        questUI.SetActive(false);

        puzzleCore.SetActive(true);
        index = _index;
        magicStone = _object;
    }

    public void CloseCore()
    {
        _manager.gameObject.SetActive(true);
        questUI.SetActive(true);
        puzzleCore.SetActive(false);
 
        _player.ControlEnabled = true;
        
    }

    public void ClearPuzzle()
    {
        _manager.gameObject.SetActive(true);

        _player.ControlEnabled = true;
        GameObject destroyObj = magicStone;
        _witch.StoneBreakCheck(index);
 
        Destroy(destroyObj);
        puzzleCore.SetActive(false);
    }
}
