using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyVoice : MonoBehaviour
{
    public AudioClip[] audioClips; // �����N���b�v�̔z��

    private AudioSource audioSource;

    GameManager gameManager;
    void Start()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");

        // GameManager��AudioSource�R���|�[�l���g���擾����
        if (gameManagerObject != null)
        {
            audioSource= gameManagerObject.GetComponent<AudioSource>();


        }
    }

        public void PlayRandomSound()
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length); // �����_���ȃC���f�b�N�X��I��
            audioSource.clip = audioClips[randomIndex]; // �����_���ȉ����N���b�v��I��
            audioSource.Play(); // �I�������������Đ�
        
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to play.");
        }

    }
    public void PlayRandomSoundExternal()
    {
        PlayRandomSound();
    }
}
