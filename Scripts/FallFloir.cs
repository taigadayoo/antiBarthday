using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    // Rigidbody2Dコンポーネントの参照
    Rigidbody2D rb;

    // Startは最初のフレームで一度だけ呼び出される
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2Dコンポーネントを取得
    }

    // Updateは毎フレーム呼び出される
    void Update()
    {
        // 現在は何も処理しない
    }

    // 他のオブジェクトと衝突したときに呼び出される
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグが"Body"、"YellowBody"、または"Player"の場合
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody" || collision.gameObject.tag == "Player")
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE22); // 効果音を再生
            rb.bodyType = RigidbodyType2D.Dynamic; // Rigidbodyのタイプを動的に変更
        }
    }
}
