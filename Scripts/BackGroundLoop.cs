using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public GameObject backgroundPrefab; // ��������w�i�̃v���n�u
    public float backgroundWidth; // �w�i�̕�
    public int numberOfBackgrounds = 3; // �\������w�i�̐�
    public Transform cameraTransform; // �J�����̃g�����X�t�H�[��

    private GameObject[] backgrounds; // �w�i�̔z��
    private float backgroundStartX; // �w�i�̊J�nX���W
    private float backgroundEndX; // �w�i�̏I��X���W

    // ����������
    void Start()
    {
        backgrounds = new GameObject[numberOfBackgrounds]; // �w�i�z��̏�����
        backgroundStartX = cameraTransform.position.x - (backgroundWidth * numberOfBackgrounds / 2f); // �w�i�̊J�n�ʒu���v�Z
        backgroundEndX = cameraTransform.position.x + (backgroundWidth * numberOfBackgrounds / 2f); // �w�i�̏I���ʒu���v�Z

        // �w�i�I�u�W�F�N�g�𐶐����Ĕz�u
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            GameObject background = Instantiate(backgroundPrefab, new Vector3(backgroundStartX + i * backgroundWidth, transform.position.y, transform.position.z), Quaternion.identity);
            backgrounds[i] = background;
        }
    }

    // ���t���[���X�V����
    void Update()
    {
        // �J�����̈ʒu���w�i�̏I���ʒu���E�ɍs������A�ł����ɂ���w�i���E�[�Ɉړ�������
        if (cameraTransform.position.x > backgroundEndX)
        {
            MoveBackgroundRight();
        }

        // �J�����̈ʒu���w�i�̊J�n�ʒu��荶�ɍs������A�ł��E�ɂ���w�i�����[�Ɉړ�������
        if (cameraTransform.position.x < backgroundStartX)
        {
            MoveBackgroundLeft();
        }
    }

    // �w�i���E�Ƀ��[�v�ړ�������
    void MoveBackgroundRight()
    {
        // �ł����̔w�i���E�[�Ɉړ�
        backgrounds[0].transform.position = new Vector3(backgrounds[backgrounds.Length - 1].transform.position.x + backgroundWidth, backgrounds[0].transform.position.y, backgrounds[0].transform.position.z);

        // �z����X�V���Ĕw�i�̏������ێ�
        GameObject temp = backgrounds[0];
        for (int i = 1; i < backgrounds.Length; i++)
        {
            backgrounds[i - 1] = backgrounds[i];
        }
        backgrounds[backgrounds.Length - 1] = temp;

        // �w�i�̊J�n�ʒu�ƏI���ʒu���X�V
        backgroundStartX += backgroundWidth;
        backgroundEndX += backgroundWidth;
    }

    // �w�i�����Ƀ��[�v�ړ�������
    void MoveBackgroundLeft()
    {
        // �ł��E�̔w�i�����[�Ɉړ�
        backgrounds[backgrounds.Length - 1].transform.position = new Vector3(backgrounds[0].transform.position.x - backgroundWidth, backgrounds[backgrounds.Length - 1].transform.position.y, backgrounds[backgrounds.Length - 1].transform.position.z);

        // �z����X�V���Ĕw�i�̏������ێ�
        GameObject temp = backgrounds[backgrounds.Length - 1];
        for (int i = backgrounds.Length - 1; i > 0; i--)
        {
            backgrounds[i] = backgrounds[i - 1];
        }
        backgrounds[0] = temp;

        // �w�i�̊J�n�ʒu�ƏI���ʒu���X�V
        backgroundStartX -= backgroundWidth;
        backgroundEndX -= backgroundWidth;
    }
}
