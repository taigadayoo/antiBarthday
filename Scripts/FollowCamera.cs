using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))] // ���̃R���|�[�l���g��Camera�R���|�[�l���g���K�v�ł��邱�Ƃ�����
public class FollowCamera : MonoBehaviour
{
    GameObject playerObj; // �v���C���[�I�u�W�F�N�g�̎Q��
    Player player; // �v���C���[�̃X�N���v�g�̎Q��
    Transform playerTransform; // �v���C���[��Transform

    public float moveTime = 1.0f; // �J�������ړ�����̂ɂ����鎞��
    public bool OnCamera = false; // �J�������v���C���[�ɒǏ]���Ă��邩�ǂ����̃t���O
    private Vector3 initialPosition; // �J�����̏����ʒu
    private bool OneCamera = false; // �J�����̓��삪��x�����s���邩�𔻒肷��t���O

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player"); // "Player"�^�O�����I�u�W�F�N�g������
        player = playerObj.GetComponent<Player>(); // �v���C���[�R���|�[�l���g���擾
        playerTransform = playerObj.transform; // �v���C���[��Transform���擾

        initialPosition = transform.position; // �J�����̏����ʒu��ۑ�

        // �ړ���̖ڕW�ʒu���v���C���[�̈ʒu�ɒǏ]����悤�ɐݒ�
        Vector3 targetPosition = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
        StartCoroutine(MoveCamera(targetPosition)); // �J�����ړ��̃R���[�`�����J�n
    }

    void LateUpdate()
    {
        if (player != null && OnCamera == true)
        {
            // �J������ڕW�ʒu�Ɉێ�
            MoveCamera();
        }
        if (player != null && OnCamera == true && !OneCamera)
        {
            player.enabled = true; // �v���C���[�̓����L���ɂ���
            OneCamera = true; // �������x�����s�����߂̃t���O��ݒ�
        }
        if (OnCamera == false)
        {
            player.enabled = false; // �v���C���[�̓���𖳌��ɂ���
        }
    }

    void MoveCamera()
    {
        // �v���C���[�̈ʒu�ɉ����ăJ�����̈ʒu���X�V
        transform.position = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
    }

    IEnumerator MoveCamera(Vector3 destination)
    {
        Vector3 startPosition = transform.position; // ���݂̃J�����̈ʒu
        float elapsedTime = 0f; // �o�ߎ��Ԃ̏�����

        SampleSoundManager.Instance.PlaySe(SeType.SE13); // SE13���Đ�

        yield return new WaitForSeconds(5.5f); // 5.5�b�ҋ@

        SampleSoundManager.Instance.PlaySe(SeType.SE14); // SE14���Đ�

        // �J������ړI�n�܂ňړ�������
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, destination, (elapsedTime / moveTime)); // ���`���
            elapsedTime += Time.deltaTime; // �o�ߎ��Ԃ��X�V
            yield return null; // ���̃t���[���܂őҋ@
        }

        // �ړ�������������A�J�����̈ʒu��ڕW�ʒu�ɐݒ肷��
        transform.position = destination;

        OnCamera = true; // �J�������ړ��������Ƃ������t���O��ݒ�
    }
}
