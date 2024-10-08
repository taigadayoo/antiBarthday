using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anitiVoice : MonoBehaviour
{
    public AudioSource audioSourceAnti;

    public bool OneVoiceAnti = false;

    public AudioClip[] MobSE;
    IEnumerator PlaySoundAndWaitMob()
    {
        if (audioSourceAnti != null)
        {
            if (!OneVoiceAnti)
            {
                if (MobSE.Length > 0)
                {
                    int randomIndex = Random.Range(0, MobSE.Length); // ランダムなインデックスを選択
                    audioSourceAnti.clip = MobSE[randomIndex]; // ランダムな音声クリップを選択
                    audioSourceAnti.Play(); // 選択した音声を再生

                }
                //audioSource.loop = true;
                OneVoiceAnti = true;
                yield return new WaitForSeconds(audioSourceAnti.clip.length);
                OneVoiceAnti = false;
            }
        }
    }

    public void EnemyAntiVoiceOn()
    {
        StartCoroutine(PlaySoundAndWaitMob());
    }
}
