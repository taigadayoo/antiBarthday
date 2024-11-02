using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    // プレイヤーオブジェクトの参照
    private Player player;

    // キャラクターの種類を定義する列挙型
    public enum Charactor
    {
        Player, // プレイヤー
        Enemy   // 敵
    }

    // 弾の移動速度
    public float MoveSpeed = 20.0f;

    // メインカメラの参照
    private Camera mainCamera;

    // キャラクターの種類
    private Charactor charactor;

    // 左右の方向を管理するフラグ
    private bool RightLeft = true;

    // スプライトレンダラーの参照
    private SpriteRenderer spriteRenderer;

    // 弾がカメラ外に出たかどうかのフラグ
    private bool _isRendered = false;

    // 弾の初期方向
    private Vector3 initialDirection;

    void Start()
    {
        // メインカメラの取得
        mainCamera = Camera.main;

        // スプライトレンダラーの取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // プレイヤーオブジェクトの取得
        player = GameObject.Find("Player").GetComponent<Player>();

        // 初期方向を右に設定
        initialDirection = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        BulletAttack(); // 弾の攻撃処理
        _isRendered = false; // 表示状態をリセット
        OffCamera(); // カメラ外のチェック
    }

    private void BulletAttack()
    {
        if (player != null) // プレイヤーが存在する場合
        {
            // プレイヤーが左を向いている場合
            if (player.spriteRenderer.flipX == true && RightLeft)
            {
                initialDirection = Vector3.left; // 初期方向を左に設定
                RightLeft = false; // フラグを更新
            }
            // プレイヤーが右を向いている場合
            else if (player.spriteRenderer.flipX == false && RightLeft)
            {
                initialDirection = Vector3.right; // 初期方向を右に設定
                RightLeft = false; // フラグを更新
            }

            // 弾の移動処理
            transform.Translate(initialDirection * MoveSpeed * Time.deltaTime);

            // 表示状態がtrueの場合、弾を破棄
            if (_isRendered == true)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        // 弾が画面外に出た場合
        _isRendered = true;
    }

    private void OffCamera()
    {
        // スプライトのバウンズを取得
        Bounds spriteBounds = spriteRenderer.bounds;

        // ワールド座標をビューポート座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);

        // ビューポートの外に出ている場合
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true; // 表示状態を更新
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグを確認
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground" ||
            collision.gameObject.tag == "DamageObject" || collision.gameObject.CompareTag("Slorp"))
        {
            // 壁や地面、ダメージオブジェクト、またはSlorpタグのオブジェクトと衝突した場合、弾を破棄
            Destroy(this.gameObject);
        }
    }
}
