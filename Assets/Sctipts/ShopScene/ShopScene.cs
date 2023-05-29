using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShopScene : MonoBehaviour
{
    [SerializeField] GameObject _craftPuzzle;
    [SerializeField] GameObject _discrimination;
    [SerializeField] GameObject _inGameUI;
    [SerializeField] GameObject _questUI;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scane, LoadSceneMode loadSceneMode)
    {
        if(_inGameUI == null && _questUI == null)
        {
            _inGameUI = GameObject.FindGameObjectWithTag("InGameUI");
            _questUI = GameObject.FindGameObjectWithTag("QuestUI");
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
