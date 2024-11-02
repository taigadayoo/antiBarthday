using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anitiVoice : MonoBehaviour
{
    public AudioSource audioSourceAnti; // 効果音のAudioSource

    public bool OneVoiceAnti = false; // 効果音が再生中かどうかを示すフラグ

    public AudioClip[] MobSE; // 複数のモブ効果音クリップを格納する配列

    // ランダムな効果音を再生し、再生が終わるまで待つコルーチン
    IEnumerator PlaySoundAndWaitMob()
    {
        // AudioSourceが存在するか確認
        if (audioSourceAnti != null)
        {
            // 効果音がまだ再生されていない場合のみ再生
            if (!OneVoiceAnti)
            {
                // 効果音の配列が空でないか確認
                if (MobSE.Length > 0)
                {
                    int randomIndex = Random.Range(0, MobSE.Length); // ランダムなインデックスを選択
                    audioSourceAnti.clip = MobSE[randomIndex]; // ランダムな音声クリップを選択
                    audioSourceAnti.Play(); // 選択した音声を再生
                }

                // 効果音再生中のフラグを立てる
                OneVoiceAnti = true;
                yield return new WaitForSeconds(audioSourceAnti.clip.length); // 効果音の長さ分だけ待機
                OneVoiceAnti = false; // 再生終了後、フラグをリセット
            }
        }
    }

    // 敵の効果音再生を開始する関数
    public void EnemyAntiVoiceOn()
    {
        StartCoroutine(PlaySoundAndWaitMob()); // 効果音再生のコルーチンを開始
    }
}
