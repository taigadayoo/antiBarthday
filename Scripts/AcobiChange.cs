using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcobiChange : MonoBehaviour
{
    public GameObject[] objectsToSwitch; // �؂�ւ���I�u�W�F�N�g�̔z��
    private int currentIndex = 0; // ���݂̃I�u�W�F�N�g�̃C���f�b�N�X

    PlayerController PlayerController;

    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        // �ŏ��̃I�u�W�F�N�g�݂̂�\������
        ShowObject(currentIndex);
    }

    void Update()
    {
        // �}�E�X�̍��N���b�N�������ꂽ�玟�̃I�u�W�F�N�g��\������
        if (Input.GetKeyDown(KeyCode.Space)|| PlayerController.IsGravityReversePressed)
        {
            currentIndex = (currentIndex + 1) % objectsToSwitch.Length;
            ShowObject(currentIndex);
        }
    }

    void ShowObject(int index)
    {
        // ���ׂẴI�u�W�F�N�g���\���ɂ���
        foreach (GameObject obj in objectsToSwitch)
        {
            obj.SetActive(false);
        }

        // �w�肳�ꂽ�C���f�b�N�X�̃I�u�W�F�N�g��\������
        objectsToSwitch[index].SetActive(true);
    }
}
