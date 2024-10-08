using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dossun : MonoBehaviour
{
    public float fallSpeed = 10f; // ���~���x
    public float returnSpeed = 2f; // �߂鑬�x
    public float fallDistance = 5f; // ���~����

    private Vector2 startPosition;
    private bool falling = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (falling)
        {
            // ���~��
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
            if (transform.position.y <= startPosition.y - fallDistance)
            {
                falling = false;
            }
        }
        else
        {
            // �߂钆
            transform.Translate(Vector2.up * returnSpeed * Time.deltaTime);
            if (transform.position.y >= startPosition.y)
            {
                transform.position = startPosition;
                falling = true;
            }
        }
    }
}
