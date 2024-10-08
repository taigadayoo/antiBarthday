using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemySpawnPoint> spawnPoints = new List<EnemySpawnPoint>(); // SpawnPoint のリスト

    void Start()
    {
       
    }

    public void RespawnAll()
    {
        // 各 SpawnPoint に対して SpawnEnemy メソッドを呼び出す
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy();
        }
    }
}
