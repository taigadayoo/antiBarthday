using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager; // ゲームの状態を管理するGameManager
    [SerializeField]
    Player Player; // プレイヤーオブジェクト
    public bool Down = false; // プレイヤーがダウンしているかどうか
    public bool OneDamage = false; // 一度だけダメージを受けたかどうか
    [SerializeField] private string sceneNameClear; // シーンクリア時の遷移先
    [SerializeField] private Color fadeColor; // フェードアウト時の色
    [SerializeField] private float fadeSpeed; // フェードアウトの速度

    private bool OnSave = false; // セーブポイント1を保存したかどうか
    private bool OnSave2 = false; // セーブポイント2を保存したかどうか

    public bool isHit = false; // プレイヤーがダメージを受けたかどうか

    PlayerAbility playerAbility; // プレイヤーの能力を管理するスクリプト

    private void Start()
    {
        playerAbility = FindObjectOfType<PlayerAbility>(); // PlayerAbilityスクリプトの取得
    }

    // プレイヤーが死亡したときの処理
    private void DamageDead()
    {
        gameManager.Dead(); // ゲームマネージャーに死亡処理を通知
        isHit = false; // ダメージ受けたフラグをリセット
    }

    // トリガーに入ったときの処理
    void OnTriggerEnter2D(Collider2D other)
    {
        // 死亡判定
        if (other.gameObject.tag == "Dead" && !isHit)
        {
            playerAbility.YellowOffSwitch = false; // プレイヤーのスイッチオフ
            Player.enabled = false; // プレイヤーの動きを無効化
            SampleSoundManager.Instance.PlaySe(SeType.SE5); // 死亡音を再生
            gameManager.EnemyAllDead = true; // 敵全滅フラグを立てる
            playerAbility.enabled = false; // プレイヤーの能力を無効化
            Invoke("DamageDead", 1.5f); // 1.5秒後に死亡処理を実行
            this.enabled = false; // このスクリプトを無効化
            isHit = true; // ダメージを受けたフラグを立てる
        }

        // ケーキアイテム取得処理
        if (other.gameObject.tag == "CakeItem")
        {
            Player.bulletNum = Player.MaxCakeNum; // プレイヤーの弾数を最大に
            SampleSoundManager.Instance.PlaySe(SeType.SE6); // アイテム取得音を再生
            Destroy(other.gameObject); // ケーキアイテムを削除
        }

        // セーブポイント1処理
        if (other.gameObject.tag == "SavePoint")
        {
            Vector3 respawnPosition = other.transform.position; // リスポーン位置の取得
            respawnPosition.y += 18; // リスポーン位置を上に移動
            gameManager.RespawnPoint = respawnPosition; // ゲームマネージャーにリスポーン位置を設定
            if (OnSave == false)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // セーブポイント到達音
                SampleSoundManager.Instance.PlaySe(SeType.SE17);
            }
            OnSave = true; // セーブポイント到達フラグを立てる
        }

        // セーブポイント2処理
        if (other.gameObject.tag == "SavePoint2")
        {
            Vector3 respawnPosition = other.transform.position; // リスポーン位置の取得
            respawnPosition.y += 18; // リスポーン位置を上に移動
            gameManager.RespawnPoint = respawnPosition; // ゲームマネージャーにリスポーン位置を設定
            if (OnSave2 == false)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // セーブポイント到達音
                SampleSoundManager.Instance.PlaySe(SeType.SE17);
            }
            OnSave2 = true; // セーブポイント到達フラグを立てる
        }

        // ゴール判定
        if (other.gameObject.tag == "Goal")
        {
            Player.enabled = false; // プレイヤーの動きを無効化
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed); // シーン遷移処理
            SampleSoundManager.Instance.StopBgm(); // BGM停止
        }
    }

    // 衝突判定処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ダメージを受けた場合の処理
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "DamageObject" && !isHit)
        {
            isHit = true; // ダメージを受けたフラグを立てる
            this.enabled = false; // このスクリプトを無効化
            SampleSoundManager.Instance.PlaySe(SeType.SE2); // ダメージ音を再生
            Down = true; // プレイヤーがダウンしたフラグを立てる
            Player.enabled = false; // プレイヤーの動きを無効化
            gameManager.EnemyAllDead = true; // 敵全滅フラグを立てる
            playerAbility.enabled = false; // プレイヤーの能力を無効化
            Player.animation.SetBool("Dead", true); // プレイヤーのアニメーションを死亡に設定
            if (OneDamage == false)
            {
                Player.OnDead(); // プレイヤーの死亡処理を実行
                OneDamage = true; // 一度だけダメージを受けたフラグを立てる
                Invoke("DamageDead", 1.5f); // 1.5秒後に死亡処理を実行
            }
        }
    }
}
