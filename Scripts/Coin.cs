using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    GameObject itemEffect; // �R�C���擾���ɐ�������G�t�F�N�g

    // �Փ˔��菈��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Body�܂���YellowBody�^�O�̃I�u�W�F�N�g�ƏՓ˂����ۂ̏���
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody")
        {
            Instantiate(itemEffect, this.transform.position, this.transform.rotation); // �G�t�F�N�g�𐶐�
            Destroy(this.gameObject); // �R�C�����폜
            SampleSoundManager.Instance.PlaySe(SeType.SE10); // �R�C���擾�����Đ�
        }
    }
}
