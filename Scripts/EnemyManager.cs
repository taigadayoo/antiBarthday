using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyPosition> enemyPositions = new List<EnemyPosition>();

  

    // 特定の条件が満たされたときにリセット関数を呼び出す関数
    public void CheckResetCondition(bool condition)
    {
        if (condition )
        {
         
            ResetEnemyPositions(); // リセット
        }
    }

    // すべての敵の位置を初期位置にリセットする関数
    private void ResetEnemyPositions()
    {
        foreach (var enemyPos in enemyPositions)
        {
            if (enemyPos.enemy != null)
            {
                enemyPos.enemy.transform.position = enemyPos.initialPosition;
            }
        }
    }

    // 敵を追加する関数
    public void AddEnemy(GameObject enemy)
    {
        // 初期位置を現在の位置に設定する
        Vector3 initialPosition = enemy.transform.position;
        EnemyPosition enemyPos = new EnemyPosition();
        enemyPos.enemy = enemy;
        enemyPos.initialPosition = initialPosition;
        enemyPositions.Add(enemyPos);
    }
}

[System.Serializable]
public class EnemyPosition
{
    public GameObject enemy;
    public Vector3 initialPosition;
}