using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsobiBotan : MonoBehaviour
{
    // �V�[�����i�N���A�V�[���ƃ^�C�g���V�[���j��ݒ肷�邽�߂̕ϐ�
    [SerializeField] private string sceneNameClear; // �N���A�V�[���̖��O
    [SerializeField] private string sceneNameTitle; // �^�C�g���V�[���̖��O
    [SerializeField] private Color fadeColor; // �t�F�[�h���̐F
    [SerializeField] private float fadeSpeed; // �t�F�[�h�̑��x

    AudioSource audioSource; // ���ʉ��Đ��p��AudioSource
    enum Scene
    {
        Asobi, // �V�уV�[��
        Title  // �^�C�g���V�[��
    }

    [SerializeField]
    Scene scene; // ���݂̃V�[���̏��

    PlayerController playerController; // �v���C���[�̃R���g���[���[

    // ����������
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g�̎擾
        playerController = GetComponent<PlayerController>(); // PlayerController�R���|�[�l���g�̎擾
    }

    // ���t���[���X�V����
    void Update()
    {
        // �v���C���[���W�����v�{�^���������A�����݂̃V�[�����V�уV�[���̏ꍇ�A�^�C�g���V�[���֑J��
        if (playerController.IsJumpPressed && scene == Scene.Asobi)
        {
            OnTitle();
        }
        // �v���C���[�����Z�b�g�{�^���������A�����݂̃V�[�����^�C�g���V�[���̏ꍇ�A�V�уV�[���֑J��
        if (playerController.IsResetPressed && scene == Scene.Title)
        {
            OnAsobi();
        }
    }

    // �V�уV�[���֑J�ڂ��鏈��
    public void OnAsobi()
    {
        audioSource.Play(); // ���ʉ����Đ�
        Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed); // �N���A�V�[���ւ̃t�F�[�h�J��
    }

    // �^�C�g���V�[���֑J�ڂ��鏈��
    public void OnTitle()
    {
        audioSource.Play(); // ���ʉ����Đ�
        Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed); // �^�C�g���V�[���ւ̃t�F�[�h�J��
    }
}
