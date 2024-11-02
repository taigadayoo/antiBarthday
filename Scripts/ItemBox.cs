using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    GameObject breakEffect; // 壊れたときのエフェクト
    [SerializeField]
    GameObject CakeItem; // アイテムとして出現するケーキ
    [SerializeField]
    GameObject enemy; // 出現する敵のプレハブ

    GameManager gameManager; // ゲームマネージャーへの参照

    // ボックスの種類を定義する列挙型
    private enum BoxName
    {
        WoodBox,       // 木のボックス
        NomalBox,      // 通常のボックス
        WoodNomalBox,  // 木の通常ボックス
        EnemyBox       // 敵のボックス
    }

    [SerializeField]
    BoxName boxName; // 現在のボックスの種類

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // ゲームマネージャーを取得
    }

    // Update is called once per frame
    void Update()
    {
        Enemyvanish(); // 敵の消去処理を実行
    }

    // 敵が全滅した場合にボックスを消去する処理
    private void Enemyvanish()
    {
        if (gameManager.EnemyAllDead == true && (boxName == BoxName.NomalBox) || gameManager.EnemyAllDead == true && (boxName == BoxName.EnemyBox))
        {
            Destroy(this.gameObject); // ボックスを消去
        }
    }

    // 衝突判定の処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 通常のボックスに衝突した場合
        if (boxName == BoxName.NomalBox)
        {
            if (collision.gameObject.tag == "RedCake" || collision.gameObject.tag == "Cake")
            {
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // 壊れたエフェクトを生成
                Instantiate(CakeItem, this.transform.position, this.transform.rotation); // ケーキアイテムを生成
                Destroy(this.gameObject); // ボックスを消去
            }
        }

        // 木のボックスに衝突した場合
        if (boxName == BoxName.WoodBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18); // 効果音を再生
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // 壊れたエフェクトを生成
                Instantiate(CakeItem, this.transform.position, this.transform.rotation); // ケーキアイテムを生成
                Destroy(this.gameObject); // ボックスを消去
            }
        }

        // 木の通常ボックスに衝突した場合
        if (boxName == BoxName.WoodNomalBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18); // 効果音を再生
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // 壊れたエフェクトを生成
                Destroy(this.gameObject); // ボックスを消去
            }
        }

        // 敵のボックスに衝突した場合
        if (boxName == BoxName.EnemyBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18); // 効果音を再生
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // 壊れたエフェクトを生成
                Instantiate(enemy, this.transform.position, this.transform.rotation); // 敵を生成
                Destroy(this.gameObject); // ボックスを消去
            }
        }
    }
}
