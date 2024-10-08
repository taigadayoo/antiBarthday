using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyPosition> enemyPositions = new List<EnemyPosition>();

  

    // ����̏������������ꂽ�Ƃ��Ƀ��Z�b�g�֐����Ăяo���֐�
    public void CheckResetCondition(bool condition)
    {
        if (condition )
        {
         
            ResetEnemyPositions(); // ���Z�b�g
        }
    }

    // ���ׂĂ̓G�̈ʒu�������ʒu�Ƀ��Z�b�g����֐�
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

    // �G��ǉ�����֐�
    public void AddEnemy(GameObject enemy)
    {
        // �����ʒu�����݂̈ʒu�ɐݒ肷��
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