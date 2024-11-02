using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 30f; // プレイヤーの移動速度
    public float bulletPower = 30.0f; // 弾のパワー
    public int bulletNum = 15; // 所持している弾の数
    public int MaxCakeNum = 15; // 最大ケーキ数

    [SerializeField]
    GameManager gameManager; // ゲームマネージャーの参照
    public new Animator animation; // プレイヤーのアニメーター
    [SerializeField]
    private Rigidbody2D rb; // プレイヤーのRigidbody2D
    [SerializeField]
    public SpriteRenderer spriteRenderer; // プレイヤーのスプライトレンダラー
    private Vector2 movementVector; // プレイヤーの移動ベクトル
    public bool canJump = true; // ジャンプ可能かどうかのフラグ
    [SerializeField, Header("ジャンプ力")]
    private int jumpPower; // ジャンプ力
    [SerializeField, Header("最大ジャンプ数")]
    public int maxJumpCount = 2; // 最大ジャンプ数
    public int currentJumpCount = 0; // 現在のジャンプ数
    [SerializeField]
    private GameObject Bullet; // 通常の弾のプレハブ
    [SerializeField]
    GameObject redBullet; // 赤い弾のプレハブ
    private Vector3 rightBulletPoint; // 右側からの弾の発射位置
    private Vector3 leftBulletPoint; // 左側からの弾の発射位置
    private int MaxBullet = 0; // 最大弾数
    private bool JumpAni = false; // ジャンプアニメーション中かどうかのフラグ
    [SerializeField] float flashInterval; // 点滅間隔
    public float invincibleTime = 2.0f; // 無敵時間
    public bool isInvincible = false; // プレイヤーが無敵かどうかを管理するフラグ
    private float invincibleStartTime; // 無敵状態が開始された時間
    private float nextFlashTime; // 次に点滅させる時間
    [SerializeField]
    Damage damage; // ダメージ管理
    [SerializeField]
    PlayerAbility playerAbility; // プレイヤーのアビリティ管理

    PlayerController _playerController; // プレイヤーコントローラーの参照

    private void Awake()
    {
        Initialize(); // 初期化処理を呼び出す
    }

    void Start()
    {
        _playerController = GetComponent<PlayerController>(); // PlayerControllerを取得
        animation = gameObject.GetComponent<Animator>(); // Animatorを取得

        // 弾の発射位置を設定
        rightBulletPoint = transform.Find("BulletPoint").localPosition;
        leftBulletPoint = transform.Find("LeftBulletPoint").localPosition;
    }

    // 初期化処理
    private void Initialize()
    {
        currentJumpCount = maxJumpCount; // 現在のジャンプ数を最大に設定
        var jumpC = gameObject.GetComponentInChildren<PlayerJumpController>(); // 子オブジェクトのPlayerJumpControllerを取得
        jumpC.JumpEvent += ResetJump; // ジャンプイベントにResetJumpメソッドを登録
    }

    void Update()
    {
        PlayerMovement(); // プレイヤーの移動処理を呼び出す

        // スペースキーまたはジャンプボタンが押された時の処理
        if (Input.GetKeyDown(KeyCode.Space) || _playerController.IsJumpPressed)
        {
            HandleJump(); // ジャンプ処理を呼び出す
        }

        // プレイヤーのアビリティによる弾の発射処理
        if (playerAbility.ability == PlayerAbility.Ability.red)
        {
            // マウスボタンが押された時の処理
            if (Input.GetMouseButtonDown(0) || _playerController.IsSelectPressed)
            {
                // 最大弾数を超え、弾が3以上ある場合
                if (bulletNum > MaxBullet && bulletNum >= 3)
                {
                    // 右側からの発射処理
                    if (spriteRenderer.flipX == false)
                    {
                        OnThrow(); // 投げるアニメーションを開始
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // サウンド再生
                        Instantiate(redBullet, transform.position + rightBulletPoint, Quaternion.identity); // 弾の生成
                        Invoke("OffThrow", 0.25f); // 投げるアニメーションをオフにする
                    }
                    // 左側からの発射処理
                    else
                    {
                        OnThrow(); // 投げるアニメーションを開始
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // サウンド再生
                        Instantiate(redBullet, transform.position + leftBulletPoint, Quaternion.identity); // 弾の生成
                        Invoke("OffThrow", 0.25f); // 投げるアニメーションをオフにする
                    }
                    bulletNum -= 3; // 弾の数を減少
                }
            }
        }
        // 通常の弾の発射処理
        else
        {
            // マウスボタンが押された時の処理
            if (Input.GetMouseButtonDown(0) || _playerController.IsSelectPressed)
            {
                // 最大弾数を超えている場合
                if (bulletNum > MaxBullet)
                {
                    // 右側からの発射処理
                    if (spriteRenderer.flipX == false)
                    {
                        OnThrow(); // 投げるアニメーションを開始
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // サウンド再生
                        Instantiate(Bullet, transform.position + rightBulletPoint, Quaternion.identity); // 弾の生成
                        Invoke("OffThrow", 0.25f); // 投げるアニメーションをオフにする
                    }
                    // 左側からの発射処理
                    else
                    {
                        OnThrow(); // 投げるアニメーションを開始
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // サウンド再生
                        Instantiate(Bullet, transform.position + leftBulletPoint, Quaternion.identity); // 弾の生成
                        Invoke("OffThrow", 0.25f); // 投げるアニメーションをオフにする
                    }
                    bulletNum -= 1; // 弾の数を減少
                }
            }
        }
    }

    // プレイヤーの移動処理
    private void PlayerMovement()
    {
        float horizontalKey = Input.GetAxis("Horizontal"); // 水平入力を取得
        movementVector = rb.velocity; // 現在の速度を取得
        movementVector.x = horizontalKey * moveSpeed; // 水平移動速度を設定
        rb.velocity = movementVector; // Rigidbodyの速度を更新

        // 右方向への移動処理
        if (horizontalKey > 0)
        {
            if (JumpAni == false)
            {
                animation.SetBool("Walk", true); // 歩行アニメーションを有効にする
            }
            spriteRenderer.flipX = false; // スプライトを右向きにする
        }
        // 左方向への移動処理
        if (horizontalKey < 0)
        {
            if (JumpAni == false)
            {
                animation.SetBool("Walk", true); // 歩行アニメーションを有効にする
            }
            spriteRenderer.flipX = true; // スプライトを左向きにする
        }
        // 停止時の処理
        if (horizontalKey == 0)
        {
            animation.SetBool("Walk", false); // 歩行アニメーションを無効にする
        }
    }

    // ジャンプ処理
    private void HandleJump()
    {
        // ジャンプ可能かつジャンプ数が残っている場合
        if (canJump && currentJumpCount > 0)
        {
            Jump(); // ジャンプ処理を呼び出す
            currentJumpCount--; // ジャンプ数を減少
        }
        // ジャンプ数が0になったらジャンプ不可
        if (currentJumpCount <= 0) canJump = false;
    }
    private void Jump()
    {
        // ジャンプ音を再生
        SampleSoundManager.Instance.PlaySe(SeType.SE3);

        // ジャンプのベクトルを作成
        Vector2 jumpVector = new Vector2(0, jumpPower);

        // 現在の速度のY成分をリセット（下方向の速度を0にする）
        rb.velocity = new Vector2(rb.velocity.x, 0);

        // ジャンプの力を加える（インパルス方式で）
        rb.AddForce(jumpVector, ForceMode2D.Impulse);

        // ジャンプアニメーションを有効にし、歩行アニメーションを無効にする
        animation.SetBool("Jump", true);
        animation.SetBool("Walk", false);

        // ジャンプアニメーション中フラグをセット
        JumpAni = true;
    }

    private void ResetJump()
    {
        // ジャンプカウントをリセット
        currentJumpCount = maxJumpCount;

        // ジャンプ可能状態に設定
        canJump = true;

        // ジャンプアニメーションをオフにする
        OffJump();

        // ジャンプアニメーション中フラグをリセット
        JumpAni = false;
    }

    public void OnThrow()
    {
        // 投げるアニメーションを有効にする
        animation.SetBool("throw", true);
    }

    public void OffThrow()
    {
        // 投げるアニメーションを無効にする
        animation.SetBool("throw", false);
    }

    public void OnJump()
    {
        // ジャンプアニメーションを有効にする
        animation.SetBool("Jump", true);
    }

    public void OffJump()
    {
        // ジャンプアニメーションを無効にする
        animation.SetBool("Jump", false);
    }

    public void OnDead()
    {
        // 死亡アニメーションを有効にする
        animation.SetBool("Dead", true);

        // オブジェクトを90度回転させる（死亡時の演出）
        this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, 90);

        // Rigidbodyの位置を固定して、動かないようにする
        this.rb.constraints = RigidbodyConstraints2D.FreezePosition;
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void OffDead()
    {
        // ダメージを受けている場合
        if (damage.Down == true)
        {
            // 死亡アニメーションを無効にする
            animation.SetBool("Dead", false);

            // オブジェクトを元の回転に戻す
            this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, -90);

            // Rigidbodyの位置制約を解除して、動かせるようにする
            this.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            // ダメージフラグをリセット
            damage.Down = false;
        }
    }

    void StartInvincibility()
    {
        // 無敵状態を開始
        isInvincible = true;
        invincibleStartTime = Time.time; // 無敵開始時刻を記録
        nextFlashTime = Time.time; // 点滅開始時刻を記録

        // 一定間隔で点滅処理を実行する
        InvokeRepeating("FlashPlayer", 0, flashInterval);

        // 無敵時間が終了したら無敵状態を解除する
        Invoke("EndInvincibility", invincibleTime);
    }

    void FlashPlayer()
    {
        // スプライトの表示を切り替える（点滅効果）
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    void EndInvincibility()
    {
        // 無敵状態を解除
        isInvincible = false;

        // スプライトを表示する（無敵状態が解除されたときに表示されるようにする）
        spriteRenderer.enabled = true;

        // InvokeRepeating を停止する
        CancelInvoke("FlashPlayer");
    }

    public void ActivateInvincibility()
    {
        // 無敵でない場合に無敵を開始
        if (!isInvincible)
        {
            StartInvincibility();
        }
    }
}