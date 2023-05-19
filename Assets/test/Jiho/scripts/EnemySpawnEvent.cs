using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnEvent : MonoBehaviour
{
    [SerializeField] private GameObject enemy_obj;
    [SerializeField] private string enemyName;

    private void Awake()
    {
        enemy_obj = transform.GetChild(0).gameObject;
        enemyName = enemy_obj.GetComponent<Enemy>().EnemyName;
    }

    private void Update()
    {
        SpawnCheck();
    }

    private void SpawnCheck()
    {
        if (!enemy_obj.activeSelf) enemy_obj.SetActive(EnemySpawner.instance.enemyDatas[enemyName].active);
    }
}
