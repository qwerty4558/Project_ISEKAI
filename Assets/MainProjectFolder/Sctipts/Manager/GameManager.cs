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
    [SerializeField] public  float colected_Time = 360f;
    

    [Header("Shop Game Infomations")]
    [SerializeField] public int selling_Count = 10;

    GameManager instance = null;

    [Header("Is Scene Change?")]
    [SerializeField] bool isChangeScene;

    Vector3 player_start_Position;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameManager Instance
    {
        get 
        { 
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        InitGameManager();
    }

    private void InitGameManager()
    {
        isChangeScene = false;
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
            
        }
        else isChangeScene = false;
    }

  
    public void InputControl()
    {
        if (isChangeScene)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (SceneManager.GetActiveScene().name == "Village")
                {
                    isChangeScene = false;
                    SceneManager.LoadScene("ShopScene");                    
                    DontDestroyOnLoad(gameObject);
                    selling_Count = 10;
                }                    
                else if (SceneManager.GetActiveScene().name == "ShopScene")
                {
                    isChangeScene = false;
                    SceneManager.LoadScene("Village");
                    InitGameManager();
                    colected_Time = 20f;                    
                    day_Count++;                    
                }                  
            }
        }
    }
}
