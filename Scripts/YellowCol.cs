using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCol : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager; // ゲームマネージャーの参照
    [SerializeField]
    GameObject deathEffect; // プレイヤーが死んだときのエフェクト
    [SerializeField]
    Player Player; // プレイヤーの参照
    public bool Down = false; // ダウン状態のフラグ
    public bool OneDamage = false; // 一度だけダメージを受けるフラグ

    [SerializeField] private string sceneNameClear; // クリア後のシーン名
    [SerializeField] private Color fadeColor; // フェード時の色
    [SerializeField] private float fadeSpeed; // フェードの速さ

    private bool OnSave = false; // セーブポイント1の保存状態
    private bool OnSave2 = false; // セーブポイント2の保存状態

    PlayerAbility playerAbility; // プレイヤーの能力管理クラス
    Player player; // プレイヤーオブジェクトの参照

    private void Start()
    {
        // プレイヤー能力の取得
        playerAbility = FindObjectOfType<PlayerAbility>();
        // プレイヤーオブジェクトの取得
        player = FindObjectOfType<Player>();
    }

    private void DamageDead()
    {
        // プレイヤーが死んだときの処理
        gameManager.Dead();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したオブジェクトのタグを確認
        if (other.gameObject.tag == "Dead")
        {
            Player.enabled = false; // プレイヤーの操作を無効化
            SampleSoundManager.Instance.PlaySe(SeType.SE5); // 死亡音の再生
            gameManager.EnemyAllDead = true; // 敵が全て死亡した状態に設定
            playerAbility.enabled = false; // プレイヤーの能力を無効化
            Invoke("DamageDead", 1.5f); // 1.5秒後にDamageDeadメソッドを呼び出す
        }
        if (other.gameObject.tag == "CakeItem")
        {
            // ケーキアイテムを取得した場合
            Player.bulletNum = Player.MaxCakeNum; // 弾数を最大に設定
            SampleSoundManager.Instance.PlaySe(SeType.SE6); // アイテム取得音の再生
            Destroy(other.gameObject); // アイテムを破棄
        }
        if (other.gameObject.tag == "SavePoint")
        {
            // セーブポイント1に触れた場合
            Vector3 respawnPosition = other.transform.position; // セーブポイントの位置を取得
            respawnPosition.y += 18; // respawn位置を上に移動
            gameManager.RespawnPoint = respawnPosition; // respawnポイントを設定
            if (OnSave == false) // 初めて触れた場合
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // セーブ音の再生
                SampleSoundManager.Instance.PlaySe(SeType.SE17); // セーブ音の再生
            }
            OnSave = true; // セーブ状態を更新
        }
        if (other.gameObject.tag == "SavePoint2")
        {
            // セーブポイント2に触れた場合
            Vector3 respawnPosition = other.transform.position; // セーブポイントの位置を取得
            respawnPosition.y += 18; // respawn位置を上に移動
            gameManager.RespawnPoint = respawnPosition; // respawnポイントを設定
            if (OnSave2 == false) // 初めて触れた場合
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // セーブ音の再生
                SampleSoundManager.Instance.PlaySe(SeType.SE17); // セーブ音の再生
            }
            OnSave2 = true; // セーブ状態を更新
        }
        if (other.gameObject.tag == "Goal")
        {
            // ゴールに触れた場合
            Player.enabled = false; // プレイヤーの操作を無効化
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed); // フェードを開始
            SampleSoundManager.Instance.StopBgm(); // BGMを停止
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突したオブジェクトのタグを確認
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "EnemyBullet")
        {
            // ダメージや敵の弾と衝突した場合
            Instantiate(deathEffect, collision.transform.position, collision.transform.rotation); // 死亡エフェクトの生成
            playerAbility.ability = PlayerAbility.Ability.nomal; // プレイヤーの能力をノーマルに戻す
            playerAbility.YellowOffSwitch = true; // イエロー能力のオフスイッチ
            playerAbility.StartYellowAbilityCooldown(); // イエロー能力のクールダウンを開始
            playerAbility.lastTrueTime = Time.time; // 最後に能力が有効だった時間を記録
            Destroy(collision.gameObject); // 衝突したオブジェクトを破棄
            playerAbility.YellowOn = false; // イエロー能力をオフにする
            playerAbility.nomalOn = true; // ノーマル能力をオンにする
            player.ActivateInvincibility(); // プレイヤーの無敵状態をアクティブ化
        }
        if (collision.gameObject.tag == "DamageObject")
        {
            // ダメージオブジェクトと衝突した場合
            playerAbility.YellowOffSwitch = true; // イエロー能力のオフスイッチ
            playerAbility.StartYellowAbilityCooldown(); // イエロー能力のクールダウンを開始
            playerAbility.lastTrueTime = Time.time; // 最後に能力が有効だった時間を記録
            playerAbility.ability = PlayerAbility.Ability.nomal; // プレイヤーの能力をノーマルに戻す
            player.ActivateInvincibility(); // プレイヤーの無敵状態をアクティブ化
            playerAbility.nomalOn = true; // ノーマル能力をオンにする
            playerAbility.YellowOn = false; // イエロー能力をオフにする
        }
    }
}
