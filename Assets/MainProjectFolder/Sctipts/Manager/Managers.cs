using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : SingletonMonoBehaviour<Managers>
{
    private GameManager gameManager;
    private UIManager uiManager;
    private ItemManager itemManager;

    public static GameManager GM { get => Instance.gameManager; }
    public static UIManager UI { get => Instance.uiManager; }
    public static ItemManager Item { get => Instance.itemManager; }

    protected override void Awake()
    {
        base.Awake();
        gameManager = new GameManager();
        uiManager = new UIManager();
        itemManager = new ItemManager();
    }
}
