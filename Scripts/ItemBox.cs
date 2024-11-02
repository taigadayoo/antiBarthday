using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    GameObject breakEffect; // ��ꂽ�Ƃ��̃G�t�F�N�g
    [SerializeField]
    GameObject CakeItem; // �A�C�e���Ƃ��ďo������P�[�L
    [SerializeField]
    GameObject enemy; // �o������G�̃v���n�u

    GameManager gameManager; // �Q�[���}�l�[�W���[�ւ̎Q��

    // �{�b�N�X�̎�ނ��`����񋓌^
    private enum BoxName
    {
        WoodBox,       // �؂̃{�b�N�X
        NomalBox,      // �ʏ�̃{�b�N�X
        WoodNomalBox,  // �؂̒ʏ�{�b�N�X
        EnemyBox       // �G�̃{�b�N�X
    }

    [SerializeField]
    BoxName boxName; // ���݂̃{�b�N�X�̎��

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // �Q�[���}�l�[�W���[���擾
    }

    // Update is called once per frame
    void Update()
    {
        Enemyvanish(); // �G�̏������������s
    }

    // �G���S�ł����ꍇ�Ƀ{�b�N�X���������鏈��
    private void Enemyvanish()
    {
        if (gameManager.EnemyAllDead == true && (boxName == BoxName.NomalBox) || gameManager.EnemyAllDead == true && (boxName == BoxName.EnemyBox))
        {
            Destroy(this.gameObject); // �{�b�N�X������
        }
    }

    // �Փ˔���̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ʏ�̃{�b�N�X�ɏՓ˂����ꍇ
        if (boxName == BoxName.NomalBox)
        {
            if (collision.gameObject.tag == "RedCake" || collision.gameObject.tag == "Cake")
            {
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // ��ꂽ�G�t�F�N�g�𐶐�
                Instantiate(CakeItem, this.transform.position, this.transform.rotation); // �P�[�L�A�C�e���𐶐�
                Destroy(this.gameObject); // �{�b�N�X������
            }
        }

        // �؂̃{�b�N�X�ɏՓ˂����ꍇ
        if (boxName == BoxName.WoodBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18); // ���ʉ����Đ�
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // ��ꂽ�G�t�F�N�g�𐶐�
                Instantiate(CakeItem, this.transform.position, this.transform.rotation); // �P�[�L�A�C�e���𐶐�
                Destroy(this.gameObject); // �{�b�N�X������
            }
        }

        // �؂̒ʏ�{�b�N�X�ɏՓ˂����ꍇ
        if (boxName == BoxName.WoodNomalBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18); // ���ʉ����Đ�
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // ��ꂽ�G�t�F�N�g�𐶐�
                Destroy(this.gameObject); // �{�b�N�X������
            }
        }

        // �G�̃{�b�N�X�ɏՓ˂����ꍇ
        if (boxName == BoxName.EnemyBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18); // ���ʉ����Đ�
                Instantiate(breakEffect, this.transform.position, this.transform.rotation); // ��ꂽ�G�t�F�N�g�𐶐�
                Instantiate(enemy, this.transform.position, this.transform.rotation); // �G�𐶐�
                Destroy(this.gameObject); // �{�b�N�X������
            }
        }
    }
}
