using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : SingletonMonoBehaviour<Managers>
{
    [Header("Manager Inspector")]
    //GameManager
    [SerializeField] GameManager gameManager;
    //UIManager
<<<<<<< Updated upstream
    [SerializeField] UIManager uiManater;

=======
    [SerializeField] UIManager uiManager;
>>>>>>> Stashed changes
    //ItemManager
    [SerializeField] ItemManager itemManager;
    // 차후 추가될 것들
    //EventManager
    //PollingManager
    //AudioManager
    //ControllerManager
    //DialogueManager

    public static GameManager GM { get => Instance.gameManager; }
<<<<<<< Updated upstream

    public static UIManager UI { get=> Instance.uiManater; }

    public static ItemManager item { get => Instance.itemManager; }

=======
    public static UIManager UI { get => Instance.uiManager; }
    public static ItemManager item { get => Instance.itemManager; }

    private void Start()
    {

    }
>>>>>>> Stashed changes
}
