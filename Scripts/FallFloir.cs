using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    // Rigidbody2D�R���|�[�l���g�̎Q��
    Rigidbody2D rb;

    // Start�͍ŏ��̃t���[���ň�x�����Ăяo�����
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D�R���|�[�l���g���擾
    }

    // Update�͖��t���[���Ăяo�����
    void Update()
    {
        // ���݂͉����������Ȃ�
    }

    // ���̃I�u�W�F�N�g�ƏՓ˂����Ƃ��ɌĂяo�����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O��"Body"�A"YellowBody"�A�܂���"Player"�̏ꍇ
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody" || collision.gameObject.tag == "Player")
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE22); // ���ʉ����Đ�
            rb.bodyType = RigidbodyType2D.Dynamic; // Rigidbody�̃^�C�v�𓮓I�ɕύX
        }
    }
}
