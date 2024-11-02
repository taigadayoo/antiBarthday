using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoice : MonoBehaviour
{
    public AudioClip BirdSE; // ���̌��ʉ�
    public AudioClip MobSE; // ���u�̌��ʉ�
    public AudioClip AntiSE; // �A���`�̌��ʉ�
    public AudioSource audioSource; // ���̉������Đ�����AudioSource
    public AudioSource audioSourceMattyo; // ���u�̉������Đ�����AudioSource
    public AudioSource audioSourceAnti; // �A���`�̉������Đ�����AudioSource

    public bool OneVoice = false; // ��x�����������Đ�����t���O

    // �ʏ�̒��̐����Đ����郁�\�b�h
    public void BirdVoiceNomal()
    {
        StartCoroutine(PlaySoundAndWait()); // �T�E���h���Đ�����R���[�`�����J�n
    }

    // �������Đ����đҋ@����R���[�`��
    IEnumerator PlaySoundAndWait()
    {
        if (!OneVoice) // ��x���Đ����Ă��Ȃ��ꍇ
        {
            audioSource.clip = BirdSE; // ���̌��ʉ���ݒ�
            audioSource.Play(); // �������Đ�
            //audioSource.loop = true; // ���[�v�ݒ�i�R�����g�A�E�g���j
            OneVoice = true; // �t���O�𗧂Ă�
            yield return new WaitForSeconds(audioSource.clip.length); // �����̍Đ����I���܂ő҂�
            OneVoice = false; // �t���O�����Z�b�g
        }
    }

    // �������񂾂Ƃ��̌��ʉ����Đ����郁�\�b�h
    public void BirdDeath()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE12); // ���S���ʉ����Đ�
    }

    // �����Đ��t���O�����Z�b�g���郁�\�b�h
    public void OneVoiceReset()
    {
        OneVoice = false; // �t���O�����Z�b�g
    }
}
