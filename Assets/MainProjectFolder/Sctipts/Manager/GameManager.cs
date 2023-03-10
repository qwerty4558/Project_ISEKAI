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

public class GameManager : MonoBehaviour
{
    [Header("Player Infomations")]    
    [SerializeField] public  int player_Money = 0;
    [SerializeField] protected  float player_HP;
    [SerializeField] protected  int day_Count = 0;

    [Header("Village Game Infomations")]
    [SerializeField] public int colected_Count = 5;
    [SerializeField] public float colected_Time;

    [Header("Shop Game Infomations")]
    [SerializeField] public int selling_Count = 10;

    [Header("Is Scene Change?")]
    [SerializeField] bool isChangeScene;
    [SerializeField] string nowScene;

    private void Awake()
    {
        
    }

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

    private void Update()
    {
        SetMoveToOtherScene();
        
        InputControl();
        
    }
    public void SetMoveToOtherScene()
    {
        if (colected_Time > 0)
        {
            colected_Time -= Time.deltaTime;
        }
        else isChangeScene = true;
        if (colected_Count == 0)
        {
            isChangeScene = true;
        }
    }

    public void SetColectCount()
    {
        if (colected_Count > 0)
        {
            isChangeScene = true;
        }
        else isChangeScene = false;
    }

  
    public void InputControl()
    {
        if (isChangeScene)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (SceneManager.GetActiveScene().name == "L_Main")
                {
                    isChangeScene = false;                    
                    LoadingSceneController.Instance.LoadScene("Shop");                    
                    selling_Count = 10;
                }                    
                else if (SceneManager.GetActiveScene().name == "Shop")
                {
                    isChangeScene = false;                    
                    LoadingSceneController.Instance.LoadScene("L_Main");                    
                    colected_Time = 20f;                    
                    day_Count++;                    
                }                  
            }
        }
    }
}
