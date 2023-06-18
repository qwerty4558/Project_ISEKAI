using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.TypeSearch;
#endif
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBroadcaster : SerializedMonoBehaviour
{
    private static SceneBroadcaster instance;
    public static SceneBroadcaster Instance { get { return instance; } }

    [ReadOnly]
    public Dictionary<string, List<string>> BroadcastLists;

    public static string currentTargetScene;
    public static string currentBroadcastID;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Instance.BroadcastLists = new Dictionary<string, List<string>>();
        }
    }

    public static void AddBroadcast(string pair)
    {
        string targetScene = pair.Split(',', System.StringSplitOptions.RemoveEmptyEntries)[0];
        string broadcastID = pair.Split(',', System.StringSplitOptions.RemoveEmptyEntries)[1];

        if(targetScene == SceneManager.GetActiveScene().name)
        {
            SceneBroadcastReceiver[] receivers = FindObjectsOfType<SceneBroadcastReceiver>();

            foreach(SceneBroadcastReceiver receiver in receivers)
            {
                if(receiver.Broadcasts.ContainsKey(broadcastID))
                {
                    receiver.Broadcasts[broadcastID].Invoke();
                    return;
                }
            }
        }

        if (SceneManager.GetSceneByName(targetScene) == null)
        {
            Debug.LogError(targetScene + "씬이 확인되지 않았습니다. 씬 이름이 제대로 되어있는지, Scene In Build에 포함되어있는지 확인해주세요.");
            return;
        }

        if(Instance.BroadcastLists.ContainsKey(targetScene))
        {
            Instance.BroadcastLists[targetScene].Add(broadcastID);
        }
        else
        {
            Instance.BroadcastLists.Add(targetScene, new List<string>());
            Instance.BroadcastLists[targetScene].Add(broadcastID);
        }

        currentTargetScene = targetScene;
        currentBroadcastID = broadcastID;
    }

    public static void RemoveBroadcast()
    {
        Scene targetSceneObj = SceneManager.GetSceneByName(currentTargetScene);
        if (!targetSceneObj.IsValid())
        {
            Debug.LogError(currentTargetScene + "씬이 확인되지 않았습니다. 씬 이름이 제대로 되어있는지, Scene In Build에 포함되어있는지 확인해주세요.");
            return;
        }

        if(Instance.BroadcastLists.ContainsKey(currentTargetScene) && Instance.BroadcastLists[currentTargetScene].Contains(currentBroadcastID))
        {
            Instance.BroadcastLists[currentTargetScene].Remove(currentBroadcastID);
            Debug.Log("Broadcast with ID " + currentBroadcastID + " has been canceled in the scene " + currentTargetScene);
        }
        else
        {
            Debug.Log("Broadcast with ID " + currentBroadcastID + " is not registered in the scene " + currentTargetScene);

        }
    }

    public static void RemoveAllBroadcast() 
    {
        Instance.BroadcastLists.Clear();
    }
}
