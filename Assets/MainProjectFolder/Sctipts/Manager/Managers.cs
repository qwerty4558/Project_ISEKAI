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

    // 차후 추가될 것들
    //EventManager
    //PollingManager
    //AudioManager
    //ControllerManager

}
