using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySE : MonoBehaviour
{
    public AudioClip RedSE; // 炎上モードの効果音
    public AudioClip BlueSE; // 爆伸びモードの効果音
    public AudioClip YellowSE; // 耐久モードの効果音
    public AudioSource mode1AudioSource; // 炎上モード用のAudioSource
    public AudioSource mode2AudioSource; // 爆伸びモード用のAudioSource
    public AudioSource mode3AudioSource; // 耐久モード用のAudioSource

    private bool isMode1Playing = false; // 炎上モードのSEが再生中かどうかを示すフラグ
    private bool isMode2Playing = false; // 爆伸びモードのSEが再生中かどうかを示すフラグ
    private bool isMode3Playing = false; // 耐久モードのSEが再生中かどうかを示すフラグ

    // 炎上モードに切り替える
    public void SwitchToMode1()
    {
        if (!isMode1Playing && mode1AudioSource != null && RedSE != null)
        {
            // 爆伸びモードが再生中であれば停止
            if (isMode2Playing)
            {
                mode2AudioSource.Stop(); // 爆伸びモードの効果音を停止
                isMode2Playing = false; // 爆伸びモードのフラグをリセット
            }
            // 耐久モードが再生中であれば停止
            if (isMode3Playing)
            {
                mode3AudioSource.Stop(); // 耐久モードの効果音を停止
                isMode3Playing = false; // 耐久モードのフラグをリセット
            }
            mode1AudioSource.clip = RedSE; // 炎上モード用の効果音を設定
            mode1AudioSource.Play(); // 炎上モードの効果音を再生
            isMode1Playing = true; // 炎上モード再生中フラグを設定
        }
    }

    // 爆伸びモードに切り替える
    public void SwitchToMode2()
    {
        if (!isMode2Playing && mode2AudioSource != null && BlueSE != null)
        {
            // 炎上モードが再生中であれば停止
            if (isMode1Playing)
            {
                mode1AudioSource.Stop(); // 炎上モードの効果音を停止
                isMode1Playing = false; // 炎上モードのフラグをリセット
            }
            // 耐久モードが再生中であれば停止
            if (isMode3Playing)
            {
                mode3AudioSource.Stop(); // 耐久モードの効果音を停止
                isMode3Playing = false; // 耐久モードのフラグをリセット
            }

            mode2AudioSource.clip = BlueSE; // 爆伸びモード用の効果音を設定
            mode2AudioSource.Play(); // 爆伸びモードの効果音を再生
            isMode2Playing = true; // 爆伸びモード再生中フラグを設定
        }
    }

    // 耐久モードに切り替える
    public void SwitchToMode3()
    {
        if (!isMode3Playing && mode3AudioSource != null && YellowSE != null)
        {
            // 炎上モードが再生中であれば停止
            if (isMode1Playing)
            {
                mode1AudioSource.Stop(); // 炎上モードの効果音を停止
                isMode1Playing = false; // 炎上モードのフラグをリセット
            }
            // 爆伸びモードが再生中であれば停止
            if (isMode2Playing)
            {
                mode2AudioSource.Stop(); // 爆伸びモードの効果音を停止
                isMode2Playing = false; // 爆伸びモードのフラグをリセット
            }

            mode3AudioSource.clip = YellowSE; // 耐久モード用の効果音を設定
            mode3AudioSource.Play(); // 耐久モードの効果音を再生
            isMode3Playing = true; // 耐久モード再生中フラグを設定
        }
    }
}
