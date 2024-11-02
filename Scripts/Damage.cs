using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager; // �Q�[���̏�Ԃ��Ǘ�����GameManager
    [SerializeField]
    Player Player; // �v���C���[�I�u�W�F�N�g
    public bool Down = false; // �v���C���[���_�E�����Ă��邩�ǂ���
    public bool OneDamage = false; // ��x�����_���[�W���󂯂����ǂ���
    [SerializeField] private string sceneNameClear; // �V�[���N���A���̑J�ڐ�
    [SerializeField] private Color fadeColor; // �t�F�[�h�A�E�g���̐F
    [SerializeField] private float fadeSpeed; // �t�F�[�h�A�E�g�̑��x

    private bool OnSave = false; // �Z�[�u�|�C���g1��ۑ��������ǂ���
    private bool OnSave2 = false; // �Z�[�u�|�C���g2��ۑ��������ǂ���

    public bool isHit = false; // �v���C���[���_���[�W���󂯂����ǂ���

    PlayerAbility playerAbility; // �v���C���[�̔\�͂��Ǘ�����X�N���v�g

    private void Start()
    {
        playerAbility = FindObjectOfType<PlayerAbility>(); // PlayerAbility�X�N���v�g�̎擾
    }

    // �v���C���[�����S�����Ƃ��̏���
    private void DamageDead()
    {
        gameManager.Dead(); // �Q�[���}�l�[�W���[�Ɏ��S������ʒm
        isHit = false; // �_���[�W�󂯂��t���O�����Z�b�g
    }

    // �g���K�[�ɓ������Ƃ��̏���
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���S����
        if (other.gameObject.tag == "Dead" && !isHit)
        {
            playerAbility.YellowOffSwitch = false; // �v���C���[�̃X�C�b�`�I�t
            Player.enabled = false; // �v���C���[�̓����𖳌���
            SampleSoundManager.Instance.PlaySe(SeType.SE5); // ���S�����Đ�
            gameManager.EnemyAllDead = true; // �G�S�Ńt���O�𗧂Ă�
            playerAbility.enabled = false; // �v���C���[�̔\�͂𖳌���
            Invoke("DamageDead", 1.5f); // 1.5�b��Ɏ��S���������s
            this.enabled = false; // ���̃X�N���v�g�𖳌���
            isHit = true; // �_���[�W���󂯂��t���O�𗧂Ă�
        }

        // �P�[�L�A�C�e���擾����
        if (other.gameObject.tag == "CakeItem")
        {
            Player.bulletNum = Player.MaxCakeNum; // �v���C���[�̒e�����ő��
            SampleSoundManager.Instance.PlaySe(SeType.SE6); // �A�C�e���擾�����Đ�
            Destroy(other.gameObject); // �P�[�L�A�C�e�����폜
        }

        // �Z�[�u�|�C���g1����
        if (other.gameObject.tag == "SavePoint")
        {
            Vector3 respawnPosition = other.transform.position; // ���X�|�[���ʒu�̎擾
            respawnPosition.y += 18; // ���X�|�[���ʒu����Ɉړ�
            gameManager.RespawnPoint = respawnPosition; // �Q�[���}�l�[�W���[�Ƀ��X�|�[���ʒu��ݒ�
            if (OnSave == false)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // �Z�[�u�|�C���g���B��
                SampleSoundManager.Instance.PlaySe(SeType.SE17);
            }
            OnSave = true; // �Z�[�u�|�C���g���B�t���O�𗧂Ă�
        }

        // �Z�[�u�|�C���g2����
        if (other.gameObject.tag == "SavePoint2")
        {
            Vector3 respawnPosition = other.transform.position; // ���X�|�[���ʒu�̎擾
            respawnPosition.y += 18; // ���X�|�[���ʒu����Ɉړ�
            gameManager.RespawnPoint = respawnPosition; // �Q�[���}�l�[�W���[�Ƀ��X�|�[���ʒu��ݒ�
            if (OnSave2 == false)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // �Z�[�u�|�C���g���B��
                SampleSoundManager.Instance.PlaySe(SeType.SE17);
            }
            OnSave2 = true; // �Z�[�u�|�C���g���B�t���O�𗧂Ă�
        }

        // �S�[������
        if (other.gameObject.tag == "Goal")
        {
            Player.enabled = false; // �v���C���[�̓����𖳌���
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed); // �V�[���J�ڏ���
            SampleSoundManager.Instance.StopBgm(); // BGM��~
        }
    }

    // �Փ˔��菈��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �_���[�W���󂯂��ꍇ�̏���
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "DamageObject" && !isHit)
        {
            isHit = true; // �_���[�W���󂯂��t���O�𗧂Ă�
            this.enabled = false; // ���̃X�N���v�g�𖳌���
            SampleSoundManager.Instance.PlaySe(SeType.SE2); // �_���[�W�����Đ�
            Down = true; // �v���C���[���_�E�������t���O�𗧂Ă�
            Player.enabled = false; // �v���C���[�̓����𖳌���
            gameManager.EnemyAllDead = true; // �G�S�Ńt���O�𗧂Ă�
            playerAbility.enabled = false; // �v���C���[�̔\�͂𖳌���
            Player.animation.SetBool("Dead", true); // �v���C���[�̃A�j���[�V���������S�ɐݒ�
            if (OneDamage == false)
            {
                Player.OnDead(); // �v���C���[�̎��S���������s
                OneDamage = true; // ��x�����_���[�W���󂯂��t���O�𗧂Ă�
                Invoke("DamageDead", 1.5f); // 1.5�b��Ɏ��S���������s
            }
        }
    }
}
