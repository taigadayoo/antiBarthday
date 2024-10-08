using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab; // 敵キャラのプレハブ

    void Start()
    {
        SpawnEnemy();
    }

   public void SpawnEnemy()
    {
        // 敵キャラのプレハブが設定されていない場合は何もしない
        if (enemyPrefab == null)
        {

            return;
        }

        // プレハブから敵キャラを生成し、SpawnPointの位置に配置する
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
