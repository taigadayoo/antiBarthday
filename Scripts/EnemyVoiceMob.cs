using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoiceMob : MonoBehaviour
{
    public AudioSource audioSourceMattyo;

    public bool OneVoiceNomal = false;

    public AudioClip[] MobSE;
    IEnumerator PlaySoundAndWaitMob()
    {
        if (audioSourceMattyo != null)
        {
            if (!OneVoiceNomal)
            {
                if (MobSE.Length > 0)
                {
                    int randomIndex = Random.Range(0, MobSE.Length); // ランダムなインデックスを選択
                    audioSourceMattyo.clip = MobSE[randomIndex]; // ランダムな音声クリップを選択
                    audioSourceMattyo.Play(); // 選択した音声を再生

                }
                //audioSource.loop = true;
                OneVoiceNomal = true;
                yield return new WaitForSeconds(audioSourceMattyo.clip.length);
                OneVoiceNomal = false;
            }
        }
    }

    public void EnemyNomalVoiceOn()
    {
        StartCoroutine(PlaySoundAndWaitMob());
    }
}
