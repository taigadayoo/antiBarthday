using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySE : MonoBehaviour
{
    public AudioClip RedSE; // ���[�h1�̌��ʉ�
    public AudioClip BlueSE; // ���[�h2�̌��ʉ�
    public AudioClip YellowSE;
    public AudioSource mode1AudioSource; // ���[�h1�p��AudioSource
    public AudioSource mode2AudioSource; // ���[�h2�p��AudioSource
    public AudioSource mode3AudioSource;

    private bool isMode1Playing = false; // ���[�h1��SE���Đ������ǂ����������t���O
    private bool isMode2Playing = false; // ���[�h2��SE���Đ������ǂ����������t���O
    private bool isMode3Playing = false;

    // ���[�h1�ɐ؂�ւ���
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

    // ���[�h2�ɐ؂�ւ���
    public void SwitchToMode2()
    {
        if (!isMode2Playing && mode2AudioSource != null && BlueSE != null)
        {
            // ���[�h1��SE���Đ����ł���Β�~
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
