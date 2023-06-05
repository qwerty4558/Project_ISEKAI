using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfomation : MonoBehaviour
{
    public static SceneInfomation Instance;
    [SerializeField] string nowScene;
    [SerializeField] GameObject spawnPosition;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadedScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadedScene;
    }

    private void OnLoadedScene(Scene scnee, LoadSceneMode loadSceneMode)
    {
        WriteSceneInfomation();
    }

    void Start()
    {
        WriteSceneInfomation();
    }

    private void WriteSceneInfomation()
    {
        nowScene = SceneManager.GetActiveScene().name;
        if (spawnPosition == null)
        {
            spawnPosition = GameObject.FindGameObjectWithTag("SpawnPos");
        }
    }

    public void ReSpawnPlayer()
    {
        if(spawnPosition != null)
        {
            PlayerController.Instance.transform.position = spawnPosition.transform.position;
            PlayerController.Instance.transform.forward = spawnPosition.transform.forward;
        }
        else
        {
            Debug.LogError("리스폰 위치가 현재 씬에 없습니다. 오브젝트를 생성하거나 태그를 RespawnPos로 지정해 주세요");
        }
    }
}
