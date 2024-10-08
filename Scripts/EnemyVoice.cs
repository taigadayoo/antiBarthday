using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoice : MonoBehaviour
{
    public AudioClip BirdSE;
    public AudioClip MobSE;
    public AudioClip AntiSE;
    public AudioSource audioSource;
    public AudioSource audioSourceMattyo;
    public AudioSource audioSourceAnti;


    public bool OneVoice = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BirdVoiceNomal()
    {
        StartCoroutine(PlaySoundAndWait());
    }
    IEnumerator PlaySoundAndWait()
    {
        if (!OneVoice)
        {
            audioSource.clip = BirdSE;
            audioSource.Play();
            //audioSource.loop = true;
            OneVoice = true;
            yield return new WaitForSeconds(audioSource.clip.length);
            OneVoice = false;
        }
    }
    public void BirdDeath()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE12);
    }
    public void OneVoiceReset()
    {
        OneVoice = false;
    }
}
