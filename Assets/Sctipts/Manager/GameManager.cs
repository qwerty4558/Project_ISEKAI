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
using Modules.EventSystem;

public class GameManager : SingletonMonoBehaviour<GameManager>
{    
    private UIManager uiManager;
    private ItemManager itemManager;
    private EventManager eventManager;
    private DialogueManager dialogueManager;
    private SoundManager soundManager;
    private QuestManager questManager;
    public static UIManager UI { get => Instance.uiManager; }
    public static ItemManager Item { get => Instance.itemManager; }
    public static EventManager Event { get => Instance.eventManager; }
    
    public static SoundManager Sound { get => Instance.soundManager; }
   

    protected override void Awake()
    {
        base.Awake();        
        uiManager = new UIManager();
        itemManager = new ItemManager();
        eventManager = new EventManager();
        
        soundManager = new SoundManager();
      
    }
}
