using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Endroll : MonoBehaviour
{
    public float duration = 40f; // �G���h���[���̊����܂ł̎���
    public float verticalOffset = 8000f; // ������Ɉړ��������
    private bool endRollComplete = false; // �G���h���[���������������ǂ����������t���O

    [SerializeField] private string sceneNameClear;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    [SerializeField]
    AudioSource audioSourceOme;
    [SerializeField]
    AudioSource audioSourceEva;

    PlayerController playerController;
    void Start()
    {
        audioSourceEva.Play();
        audioSourceOme.Play();
        playerController = GetComponent<PlayerController>();
        // �q�I�u�W�F�N�g�S�̂��擾���A�A�j���[�V������K�p����
        foreach (Transform child in transform)
        {
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                // DOTween���g���ăA�j���[�V�������쐬���A������Ɉړ�
                text.rectTransform.DOAnchorPosY(text.rectTransform.anchoredPosition.y + verticalOffset, duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => { endRollComplete = true; }); // �A�j���[�V�����������Ƀt���O�𗧂Ă�
            }
        }
    }

    // �G���h���[���������������ǂ������m�F���邽�߂̃��\�b�h
    public bool IsEndRollComplete()
    {
        return endRollComplete;
    }

    private void Update()
    {
        if (endRollComplete || Input.GetKeyDown(KeyCode.T) || playerController.IsTitlePressed)
        {
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
        }
    }
}

    
