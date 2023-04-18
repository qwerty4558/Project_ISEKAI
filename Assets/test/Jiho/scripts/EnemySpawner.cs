using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private int initEnemyCount;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private Camera cam;
    [SerializeField] private List<Queue<GameObject>> enemyQueueList;


    private void Awake()
    {
        enemyQueueList = new List<Queue<GameObject>>();
        InitList();
    }

    private void Update() //임시 업데이트문(몬스터 생성 테스트용) 나중에 삭제 바람
    {
        if(Input.GetKeyDown(KeyCode.O)) //슬라임 생성
        {
            SpawnEnemy(0);
        }
        
        if(Input.GetKeyDown(KeyCode.P)) //고블린 생성
        {
            SpawnEnemy(1);
        }
    }

    private void InitList()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
            enemyQueueList.Add(InitQueue(enemyPrefabs[i]));
            
    }

    private Queue<GameObject> InitQueue(GameObject prefab)
    {
        Queue<GameObject> temp = new Queue<GameObject>();
        for(int i = 0; i < initEnemyCount; i++)
        {
            GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity);
            enemy.transform.SetParent(this.transform);
            enemy.SetActive(false);
            temp.Enqueue(enemy);
        }
        return temp;
    }

    private void SpawnEnemy(int list)
    {
        if (enemyQueueList[list].Count > 0)
        {
            int a = Random.Range(0, 4);
            GameObject temp = enemyQueueList[list].Dequeue();
            Enemy enemy = temp.GetComponentInChildren<Enemy>();
            enemy.cam = cam;
            enemy.canvas.worldCamera = cam;
            enemy.Player = player;
            enemy.StartPos = spawnPos[a].position;
            temp.transform.position = spawnPos[a].position;
            temp.SetActive(true);
        }
    }

}

