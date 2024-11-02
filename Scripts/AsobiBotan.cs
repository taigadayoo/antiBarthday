using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsobiBotan : MonoBehaviour
{
    // シーン名（クリアシーンとタイトルシーン）を設定するための変数
    [SerializeField] private string sceneNameClear; // クリアシーンの名前
    [SerializeField] private string sceneNameTitle; // タイトルシーンの名前
    [SerializeField] private Color fadeColor; // フェード時の色
    [SerializeField] private float fadeSpeed; // フェードの速度

    AudioSource audioSource; // 効果音再生用のAudioSource
    enum Scene
    {
        Asobi, // 遊びシーン
        Title  // タイトルシーン
    }

    [SerializeField]
    Scene scene; // 現在のシーンの状態

    PlayerController playerController; // プレイヤーのコントローラー

    // 初期化処理
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントの取得
        playerController = GetComponent<PlayerController>(); // PlayerControllerコンポーネントの取得
    }

    // 毎フレーム更新処理
    void Update()
    {
        // プレイヤーがジャンプボタンを押し、かつ現在のシーンが遊びシーンの場合、タイトルシーンへ遷移
        if (playerController.IsJumpPressed && scene == Scene.Asobi)
        {
            OnTitle();
        }
        // プレイヤーがリセットボタンを押し、かつ現在のシーンがタイトルシーンの場合、遊びシーンへ遷移
        if (playerController.IsResetPressed && scene == Scene.Title)
        {
            OnAsobi();
        }
    }

    // 遊びシーンへ遷移する処理
    public void OnAsobi()
    {
        audioSource.Play(); // 効果音を再生
        Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed); // クリアシーンへのフェード遷移
    }

    // タイトルシーンへ遷移する処理
    public void OnTitle()
    {
        audioSource.Play(); // 効果音を再生
        Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed); // タイトルシーンへのフェード遷移
    }
}
