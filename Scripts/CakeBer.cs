using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeBer : MonoBehaviour
{
    [SerializeField] private Image _hpBarcurrent; // ���݂�HP�o�[�̃C���[�W
    [SerializeField] private Player player; // �v���C���[�̎Q��
    private float currentBullet; // �����̒e���iHP�o�[�̊�ƂȂ�l�j

    // ����������
    void Awake()
    {
        currentBullet = player.bulletNum; // �v���C���[�̒e������l�Ƃ��Đݒ�
    }

    // ���t���[���̍X�V����
    void Update()
    {
        // HP�o�[��fillAmount�����݂̒e���Ɗ�̒e���ɉ����Đݒ�
        _hpBarcurrent.fillAmount = player.bulletNum / currentBullet;
    }
}
