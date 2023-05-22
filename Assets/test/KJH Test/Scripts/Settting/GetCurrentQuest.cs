using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetCurrentQuest : MonoBehaviour
{
    [SerializeField] GameObject questTitle;
    [SerializeField] QuestTitle questStorage;
    // Start is called before the first frame update

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
        if(questTitle == null)
        {
            questTitle = GameObject.FindWithTag("Quest");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
