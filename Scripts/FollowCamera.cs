using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    Transform playerTransform;

    public float moveTime = 1.0f; // �ړ��ɂ����鎞��
    public bool OnCamera = false;
    private Vector3 initialPosition; // �J�����̏����ʒu
    private bool OneCamera = false;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        playerTransform = playerObj.transform;

        initialPosition = transform.position;

        // �ړ���̖ڕW�ʒu���v���C���[�̈ʒu�ɒǏ]����悤�ɐݒ�
        Vector3 targetPosition = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
        StartCoroutine(MoveCamera(targetPosition));
    }

    void LateUpdate()
    {
        if (player != null && OnCamera == true )
        {
            // �ړ���������������J������ڕW�ʒu�Ɉێ�
            MoveCamera();
           
        }
        if (player != null && OnCamera == true && !OneCamera)
        {
            player.enabled = true;
            OneCamera = true;
        }
            if (OnCamera == false)
        {
            player.enabled = false;
        }
       
    }

    void MoveCamera()
    {
        transform.position = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
    }

    IEnumerator MoveCamera(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        SampleSoundManager.Instance.PlaySe(SeType.SE13);

        yield return new WaitForSeconds(5.5f);

        SampleSoundManager.Instance.PlaySe(SeType.SE14);

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, destination, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ړ�������������A�J�����̈ʒu��ڕW�ʒu�ɐݒ肷��
        transform.position = destination;
        
        OnCamera = true;

       
    }
}