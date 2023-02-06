using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    [Header("Player Infomations")]
    [SerializeField] protected  GameObject player_Prefab;
    [SerializeField] public  int player_Money = 0;
    [SerializeField] protected  float player_HP;
    [SerializeField] protected  int day_Count = 0;

    [Header("Village Game Infomations")]
    [SerializeField] public int colected_Count = 5;
    [SerializeField] public  float colected_Time = 360f;
    

    [Header("Shop Game Infomations")]
    [SerializeField] public int selling_Count = 10;

    [Header("UI Object on Village")]
    [SerializeField] Text countText;
    [SerializeField] Text moneyText;
    [SerializeField] GameObject showPressKeyText;

    GameObject player;

    [SerializeField] GameObject startPosition;

    [SerializeField] bool isChangeScene;

    Vector3 player_start_Position;

    private void Start()
    {
        player_start_Position = startPosition.transform.position;
        player = Instantiate(player_Prefab);
        player.transform.position = player_start_Position;

        isChangeScene = false;
        //showPressKeyText.SetActive(false);
        countText.text = " 남은 채집 횟수 " + colected_Count.ToString();
        moneyText.text = " G : " + player_Money.ToString();
    }

    private void Update()
    {
        SetTimer();
        UpdateUI();
        InputControl();
        if (selling_Count < 0)
        {
            isChangeScene = true;
        }
        moneyText.text = " G : " + player_Money.ToString();
    }

    public void SetTimer()
    {
        if (colected_Time > 0)
        {
            colected_Time -= Time.deltaTime;
        }
        else isChangeScene = true;
    }

    public void SetColectCount()
    {
        if (colected_Count > 0)
        {
            countText.text = " 남은 채집 횟수 " + colected_Count.ToString();
        }
        else isChangeScene = false;
    }

    public void UpdateUI()
    {
        //showPressKeyText.SetActive(isChangeScene);
    }

    public void InputControl()
    {
        if (isChangeScene)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (SceneManager.GetActiveScene().name == "v02")
                {
                    SceneManager.LoadScene("TestUI");
                    DontDestroyOnLoad(this.gameObject);
                    selling_Count = 10;
                    isChangeScene = false;
                }                    
                else if (SceneManager.GetActiveScene().name == "TestUI")
                {
                    SceneManager.LoadScene("v02");
                    DontDestroyOnLoad(this.gameObject);
                    colected_Time = 20f;
                    day_Count++;
                    isChangeScene = false;
                }                  
            }
        }
    }

}
