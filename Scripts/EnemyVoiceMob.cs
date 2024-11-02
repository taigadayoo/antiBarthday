using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoiceMob : MonoBehaviour
{
    public AudioSource audioSourceMattyo; // モブの音声を再生するAudioSource

    public bool OneVoiceNomal = false; // 一度だけ音声を再生するフラグ

    public AudioClip[] MobSE; // モブの効果音クリップの配列

    // モブの音声を再生して待機するコルーチン
    IEnumerator PlaySoundAndWaitMob()
    {
        if (audioSourceMattyo != null) // AudioSourceが設定されている場合
        {
            if (!OneVoiceNomal) // 一度も再生していない場合
            {
                if (MobSE.Length > 0) // 効果音クリップがある場合
                {
                    int randomIndex = Random.Range(0, MobSE.Length); // ランダムなインデックスを選択
                    audioSourceMattyo.clip = MobSE[randomIndex]; // ランダムな音声クリップを設定
                    audioSourceMattyo.Play(); // 選択した音声を再生
                }
                //audioSource.loop = true; // ループ設定（コメントアウト中）
                OneVoiceNomal = true; // フラグを立てる
                yield return new WaitForSeconds(audioSourceMattyo.clip.length); // 音声の再生が終わるまで待つ
                OneVoiceNomal = false; // フラグをリセット
            }
        }
    }

    // モブの通常音声を再生するメソッド
    public void EnemyNomalVoiceOn()
    {
        StartCoroutine(PlaySoundAndWaitMob()); // サウンドを再生するコルーチンを開始
    }
}
