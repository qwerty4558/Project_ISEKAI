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
            Debug.LogError("SceneBroadcaster ��ũ��Ʈ�� �����ϴ�!");
            return;
        }

       string currentScene = SceneManager.GetActiveScene().name;
       var retrivedBroadcasts = SceneBroadcaster.Instance.BroadcastLists[currentScene];

        foreach (var retrived in retrivedBroadcasts)
        {
            if(!Broadcasts.ContainsKey(retrived))
            { Debug.LogWarning("BroadcastReceiver���� ID:" + retrived + "�� ã�� ���߽��ϴ�."); return; }
            Broadcasts[retrived].Invoke();
        }

        SceneBroadcaster.Instance.BroadcastLists[currentScene].Clear();
    }
}
