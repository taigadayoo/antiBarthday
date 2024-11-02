using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // �X�v���C�g�����_���[�̎Q��

    [SerializeField]
    private Sprite Onrever; // �{�^���������ꂽ�Ƃ��ɕ\������X�v���C�g

    private ReverDoor reverDoor; // ReverDoor�X�N���v�g�̎Q��

    // ����������
    void Start()
    {
        reverDoor = FindObjectOfType<ReverDoor>(); // ReverDoor�R���|�[�l���g���V�[������擾
        spriteRenderer = GetComponent<SpriteRenderer>(); // �X�v���C�g�����_���[���擾
    }

    // �Փˎ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O��"RedCake"�܂���"Cake"�̏ꍇ
        if (collision.gameObject.tag == "RedCake" || collision.gameObject.tag == "Cake")
        {
            reverDoor.enabled = true; // ReverDoor�X�N���v�g��L����
            spriteRenderer.sprite = Onrever; // �{�^���̃X�v���C�g��Onrever�ɕύX
            SampleSoundManager.Instance.PlaySe(SeType.SE20); // �T�E���h���Đ��iSE20�j
            SampleSoundManager.Instance.PlaySe(SeType.SE19); // �T�E���h���Đ��iSE19�j
        }
    }
}
