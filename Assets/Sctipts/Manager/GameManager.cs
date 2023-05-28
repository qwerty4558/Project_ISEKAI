using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;
using Cinemachine;
using Modules.EventSystem;

[System.Serializable]
public class GameManager
{    
    private readonly UIManager uiManager;
    private readonly ItemManager itemManager;
    private readonly EventManager eventManager;
    private readonly SoundManager soundManager;

    public static GameManager instance;

    public GameManager(UIManager _uiManager, ItemManager _itemManager, EventManager _evenetManager, SoundManager _soundManager)
    {
        this.uiManager = _uiManager;
        this.itemManager = _itemManager;
        this.eventManager = _evenetManager;
        this.soundManager = _soundManager;
    }

    

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

}
