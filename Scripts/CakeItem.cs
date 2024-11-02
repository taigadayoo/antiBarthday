using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeItem : MonoBehaviour
{
    private Player player; // �v���C���[�̎Q��

    public float floatStrength = 400f; // �A�C�e���̕����̋��x
    public float floatSpeed = 1f; // �A�C�e���̕����̑��x
    [SerializeField]
    GameObject itemEffect; // �A�C�e���̌��ʃG�t�F�N�g

    GameManager gameManager; // �Q�[���}�l�[�W���[�̎Q��

    private Vector3 startPosition; // �A�C�e���̏����ʒu

    // ����������
    void Start()
    {
        startPosition = transform.position; // ���݂̈ʒu�������ʒu�Ƃ��ċL�^
        GameObject playerObject = GameObject.Find("Player"); // "Player"�I�u�W�F�N�g��T��
        gameManager = FindObjectOfType<GameManager>(); // GameManager���������Ď擾
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>(); // Player�X�N���v�g���擾
            if (player == null)
            {
                Debug.LogError("Player�I�u�W�F�N�g��PlayerScript���A�^�b�`����Ă��܂���I"); // Player�X�N���v�g��������Ȃ��ꍇ�̃G���[���O
            }
        }
        else
        {
            Debug.LogError("Player�I�u�W�F�N�g��������܂���ł����I"); // Player�I�u�W�F�N�g��������Ȃ��ꍇ�̃G���[���O
        }
    }

    // ���t���[���̍X�V����
    void Update()
    {
        Enemyvanish(); // �G�̏��Ŕ���
        // Sin�g�ŃA�C�e���𕂓�������
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatStrength, 0);
    }

    // �Փ˔���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Body�܂���YellowBody�^�O�̃I�u�W�F�N�g�ƏՓ˂����ۂ̏���
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody")
        {
            Instantiate(itemEffect, this.transform.position, this.transform.rotation); // �A�C�e���̃G�t�F�N�g�𐶐�
        }
    }

    // �S�Ă̓G���|���ꂽ�ۂ̃A�C�e�����ŏ���
    private void Enemyvanish()
    {
        if (gameManager.EnemyAllDead == true)
        {
            Destroy(this.gameObject); // �S�Ă̓G���|����Ă���΃A�C�e�����폜
        }
    }
}
