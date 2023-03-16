using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;
using Cinemachine.Editor;
using Cinemachine;

public class GameManager : Managers
{
    [Header("Player Infomations")]
    [SerializeField] public int player_Money = 0;
    [SerializeField] protected float player_HP;


    [Header("Village Game Infomations")]
    [SerializeField] public int colected_Count = 5;
    [SerializeField] public float colected_Time;

    [Header("Shop Game Infomations")]
    [SerializeField] public int selling_Count = 10;

    [Header("Is Scene Change?")]
    [SerializeField] bool isChangeScene;
    [SerializeField] string nowScene;


    private void Start()
    {
        InitGameManager();
        Debug.Log("Start");
    }

    private void InitGameManager()
    {
        isChangeScene = false;
        nowScene = SceneManager.GetActiveScene().name;
    } 
}
