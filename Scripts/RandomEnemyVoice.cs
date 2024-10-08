using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyVoice : MonoBehaviour
{
    public AudioClip[] audioClips; // 音声クリップの配列

    private AudioSource audioSource;

    GameManager gameManager;
    void Start()
    {
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");

        // GameManagerのAudioSourceコンポーネントを取得する
        if (gameManagerObject != null)
        {
            audioSource= gameManagerObject.GetComponent<AudioSource>();


        }
    }

        public void PlayRandomSound()
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length); // ランダムなインデックスを選択
            audioSource.clip = audioClips[randomIndex]; // ランダムな音声クリップを選択
            audioSource.Play(); // 選択した音声を再生
        
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
