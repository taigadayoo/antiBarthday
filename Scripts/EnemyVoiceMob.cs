using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoiceMob : MonoBehaviour
{
    public AudioSource audioSourceMattyo; // ���u�̉������Đ�����AudioSource

    public bool OneVoiceNomal = false; // ��x�����������Đ�����t���O

    public AudioClip[] MobSE; // ���u�̌��ʉ��N���b�v�̔z��

    // ���u�̉������Đ����đҋ@����R���[�`��
    IEnumerator PlaySoundAndWaitMob()
    {
        if (audioSourceMattyo != null) // AudioSource���ݒ肳��Ă���ꍇ
        {
            if (!OneVoiceNomal) // ��x���Đ����Ă��Ȃ��ꍇ
            {
                if (MobSE.Length > 0) // ���ʉ��N���b�v������ꍇ
                {
                    int randomIndex = Random.Range(0, MobSE.Length); // �����_���ȃC���f�b�N�X��I��
                    audioSourceMattyo.clip = MobSE[randomIndex]; // �����_���ȉ����N���b�v��ݒ�
                    audioSourceMattyo.Play(); // �I�������������Đ�
                }
                //audioSource.loop = true; // ���[�v�ݒ�i�R�����g�A�E�g���j
                OneVoiceNomal = true; // �t���O�𗧂Ă�
                yield return new WaitForSeconds(audioSourceMattyo.clip.length); // �����̍Đ����I���܂ő҂�
                OneVoiceNomal = false; // �t���O�����Z�b�g
            }
        }
    }

    // ���u�̒ʏ퉹�����Đ����郁�\�b�h
    public void EnemyNomalVoiceOn()
    {
        StartCoroutine(PlaySoundAndWaitMob()); // �T�E���h���Đ�����R���[�`�����J�n
    }
}
