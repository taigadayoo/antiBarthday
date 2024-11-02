using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    GameObject itemEffect; // コイン取得時に生成するエフェクト

    // 衝突判定処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // BodyまたはYellowBodyタグのオブジェクトと衝突した際の処理
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody")
        {
            Instantiate(itemEffect, this.transform.position, this.transform.rotation); // エフェクトを生成
            Destroy(this.gameObject); // コインを削除
            SampleSoundManager.Instance.PlaySe(SeType.SE10); // コイン取得音を再生
        }
    }
}
