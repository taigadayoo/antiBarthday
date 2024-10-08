using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverDoor : MonoBehaviour
{
    public float targetY = -5f; // �ړ����Y���W
    public float moveSpeed = 2.0f; // �ړ����x

    private Rigidbody2D[] childRigidbodies;
    void Start()
    {
        
        childRigidbodies = GetComponentsInChildren<Rigidbody2D>();
        this.enabled = false;
        
    }

    private void FixedUpdate()
    {
        // �e�q�I�u�W�F�N�g��Rigidbody2D�ɑ��x��ݒ肵�Ĉړ�
        foreach (Rigidbody2D rb in childRigidbodies)
        {
            // �ړ���̈ʒu��ݒ�
            Vector2 targetPosition = new Vector2(rb.transform.position.x, targetY);

            // ���݂̈ʒu����ړ���̈ʒu�ւ̃x�N�g�����v�Z
            Vector2 moveDirection = targetPosition - rb.position;

            // �x�N�g���̒��������x�𒴂��Ȃ��悤�ɐ���
            moveDirection = Vector2.ClampMagnitude(moveDirection, moveSpeed);

            // Rigidbody2D�ɑ��x��ݒ�
            rb.velocity = moveDirection;
        }
    }
}
