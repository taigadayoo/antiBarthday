using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeBer : MonoBehaviour
{
    [SerializeField] private Image _hpBarcurrent; // 現在のHPバーのイメージ
    [SerializeField] private Player player; // プレイヤーの参照
    private float currentBullet; // 初期の弾数（HPバーの基準となる値）

    // 初期化処理
    void Awake()
    {
        currentBullet = player.bulletNum; // プレイヤーの弾数を基準値として設定
    }

    // 毎フレームの更新処理
    void Update()
    {
        // HPバーのfillAmountを現在の弾数と基準の弾数に応じて設定
        _hpBarcurrent.fillAmount = player.bulletNum / currentBullet;
    }
}
