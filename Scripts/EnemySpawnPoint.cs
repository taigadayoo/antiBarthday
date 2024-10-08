using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab; // �G�L�����̃v���n�u

    void Start()
    {
        SpawnEnemy();
    }

   public void SpawnEnemy()
    {
        // �G�L�����̃v���n�u���ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (enemyPrefab == null)
        {

            return;
        }

        // �v���n�u����G�L�����𐶐����ASpawnPoint�̈ʒu�ɔz�u����
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
