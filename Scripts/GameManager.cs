using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerLife = 3; // プレイヤーのライフ
    [SerializeField]
    GameObject Player; // プレイヤーのゲームオブジェクト
    [SerializeField]
    public Vector3 RespawnPoint; // リスポーンポイント
    [SerializeField] private string sceneName; // ゲームオーバー時に遷移するシーン名
    [SerializeField] private string sceneNameTitle; // タイトルシーン名
    [SerializeField] private Color fadeColor; // フェードの色
    [SerializeField] private float fadeSpeed; // フェードの速度
    [SerializeField] Player player; // プレイヤーのスクリプト
    [SerializeField]
    Damage damage; // ダメージ処理用のスクリプト
    [SerializeField]
    SpawnManager spawnManager; // スポーンマネージャーの参照
    public bool EnemyAllDead = false; // 敵が全て死んでいるかどうかのフラグ

    private Transform playerTransform; // プレイヤーのTransform
    private Transform enemyTransform; // 敵のTransform
    public Vector3 playerPosition; // プレイヤーの位置
    public Vector3 enemyPosition; // 敵の位置
    public bool OnLeft = false; // プレイヤーと敵の位置関係を判定するフラグ

    private bool OnBGM = false; // BGMが再生中かどうかのフラグ
    PlayerAbility playerAbility; // プレイヤーの能力を管理するスクリプト

    [SerializeField]
    PlayerController playerController; // プレイヤーの操作を管理するスクリプト

    FollowCamera followCamera; // カメラの追従を管理するスクリプト

    private void Awake()
    {
        followCamera = FindObjectOfType<FollowCamera>(); // FollowCameraスクリプトを探して取得
    }

    void Start()
    {
        // カメラが追従している場合にBGMを再生
        if (followCamera.OnCamera)
        {
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // プレイヤーのTransformを取得
        enemyTransform = GameObject.Find("EnemyThrow").transform; // 敵のTransformを取得
        playerAbility = FindObjectOfType<PlayerAbility>(); // PlayerAbilityスクリプトを取得
    }

    void Update()
    {
        // カメラが追従していて、BGMが再生中でない場合にBGMを再生
        if (followCamera.OnCamera && OnBGM == false)
        {
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
            OnBGM = true; // BGMが再生中のフラグを設定
        }

        GameOver(); // ゲームオーバーの判定
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position; // プレイヤーの位置を更新
        }
        if (enemyTransform != null)
        {
            enemyPosition = enemyTransform.position; // 敵の位置を更新
        }
        LeftorRight(); // プレイヤーと敵の位置関係を判定

        // Escapeキーまたはリセットボタンが押された場合にタイトルシーンに戻る
        if (Input.GetKeyDown(KeyCode.Escape) || playerController.IsResetPressed)
        {
            Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed); // フェードを開始
            SampleSoundManager.Instance.StopBgm(); // BGMを停止
        }
    }

    public void Dead()
    {
        PlayerLife -= 1; // プレイヤーのライフを減少
        if (PlayerLife != 0)
        {
            damage.enabled = true; // ダメージ処理を有効にする
            playerAbility.YellowOffSwitch = false; // プレイヤーの能力をリセット
            playerAbility.lastTrueTime = Time.time; // 最後の正しい時間を記録
            player.OffDead(); // プレイヤーの死に関する処理
            player.enabled = true; // プレイヤーを再度有効にする
            Player.transform.position = RespawnPoint; // プレイヤーをリスポーンポイントに移動
            player.bulletNum = player.MaxCakeNum; // 弾の数を最大に設定
            player.OffJump(); // ジャンプを無効にする
            player.OffThrow(); // 投擲を無効にする
            damage.OneDamage = false; // 一度のダメージをリセット
            EnemyAllDead = false; // 敵全滅のフラグをリセット
            spawnManager.RespawnAll(); // 敵を全てリスポーンさせる
            playerAbility.enabled = true; // プレイヤーの能力を有効にする
            playerAbility.NomalMode(); // 通常モードに設定
        }
        else if (PlayerLife == 0)
        {
            Destroy(Player); // プレイヤーが死亡した場合はオブジェクトを削除
        }
    }

    private void GameOver()
    {
        if (PlayerLife == 0)
        {
            Initiate.Fade(sceneName, fadeColor, fadeSpeed); // ゲームオーバー時にフェードを開始
            SampleSoundManager.Instance.StopBgm(); // BGMを停止
        }
    }

    private void LeftorRight()
    {
        // 敵の位置がプレイヤーの位置より右にある場合
        if (enemyPosition.x > playerPosition.x)
        {
            OnLeft = true; // プレイヤーが左側にいる
        }
        // 敵の位置がプレイヤーの位置より左にある場合
        if (enemyPosition.x < playerPosition.x)
        {
            OnLeft = false; // プレイヤーが右側にいる
        }
    }
}
