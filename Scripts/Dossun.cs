using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dossun : MonoBehaviour
{
    public float fallSpeed = 10f; // â∫ç~ë¨ìx
    public float returnSpeed = 2f; // ñﬂÇÈë¨ìx
    public float fallDistance = 5f; // â∫ç~ãóó£

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
            // â∫ç~íÜ
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
            if (transform.position.y <= startPosition.y - fallDistance)
            {
                falling = false;
            }
        }
        else
        {
            // ñﬂÇÈíÜ
            transform.Translate(Vector2.up * returnSpeed * Time.deltaTime);
            if (transform.position.y >= startPosition.y)
            {
                transform.position = startPosition;
                falling = true;
            }
        }
    }
}
