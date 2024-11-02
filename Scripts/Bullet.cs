using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player player; // プレイヤーへの参照
    public enum Charactor
    {
        Player, // プレイヤー用の弾
        Enemy   // 敵用の弾
    }
    public float MoveSpeed = 20.0f; // 弾の移動速度

    private Camera mainCamera; // メインカメラへの参照
    private Charactor charactor; // キャラクターの種類（プレイヤーか敵か）
    private bool RightLeft = true; // 弾が右に飛ぶか左に飛ぶかのフラグ
    private SpriteRenderer spriteRenderer; // スプライトレンダラーの参照
    private bool _isRendered = false; // 画面内に描画されているかどうかのフラグ

    private Vector3 initialDirection; // 弾の初期方向

    // 初期化処理
    void Start()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE11); // 弾発射音を再生
        mainCamera = Camera.main; // メインカメラを取得
        spriteRenderer = GetComponent<SpriteRenderer>(); // スプライトレンダラーを取得
        player = GameObject.Find("Player").GetComponent<Player>(); // プレイヤーコンポーネントを取得
        initialDirection = Vector3.right; // 初期の移動方向を右に設定
    }

    // 毎フレーム更新処理
    void Update()
    {
        BulletAttack(); // 弾の攻撃処理
        _isRendered = false; // 描画フラグをリセット
        OffCamera(); // カメラ外に出たかチェック
    }

    // 弾の攻撃処理
    private void BulletAttack()
    {
        if (player != null)
        {
            // プレイヤーの向きに応じて弾の向きを設定
            if (player.spriteRenderer.flipX == true && RightLeft)
            {
                initialDirection = Vector3.left; // プレイヤーが左向きなら弾も左へ
                RightLeft = false;
            }
            else if (player.spriteRenderer.flipX == false && RightLeft)
            {
                initialDirection = Vector3.right; // プレイヤーが右向きなら弾も右へ
                RightLeft = false;
            }

            // 弾を設定した方向に移動
            transform.Translate(initialDirection * MoveSpeed * Time.deltaTime);

            // 画面外に出たら弾を破壊
            if (_isRendered == true)
            {
                Destroy(gameObject);
            }
        }
    }

    // 弾が画面外に出たときに呼ばれる
    private void OnBecameInvisible()
    {
        _isRendered = true; // 描画フラグをオンにする
    }

    // 弾がカメラ外に出たかをチェック
    private void OffCamera()
    {
        Bounds spriteBounds = spriteRenderer.bounds; // スプライトの境界を取得

        // ビューポート座標に変換して範囲外かどうか確認
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true; // 範囲外なら描画フラグをオン
        }
    }

    // 他のオブジェクトとの衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 特定のタグを持つオブジェクトに衝突したら弾を破壊
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "DamageObject" || collision.gameObject.CompareTag("Slorp") || collision.gameObject.CompareTag("ItemBox"))
        {
            Destroy(this.gameObject);
        }
    }
}
