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
            Debug.LogError(targetScene + "���� Ȯ�ε��� �ʾҽ��ϴ�. �� �̸��� ������ �Ǿ��ִ���, Scene In Build�� ���ԵǾ��ִ��� Ȯ�����ּ���.");
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
    }
}
