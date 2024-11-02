using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoice : MonoBehaviour
{
    public AudioClip BirdSE; // 鳥の効果音
    public AudioClip MobSE; // モブの効果音
    public AudioClip AntiSE; // アンチの効果音
    public AudioSource audioSource; // 鳥の音声を再生するAudioSource
    public AudioSource audioSourceMattyo; // モブの音声を再生するAudioSource
    public AudioSource audioSourceAnti; // アンチの音声を再生するAudioSource

    public bool OneVoice = false; // 一度だけ音声を再生するフラグ

    // 通常の鳥の声を再生するメソッド
    public void BirdVoiceNomal()
    {
        StartCoroutine(PlaySoundAndWait()); // サウンドを再生するコルーチンを開始
    }

    // 音声を再生して待機するコルーチン
    IEnumerator PlaySoundAndWait()
    {
        if (!OneVoice) // 一度も再生していない場合
        {
            audioSource.clip = BirdSE; // 鳥の効果音を設定
            audioSource.Play(); // 音声を再生
            //audioSource.loop = true; // ループ設定（コメントアウト中）
            OneVoice = true; // フラグを立てる
            yield return new WaitForSeconds(audioSource.clip.length); // 音声の再生が終わるまで待つ
            OneVoice = false; // フラグをリセット
        }
    }

    // 鳥が死んだときの効果音を再生するメソッド
    public void BirdDeath()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE12); // 死亡効果音を再生
    }

    // 音声再生フラグをリセットするメソッド
    public void OneVoiceReset()
    {
        OneVoice = false; // フラグをリセット
    }
}
