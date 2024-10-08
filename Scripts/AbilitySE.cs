using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySE : MonoBehaviour
{
    public AudioClip RedSE; // モード1の効果音
    public AudioClip BlueSE; // モード2の効果音
    public AudioClip YellowSE;
    public AudioSource mode1AudioSource; // モード1用のAudioSource
    public AudioSource mode2AudioSource; // モード2用のAudioSource
    public AudioSource mode3AudioSource;

    private bool isMode1Playing = false; // モード1のSEが再生中かどうかを示すフラグ
    private bool isMode2Playing = false; // モード2のSEが再生中かどうかを示すフラグ
    private bool isMode3Playing = false;

    // モード1に切り替える
    public void SwitchToMode1()
    {
        if (!isMode1Playing && mode1AudioSource != null && RedSE != null)
        {
          
            if (isMode2Playing)
            {
                mode2AudioSource.Stop();
                isMode2Playing = false;
            }
            if (isMode3Playing)
            {
                mode3AudioSource.Stop();
                isMode3Playing = false;
            }
            mode1AudioSource.clip = RedSE;
            mode1AudioSource.Play();
            isMode1Playing = true;
        }
    }

    // モード2に切り替える
    public void SwitchToMode2()
    {
        if (!isMode2Playing && mode2AudioSource != null && BlueSE != null)
        {
            // モード1のSEが再生中であれば停止
            if (isMode1Playing)
            {
                mode1AudioSource.Stop();
                isMode1Playing = false;
            }
            if (isMode3Playing)
            {
                mode3AudioSource.Stop();
                isMode3Playing = false;
            }

            mode2AudioSource.clip = BlueSE;
            mode2AudioSource.Play();
            isMode2Playing = true;
        }
    }
    public void SwitchToMode3()
    {
        if (!isMode3Playing && mode3AudioSource != null && YellowSE != null)
        {
           
            if (isMode1Playing)
            {
                mode1AudioSource.Stop();
                isMode1Playing = false;
            }
            if (isMode2Playing)
            {
                mode2AudioSource.Stop();
                isMode2Playing = false;
            }

            mode3AudioSource.clip = YellowSE;
            mode3AudioSource.Play();
            isMode3Playing = true;
        }
    }
}
