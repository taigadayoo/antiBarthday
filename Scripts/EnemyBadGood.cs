using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBadGood : MonoBehaviour
{
    public float speed = 20f; // 弾の速度
    private Camera mainCamera; // メインカメラ
    [SerializeField]
    private SpriteRenderer spriteRenderer; // スプライトレンダラー
    private bool _isRendered = false; // オフスクリーン状態
    private bool OneBullet = false; // 一度だけ弾を発射するフラグ
    public Vector3 initialDirection; // 初期の移動方向
    private GameManager gameManager; // ゲームマネージャー
    [SerializeField]
    GameObject deathEffect; // 敵が死んだときのエフェクト

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // ゲームマネージャーを取得
        mainCamera = Camera.main; // メインカメラを取得
        spriteRenderer = GetComponent<SpriteRenderer>(); // スプライトレンダラーを取得
    }

    // Update is called once per frame
    void Update()
    {
        // 初期方向に基づいて移動
        transform.Translate(initialDirection * speed * Time.deltaTime);

        GoodVector(); // 移動方向を決定
        _isRendered = false; // レンダリング状態をリセット
        OffCamera(); // カメラ外判定
        if (_isRendered == true)
        {
            Destroy(this.gameObject); // オフスクリーンの場合は自分を削除
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトによって死亡判定
        if (collision.gameObject.tag == "Damage" ||
            collision.gameObject.tag == "Wall" ||
            collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Ground" ||
            collision.gameObject.tag == "Slorp" ||
            collision.gameObject.tag == "DamageObject" ||
            collision.gameObject.CompareTag("ItemBox"))
        {
            // 死亡エフェクトを生成
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject); // 自分を削除
        }
    }

    private void OffCamera()
    {
        // スプライトのバウンディングボックスを取得
        Bounds spriteBounds = spriteRenderer.bounds;

        // スプライトの中心をビューポート座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);
        // ビューポート外に出た場合
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true; // オフスクリーン状態を設定
        }
    }

    public void OnRight()
    {
        // 右方向に移動する設定
        initialDirection = Vector3.right;
        spriteRenderer.flipX = true; // スプライトを反転
    }

    public void OnLeft()
    {
        // 左方向に移動する設定
        initialDirection = Vector3.left;
        spriteRenderer.flipX = false; // スプライトを反転
    }

    private void GoodVector()
    {
        // 一度だけ移動方向を設定
        if (OneBullet == false)
        {
            if (gameManager.OnLeft == false)
            {
                OnRight(); // 右方向を選択
                OneBullet = true; // フラグを立てる
            }
            if (gameManager.OnLeft == true)
            {
                OnLeft(); // 左方向を選択
                OneBullet = true; // フラグを立てる
            }
        }
    }
}
