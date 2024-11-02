using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySE : MonoBehaviour
{
    public AudioClip RedSE; // ���ヂ�[�h�̌��ʉ�
    public AudioClip BlueSE; // ���L�у��[�h�̌��ʉ�
    public AudioClip YellowSE; // �ϋv���[�h�̌��ʉ�
    public AudioSource mode1AudioSource; // ���ヂ�[�h�p��AudioSource
    public AudioSource mode2AudioSource; // ���L�у��[�h�p��AudioSource
    public AudioSource mode3AudioSource; // �ϋv���[�h�p��AudioSource

    private bool isMode1Playing = false; // ���ヂ�[�h��SE���Đ������ǂ����������t���O
    private bool isMode2Playing = false; // ���L�у��[�h��SE���Đ������ǂ����������t���O
    private bool isMode3Playing = false; // �ϋv���[�h��SE���Đ������ǂ����������t���O

    // ���ヂ�[�h�ɐ؂�ւ���
    public void SwitchToMode1()
    {
        if (!isMode1Playing && mode1AudioSource != null && RedSE != null)
        {
            // ���L�у��[�h���Đ����ł���Β�~
            if (isMode2Playing)
            {
                mode2AudioSource.Stop(); // ���L�у��[�h�̌��ʉ����~
                isMode2Playing = false; // ���L�у��[�h�̃t���O�����Z�b�g
            }
            // �ϋv���[�h���Đ����ł���Β�~
            if (isMode3Playing)
            {
                mode3AudioSource.Stop(); // �ϋv���[�h�̌��ʉ����~
                isMode3Playing = false; // �ϋv���[�h�̃t���O�����Z�b�g
            }
            mode1AudioSource.clip = RedSE; // ���ヂ�[�h�p�̌��ʉ���ݒ�
            mode1AudioSource.Play(); // ���ヂ�[�h�̌��ʉ����Đ�
            isMode1Playing = true; // ���ヂ�[�h�Đ����t���O��ݒ�
        }
    }

    // ���L�у��[�h�ɐ؂�ւ���
    public void SwitchToMode2()
    {
        if (!isMode2Playing && mode2AudioSource != null && BlueSE != null)
        {
            // ���ヂ�[�h���Đ����ł���Β�~
            if (isMode1Playing)
            {
                mode1AudioSource.Stop(); // ���ヂ�[�h�̌��ʉ����~
                isMode1Playing = false; // ���ヂ�[�h�̃t���O�����Z�b�g
            }
            // �ϋv���[�h���Đ����ł���Β�~
            if (isMode3Playing)
            {
                mode3AudioSource.Stop(); // �ϋv���[�h�̌��ʉ����~
                isMode3Playing = false; // �ϋv���[�h�̃t���O�����Z�b�g
            }

            mode2AudioSource.clip = BlueSE; // ���L�у��[�h�p�̌��ʉ���ݒ�
            mode2AudioSource.Play(); // ���L�у��[�h�̌��ʉ����Đ�
            isMode2Playing = true; // ���L�у��[�h�Đ����t���O��ݒ�
        }
    }

    // �ϋv���[�h�ɐ؂�ւ���
    public void SwitchToMode3()
    {
        if (!isMode3Playing && mode3AudioSource != null && YellowSE != null)
        {
            // ���ヂ�[�h���Đ����ł���Β�~
            if (isMode1Playing)
            {
                mode1AudioSource.Stop(); // ���ヂ�[�h�̌��ʉ����~
                isMode1Playing = false; // ���ヂ�[�h�̃t���O�����Z�b�g
            }
            // ���L�у��[�h���Đ����ł���Β�~
            if (isMode2Playing)
            {
                mode2AudioSource.Stop(); // ���L�у��[�h�̌��ʉ����~
                isMode2Playing = false; // ���L�у��[�h�̃t���O�����Z�b�g
            }

            mode3AudioSource.clip = YellowSE; // �ϋv���[�h�p�̌��ʉ���ݒ�
            mode3AudioSource.Play(); // �ϋv���[�h�̌��ʉ����Đ�
            isMode3Playing = true; // �ϋv���[�h�Đ����t���O��ݒ�
        }
    }
}
