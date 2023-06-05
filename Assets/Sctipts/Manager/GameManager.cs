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
    public float maxHP;
    public float currentHP;

    public string nowScene;


    
    

    [SerializeField] GameObject spawnPosition;
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
