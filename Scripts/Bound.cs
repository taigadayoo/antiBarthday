using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    public float bounceForce = 50f; // 飛び跳ねる力

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトがプレイヤーの場合
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // プレイヤーに力を加えて飛び跳ねさせる
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, bounceForce);
        }
    }
}
