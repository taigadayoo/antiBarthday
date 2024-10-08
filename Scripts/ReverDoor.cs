using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverDoor : MonoBehaviour
{
    public float targetY = -5f; // 移動先のY座標
    public float moveSpeed = 2.0f; // 移動速度

    private Rigidbody2D[] childRigidbodies;
    void Start()
    {
        
        childRigidbodies = GetComponentsInChildren<Rigidbody2D>();
        this.enabled = false;
        
    }

    private void FixedUpdate()
    {
        // 各子オブジェクトのRigidbody2Dに速度を設定して移動
        foreach (Rigidbody2D rb in childRigidbodies)
        {
            // 移動先の位置を設定
            Vector2 targetPosition = new Vector2(rb.transform.position.x, targetY);

            // 現在の位置から移動先の位置へのベクトルを計算
            Vector2 moveDirection = targetPosition - rb.position;

            // ベクトルの長さが速度を超えないように制限
            moveDirection = Vector2.ClampMagnitude(moveDirection, moveSpeed);

            // Rigidbody2Dに速度を設定
            rb.velocity = moveDirection;
        }
    }
}
