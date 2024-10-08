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
                    int randomIndex = Random.Range(0, MobSE.Length); // �����_���ȃC���f�b�N�X��I��
                    audioSourceAnti.clip = MobSE[randomIndex]; // �����_���ȉ����N���b�v��I��
                    audioSourceAnti.Play(); // �I�������������Đ�

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
