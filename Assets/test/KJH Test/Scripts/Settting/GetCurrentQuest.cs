using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetCurrentQuest : MonoBehaviour
{
    [SerializeField] private GameObject questTitle;
    [SerializeField] private QuestTitle questStorage;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (questTitle == null)
        {
            questTitle = GameObject.FindWithTag("Quest");
            questStorage = questTitle.GetComponent<QuestTitle>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
