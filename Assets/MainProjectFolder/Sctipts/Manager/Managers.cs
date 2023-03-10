using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : SingletonMonoBehaviour<Managers>
{
    [Header("Manager Inspector")]
    //GameManager
    [SerializeField] GameManager gameManager;

    //UIManager
    [SerializeField] UIManager uiManater;

    //ItemManager
    [SerializeField] ItemManager itemManager;
    // ���� �߰��� �͵�
    //EventManager
    //PollingManager
    //AudioManager
    //ControllerManager

    public static GameManager GM { get => Instance.gameManager; }

    public static UIManager UI { get=> Instance.uiManater; }

    public static ItemManager item { get => Instance.itemManager; }

}
