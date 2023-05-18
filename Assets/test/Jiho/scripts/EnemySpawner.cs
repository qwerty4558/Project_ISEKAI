using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SerializedMonoBehaviour
{
    public static EnemySpawner instance;

    [System.Serializable]
    public class SpawnData
    {
        public float spawnTime;
        public bool active;
    }
    
    public Dictionary<string, SpawnData> enemyDatas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        foreach (KeyValuePair<string, SpawnData> info in enemyDatas)
        {
            SpawnData temp = info.Value;

            if (temp.spawnTime > 0 && !temp.active)
            {
                temp.spawnTime -= Time.deltaTime;
            }
            else if (temp.spawnTime <= 0 && !temp.active)
            {
                temp.active = true;
                temp.spawnTime = 15f;
            }
        }
    }

    public bool SetEnemyData(string key)
    {
        if(enemyDatas.ContainsKey(key)) return enemyDatas[key].active;
        else return false;
    }

    public void GetEnemyData(string key, bool _bool)
    {
        if (enemyDatas.ContainsKey(key)) enemyDatas[key].active = _bool;
    }
}
