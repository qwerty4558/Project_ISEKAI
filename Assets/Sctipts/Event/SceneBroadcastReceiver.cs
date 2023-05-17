using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneBroadcastReceiver : SerializedMonoBehaviour
{
    public Dictionary<string, UnityEvent> Broadcasts;

    private void Start()
    {
        if(SceneBroadcaster.Instance == null)
        {
            Debug.LogError("SceneBroadcaster 스크립트가 없습니다!");
            return;
        }

       string currentScene = SceneManager.GetActiveScene().name;
       var retrivedBroadcasts = SceneBroadcaster.Instance.BroadcastLists[currentScene];

        foreach (var retrived in retrivedBroadcasts)
        {
            if(!Broadcasts.ContainsKey(retrived))
            { Debug.LogWarning("BroadcastReceiver에서 ID:" + retrived + "를 찾지 못했습니다."); return; }
            Broadcasts[retrived].Invoke();
        }

        SceneBroadcaster.Instance.BroadcastLists[currentScene].Clear();
    }
}
