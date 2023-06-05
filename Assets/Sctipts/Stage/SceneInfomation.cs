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
        if(Instance == null) Instance = this;
    }
    void Start()
    {
        nowScene = SceneManager.GetActiveScene().name;
        if(spawnPosition == null)
        {
            spawnPosition = GameObject.FindGameObjectWithTag("SpawnPos");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReSpawnPlayer()
    {
        if(spawnPosition != null)
        {
            PlayerController.Instance.transform.position = spawnPosition.transform.position;
            PlayerController.Instance.transform.forward = spawnPosition.transform.forward;
        }
    }
}
