using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemySpawnPoint> spawnPoints = new List<EnemySpawnPoint>(); // SpawnPoint �̃��X�g

    void Start()
    {
       
    }

    public void RespawnAll()
    {
        // �e SpawnPoint �ɑ΂��� SpawnEnemy ���\�b�h���Ăяo��
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemy();
        }
    }
}
