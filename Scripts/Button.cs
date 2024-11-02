using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // スプライトレンダラーの参照

    [SerializeField]
    private Sprite Onrever; // ボタンが押されたときに表示するスプライト

    private ReverDoor reverDoor; // ReverDoorスクリプトの参照

    // 初期化処理
    void Start()
    {
        reverDoor = FindObjectOfType<ReverDoor>(); // ReverDoorコンポーネントをシーンから取得
        spriteRenderer = GetComponent<SpriteRenderer>(); // スプライトレンダラーを取得
    }

    // 衝突時の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグが"RedCake"または"Cake"の場合
        if (collision.gameObject.tag == "RedCake" || collision.gameObject.tag == "Cake")
        {
            reverDoor.enabled = true; // ReverDoorスクリプトを有効化
            spriteRenderer.sprite = Onrever; // ボタンのスプライトをOnreverに変更
            SampleSoundManager.Instance.PlaySe(SeType.SE20); // サウンドを再生（SE20）
            SampleSoundManager.Instance.PlaySe(SeType.SE19); // サウンドを再生（SE19）
        }
    }
}
