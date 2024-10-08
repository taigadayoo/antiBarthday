using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    public float bounceForce = 50f; // ��ђ��˂��

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g���v���C���[�̏ꍇ
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // �v���C���[�ɗ͂������Ĕ�ђ��˂�����
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, bounceForce);
        }
    }
}
