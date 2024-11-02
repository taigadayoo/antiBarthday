using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeItem : MonoBehaviour
{
    private Player player; // プレイヤーの参照

    public float floatStrength = 400f; // アイテムの浮動の強度
    public float floatSpeed = 1f; // アイテムの浮動の速度
    [SerializeField]
    GameObject itemEffect; // アイテムの効果エフェクト

    GameManager gameManager; // ゲームマネージャーの参照

    private Vector3 startPosition; // アイテムの初期位置

    // 初期化処理
    void Start()
    {
        startPosition = transform.position; // 現在の位置を初期位置として記録
        GameObject playerObject = GameObject.Find("Player"); // "Player"オブジェクトを探す
        gameManager = FindObjectOfType<GameManager>(); // GameManagerを検索して取得
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>(); // Playerスクリプトを取得
            if (player == null)
            {
                Debug.LogError("PlayerオブジェクトにPlayerScriptがアタッチされていません！"); // Playerスクリプトが見つからない場合のエラーログ
            }
        }
        else
        {
            Debug.LogError("Playerオブジェクトが見つかりませんでした！"); // Playerオブジェクトが見つからない場合のエラーログ
        }
    }

    // 毎フレームの更新処理
    void Update()
    {
        Enemyvanish(); // 敵の消滅判定
        // Sin波でアイテムを浮動させる
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatStrength, 0);
    }

    // 衝突判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // BodyまたはYellowBodyタグのオブジェクトと衝突した際の処理
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody")
        {
            Instantiate(itemEffect, this.transform.position, this.transform.rotation); // アイテムのエフェクトを生成
        }
    }

    // 全ての敵が倒された際のアイテム消滅処理
    private void Enemyvanish()
    {
        if (gameManager.EnemyAllDead == true)
        {
            Destroy(this.gameObject); // 全ての敵が倒されていればアイテムを削除
        }
    }
}
