using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject prefab;
        public float spawnTime;
    }
    public SpawnData[] enemyDatas = null;


    private void Update()
    {
        for(int i = 0; i < enemyDatas.Length; i++)
        {
            if (!enemyDatas[i].prefab.activeSelf)
            {
                if (enemyDatas[i].spawnTime > 0)
                {
                    enemyDatas[i].spawnTime -= Time.deltaTime;
                }
                else
                {
                    enemyDatas[i].spawnTime = 15f;
                    enemyDatas[i].prefab.SetActive(true);
                }
            }
        }
    }

}
