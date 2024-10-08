using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Endroll : MonoBehaviour
{
    public float duration = 40f; // エンドロールの完了までの時間
    public float verticalOffset = 8000f; // 上方向に移動させる量
    private bool endRollComplete = false; // エンドロールが完了したかどうかを示すフラグ

    [SerializeField] private string sceneNameClear;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    [SerializeField]
    AudioSource audioSourceOme;
    [SerializeField]
    AudioSource audioSourceEva;

    PlayerController playerController;
    void Start()
    {
        audioSourceEva.Play();
        audioSourceOme.Play();
        playerController = GetComponent<PlayerController>();
        // 子オブジェクト全体を取得し、アニメーションを適用する
        foreach (Transform child in transform)
        {
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                // DOTweenを使ってアニメーションを作成し、上方向に移動
                text.rectTransform.DOAnchorPosY(text.rectTransform.anchoredPosition.y + verticalOffset, duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => { endRollComplete = true; }); // アニメーション完了時にフラグを立てる
            }
        }
    }

    // エンドロールが完了したかどうかを確認するためのメソッド
    public bool IsEndRollComplete()
    {
        return endRollComplete;
    }

    private void Update()
    {
        if (endRollComplete || Input.GetKeyDown(KeyCode.T) || playerController.IsTitlePressed)
        {
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
        }
    }
}

    
