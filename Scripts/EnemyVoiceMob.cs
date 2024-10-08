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
                    int randomIndex = Random.Range(0, MobSE.Length); // �����_���ȃC���f�b�N�X��I��
                    audioSourceMattyo.clip = MobSE[randomIndex]; // �����_���ȉ����N���b�v��I��
                    audioSourceMattyo.Play(); // �I�������������Đ�

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
