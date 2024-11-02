using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyLife = 1; // 敵のライフ

    public float amplitude = 1f; // 縦方向の動きの振幅
    public float moveSpeed = 20f; // 移動速度
    public float moveSpeedVertical = 15f; // 縦移動の速度
    public float maxY = 5f; // 上方向の移動範囲
    public float minY = -5f; // 下方向の移動範囲
    public float jumpForce = 5f; // ジャンプの力
    public float minJumpInterval = 1f; // 最小ジャンプ間隔
    public float maxJumpInterval = 3f; // 最大ジャンプ間隔
    public float changeDirectionInterval = 3f; // 方向転換の間隔

    [SerializeField]
    GameObject deathEffect; // 敵が死亡した際のエフェクト
    [SerializeField]
    GameObject Ban1; // バン1（スプライト等）
    [SerializeField]
    GameObject Ban2; // バン2（スプライト等）

    [SerializeField]
    public GameObject bulletPrefab; // 発射する弾のプレハブ
    public Transform firePoint; // 弾の発射位置

    private float nextFireTime; // 次に弾を発射する時間
    private float minFireInterval = 1f; // 最小発射間隔
    private float maxFireInterval = 3f; // 最大発射間隔

    private GameObject bulletInstance; // 発射された弾のインスタンス

    [SerializeField]
    public SpriteRenderer spriteRenderer; // スプライトレンダラー
    [SerializeField]
    EnemyCol enemyCol; // 敵のコリジョン管理
    private Rigidbody2D rb; // リジッドボディ

    private Camera mainCamera; // メインカメラの参照

    private Vector2 initialPosition; // 初期位置

    public bool _isRendered = false; // 描画されているかのフラグ
    public bool _isRenderedThrow = false; // 投げる敵が描画されているかのフラグ
    public bool _isRenderedBird = false; // 鳥型敵が描画されているかのフラグ
    private bool movingRight = true; // 右方向に移動しているかのフラグ
    private float timeSinceLastDirectionChange = 0f; // 最後に方向を変えてからの時間
    GameManager gameManager; // ゲームマネージャーの参照

    FollowCamera followCamera; // カメラ追尾用のスクリプト参照
    EnemyVoice enemyVoice; // 敵の音声管理

    RandomEnemyVoice randomEnemyVoice; // ランダムな敵の音声を管理

    [SerializeField]
    EnemyVoiceMob voiceMob; // 敵ボイスのモブ管理

    [SerializeField]
    anitiVoice AntiVoice; // アンチボイス管理

    public enum EnemyType
    {
        nomal, // 通常の敵
        side, // 横移動する敵
        vertical, // 縦移動する敵
        Throw // 弾を投げる敵
    }
    [SerializeField] EnemyType enemyType; // 敵のタイプ

    void Start()
    {
        followCamera = FindObjectOfType<FollowCamera>(); // FollowCameraコンポーネントを探して取得
        randomEnemyVoice = GetComponent<RandomEnemyVoice>(); // 自身のRandomEnemyVoiceコンポーネントを取得
        enemyVoice = FindObjectOfType<EnemyVoice>(); // シーン内のEnemyVoiceコンポーネントを探して取得
        initialPosition = transform.position; // 敵の初期位置を保存
        mainCamera = Camera.main; // メインカメラを取得
        spriteRenderer = GetComponent<SpriteRenderer>(); // 自身のSpriteRendererコンポーネントを取得
        gameManager = FindObjectOfType<GameManager>(); // シーン内のGameManagerコンポーネントを探して取得
        rb = GetComponent<Rigidbody2D>(); // 自身のRigidbody2Dコンポーネントを取得
        if (enemyType == EnemyType.side)
        {
            StartCoroutine(RandomJump()); // 敵のタイプがサイドの場合、ランダムにジャンプするコルーチンを開始
        }
        if (enemyType == EnemyType.Throw)
        {
            nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval); // 弾を発射するタイミングを設定
        }
    }

    // Updateは毎フレーム呼び出される
    void Update()
    {
        EnemyDead(); // 敵の死亡処理
        SideMove(); // 横移動処理
        VerticalMove(); // 縦移動処理
        Enemyvanish(); // 敵が画面外に出た場合の処理
        ThrowEnemy(); // 弾を投げる処理
        _isRendered = false; // 描画フラグをリセット
        _isRenderedBird = false; // 鳥型敵の描画フラグをリセット
        _isRenderedThrow = false; // 投げる敵の描画フラグをリセット
        OffCamera(); // カメラ外の敵の処理
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトがケーキ系のタグの場合、敵のライフを減らす
        if (collision.gameObject.tag == "Cake" || collision.gameObject.tag == "RedCake")
        {
            EnemyLife -= 1; // 敵のライフを1減少
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // トリガーに入ったオブジェクトが「Dead」タグの場合、死亡処理を行う
        if (collision.gameObject.tag == "Dead")
        {
            Instantiate(deathEffect, this.transform.position, this.transform.rotation); // 死亡エフェクトを生成
            Destroy(this.gameObject); // 敵オブジェクトを削除
        }
    }

    private void EnemyDead()
    {
        // 敵のライフが0以下になった場合の処理
        if (EnemyLife <= 0)
        {
            // 敵のタイプが縦の場合、音声を停止して死亡音を再生
            if (enemyType == EnemyType.vertical && enemyVoice != null)
            {
                enemyVoice.audioSource.Stop(); // 音声を停止
                enemyVoice.BirdDeath(); // 死亡音を再生
            }

            // 敵のタイプが横の場合、音声を停止してランダムなサウンドを再生
            if (enemyType == EnemyType.side)
            {
                voiceMob.audioSourceMattyo.Stop(); // 音声を停止
                randomEnemyVoice.PlayRandomSoundExternal(); // ランダムなサウンドを再生
            }

            // 敵のタイプが投げる場合、音声を停止してSEを再生
            if (enemyType == EnemyType.Throw)
            {
                AntiVoice.audioSourceAnti.Stop(); // 音声を停止
                SampleSoundManager.Instance.PlaySe(SeType.SE15); // SEを再生
            }

            // 死亡エフェクトを生成し、敵オブジェクトを削除
            Instantiate(deathEffect, this.transform.position, this.transform.rotation); // 死亡エフェクトを生成
            Destroy(this.gameObject); // 敵オブジェクトを削除
        }
    }

    private void ThrowEnemy()
    {
        // 敵のタイプが投げる場合の処理
        if (enemyType == EnemyType.Throw)
        {
            // 敵が描画されていて、カメラに映っている場合
            if (this._isRenderedThrow == true && followCamera.OnCamera == true)
            {
                AntiVoice.EnemyAntiVoiceOn(); // 敵の音声を再生
            }

            // 敵の移動方向が設定されている場合
            if (enemyCol != null)
            {
                // 右に移動する場合
                if (enemyCol.moveRight)
                {
                    transform.Translate(Vector2.right * moveSpeed * 3 * Time.deltaTime); // 右に移動
                    spriteRenderer.flipX = true; // スプライトを反転
                }
                // 左に移動する場合
                else
                {
                    transform.Translate(Vector2.left * moveSpeed * 3 * Time.deltaTime); // 左に移動
                    spriteRenderer.flipX = false; // スプライトを反転
                }
            }

            // 敵が描画されている場合
            if (_isRenderedThrow == true)
            {
                // 次の発射タイミングが来た場合
                if (Time.time >= nextFireTime)
                {
                    Shoot(); // 弾を発射
                    nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval); // 次の発射タイミングを設定
                }
            }
        }
    }

    private void SideMove()
    {
        // 敵が描画されていて、音声コンポーネントが存在し、カメラに映っている場合
        if (this._isRendered == true && enemyVoice != null && followCamera.OnCamera == true)
        {
            voiceMob.EnemyNomalVoiceOn(); // 敵の通常音声を再生
        }

        // 敵のタイプが横で、描画されている場合
        if (enemyType == EnemyType.side && _isRendered)
        {
            // 敵の移動方向が設定されている場合
            if (enemyCol != null)
            {
                // 右に移動する場合
                if (enemyCol.moveRight)
                {
                    transform.Translate(Vector2.right * moveSpeed * 2 * Time.deltaTime); // 右に移動
                    spriteRenderer.flipX = true; // スプライトを反転
                }
                // 左に移動する場合
                else
                {
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // 左に移動
                    spriteRenderer.flipX = false; // スプライトを反転
                }
            }
        }
    }

    private void VerticalMove()
    {
        // 敵のタイプが縦の場合
        if (enemyType == EnemyType.vertical)
        {
            // 敵が描画されていて、音声コンポーネントが存在し、カメラに映っている場合
            if (this._isRenderedBird == true && enemyVoice != null && followCamera.OnCamera == true)
            {
                enemyVoice.BirdVoiceNomal(); // 鳥の通常音声を再生
            }

            // 新しいY座標を計算（サイン波による上下動）
            float newY = initialPosition.y + Mathf.Sin(Time.time * moveSpeedVertical) * amplitude;

            // 新しいX座標を計算（左右移動）
            float newX = transform.position.x - (movingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(newX, newY, transform.position.z); // 新しい位置に移動

            // 方向転換の時間を計測
            timeSinceLastDirectionChange += Time.deltaTime;
            if (timeSinceLastDirectionChange >= changeDirectionInterval)
            {
                movingRight = !movingRight; // 移動方向を反転
                timeSinceLastDirectionChange = 0f; // タイマーをリセット
            }

            // 移動方向に応じてバンの表示を切り替え
            if (movingRight)
            {
                if (Ban1 != null && Ban2 != null)
                {
                    Ban1.SetActive(true); // Ban1を表示
                    Ban2.SetActive(false); // Ban2を非表示
                    spriteRenderer.flipX = false; // スプライトを反転しない
                }
            }
            else if (!movingRight)
            {
                if (Ban1 != null && Ban2 != null)
                {
                    Ban2.SetActive(true); // Ban2を表示
                    Ban1.SetActive(false); // Ban1を非表示
                    spriteRenderer.flipX = true; // スプライトを反転
                }
            }
        }
    }

    private void Enemyvanish()
    {
        // ゲーム内のすべての敵が死亡している場合
        if (gameManager.EnemyAllDead == true)
        {
            Destroy(this.gameObject); // 自身のゲームオブジェクトを削除
        }
    }

    IEnumerator RandomJump()
    {
        // 無限ループ
        while (true)
        {
            // ランダムな時間待機
            yield return new WaitForSeconds(Random.Range(minJumpInterval, maxJumpInterval));
            Jump(); // ジャンプメソッドを呼び出す
        }
    }

    void Jump()
    {
        // 上方向に力を加えてジャンプさせる
        rb.velocity = Vector2.up * jumpForce;
    }

    private void Shoot()
    {
        // 弾を生成
        bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // 弾と自分のコリジョンを無視する
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OffCamera()
    {
        // スプライトのバウンディングボックスを取得
        Bounds spriteBounds = spriteRenderer.bounds;

        // スプライトの中心をビューポート座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);

        // スプライトがカメラのビューポート内にある場合
        if (viewportPosition.x > 0 && viewportPosition.x < 1)
        {
            // 敵のタイプに応じて描画状態を更新
            if (enemyType == EnemyType.vertical)
            {
                _isRenderedBird = true; // 鳥型敵の描画状態を設定
            }
            if (enemyType == EnemyType.side)
            {
                _isRendered = true; // 横型敵の描画状態を設定
            }
            if (enemyType == EnemyType.Throw)
            {
                _isRenderedThrow = true; // 投擲型敵の描画状態を設定
            }
        }
    }
}
