using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill; // タイマーUIの塗りつぶし画像
    [SerializeField]
    PlayerAbility playerAbility; // プレイヤーの能力を管理するクラスへの参照

    private float currentTime = 0f; // 現在のタイマーの時間

    void Start()
    {
        // 初期化処理が必要な場合にここに記述
    }

    private void UpdateTimerUI()
    {
        // タイマーUIの更新
        if (uiFill != null)
        {
            float elapsedTime = playerAbility.YellowCoolTime - currentTime; // 経過時間を計算
            float fillAmount = elapsedTime / playerAbility.YellowCoolTime; // 塗りつぶし割合を計算
            uiFill.fillAmount = Mathf.Clamp01(fillAmount); // 塗りつぶし割合を0から1の範囲に制限
        }
    }

    public void StartTimer(float cooldown)
    {
        // タイマーの開始
        playerAbility.YellowCoolTime = cooldown; // クールダウン時間を設定
        currentTime = playerAbility.YellowCoolTime; // 現在の時間をクールダウン時間に設定
        UpdateTimerUI(); // UIを更新
    }

    // Update is called once per frame
    void Update()
    {
        // タイマーがオフスイッチの状態である場合にタイマーを更新
        if (playerAbility.YellowOffSwitch)
        {
            currentTime -= Time.deltaTime; // 経過時間を減少
            if (currentTime <= 0f)
            {
                currentTime = 0f; // タイマーが0以下にならないように設定
            }
            UpdateTimerUI(); // UIを更新
        }
    }
}
