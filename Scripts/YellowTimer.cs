using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill; // �^�C�}�[UI�̓h��Ԃ��摜
    [SerializeField]
    PlayerAbility playerAbility; // �v���C���[�̔\�͂��Ǘ�����N���X�ւ̎Q��

    private float currentTime = 0f; // ���݂̃^�C�}�[�̎���

    void Start()
    {
        // �������������K�v�ȏꍇ�ɂ����ɋL�q
    }

    private void UpdateTimerUI()
    {
        // �^�C�}�[UI�̍X�V
        if (uiFill != null)
        {
            float elapsedTime = playerAbility.YellowCoolTime - currentTime; // �o�ߎ��Ԃ��v�Z
            float fillAmount = elapsedTime / playerAbility.YellowCoolTime; // �h��Ԃ��������v�Z
            uiFill.fillAmount = Mathf.Clamp01(fillAmount); // �h��Ԃ�������0����1�͈̔͂ɐ���
        }
    }

    public void StartTimer(float cooldown)
    {
        // �^�C�}�[�̊J�n
        playerAbility.YellowCoolTime = cooldown; // �N�[���_�E�����Ԃ�ݒ�
        currentTime = playerAbility.YellowCoolTime; // ���݂̎��Ԃ��N�[���_�E�����Ԃɐݒ�
        UpdateTimerUI(); // UI���X�V
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[���I�t�X�C�b�`�̏�Ԃł���ꍇ�Ƀ^�C�}�[���X�V
        if (playerAbility.YellowOffSwitch)
        {
            currentTime -= Time.deltaTime; // �o�ߎ��Ԃ�����
            if (currentTime <= 0f)
            {
                currentTime = 0f; // �^�C�}�[��0�ȉ��ɂȂ�Ȃ��悤�ɐݒ�
            }
            UpdateTimerUI(); // UI���X�V
        }
    }
}
