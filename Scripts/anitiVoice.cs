using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anitiVoice : MonoBehaviour
{
    public AudioSource audioSourceAnti; // ���ʉ���AudioSource

    public bool OneVoiceAnti = false; // ���ʉ����Đ������ǂ����������t���O

    public AudioClip[] MobSE; // �����̃��u���ʉ��N���b�v���i�[����z��

    // �����_���Ȍ��ʉ����Đ����A�Đ����I���܂ő҂R���[�`��
    IEnumerator PlaySoundAndWaitMob()
    {
        // AudioSource�����݂��邩�m�F
        if (audioSourceAnti != null)
        {
            // ���ʉ����܂��Đ�����Ă��Ȃ��ꍇ�̂ݍĐ�
            if (!OneVoiceAnti)
            {
                // ���ʉ��̔z�񂪋�łȂ����m�F
                if (MobSE.Length > 0)
                {
                    int randomIndex = Random.Range(0, MobSE.Length); // �����_���ȃC���f�b�N�X��I��
                    audioSourceAnti.clip = MobSE[randomIndex]; // �����_���ȉ����N���b�v��I��
                    audioSourceAnti.Play(); // �I�������������Đ�
                }

                // ���ʉ��Đ����̃t���O�𗧂Ă�
                OneVoiceAnti = true;
                yield return new WaitForSeconds(audioSourceAnti.clip.length); // ���ʉ��̒����������ҋ@
                OneVoiceAnti = false; // �Đ��I����A�t���O�����Z�b�g
            }
        }
    }

    // �G�̌��ʉ��Đ����J�n����֐�
    public void EnemyAntiVoiceOn()
    {
        StartCoroutine(PlaySoundAndWaitMob()); // ���ʉ��Đ��̃R���[�`�����J�n
    }
}
