using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.TypeSearch;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBroadcaster : SingletonMonoBehaviour<SceneBroadcaster>
{
    public Dictionary<string, List<string>> BroadcastLists;

    protected override void Awake()
    {
        base.Awake();
        BroadcastLists = new Dictionary<string, List<string>>();
    }

    public static void AddBroadcast(string pair)
    {
        string targetScene = pair.Split(',', System.StringSplitOptions.RemoveEmptyEntries)[0];
        string broadcastID = pair.Split(',', System.StringSplitOptions.RemoveEmptyEntries)[1];

        if (SceneManager.GetSceneByName(targetScene).IsValid()==false)
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
