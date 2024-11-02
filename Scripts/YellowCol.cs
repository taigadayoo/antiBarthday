using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCol : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager; // �Q�[���}�l�[�W���[�̎Q��
    [SerializeField]
    GameObject deathEffect; // �v���C���[�����񂾂Ƃ��̃G�t�F�N�g
    [SerializeField]
    Player Player; // �v���C���[�̎Q��
    public bool Down = false; // �_�E����Ԃ̃t���O
    public bool OneDamage = false; // ��x�����_���[�W���󂯂�t���O

    [SerializeField] private string sceneNameClear; // �N���A��̃V�[����
    [SerializeField] private Color fadeColor; // �t�F�[�h���̐F
    [SerializeField] private float fadeSpeed; // �t�F�[�h�̑���

    private bool OnSave = false; // �Z�[�u�|�C���g1�̕ۑ����
    private bool OnSave2 = false; // �Z�[�u�|�C���g2�̕ۑ����

    PlayerAbility playerAbility; // �v���C���[�̔\�͊Ǘ��N���X
    Player player; // �v���C���[�I�u�W�F�N�g�̎Q��

    private void Start()
    {
        // �v���C���[�\�͂̎擾
        playerAbility = FindObjectOfType<PlayerAbility>();
        // �v���C���[�I�u�W�F�N�g�̎擾
        player = FindObjectOfType<Player>();
    }

    private void DamageDead()
    {
        // �v���C���[�����񂾂Ƃ��̏���
        gameManager.Dead();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O���m�F
        if (other.gameObject.tag == "Dead")
        {
            Player.enabled = false; // �v���C���[�̑���𖳌���
            SampleSoundManager.Instance.PlaySe(SeType.SE5); // ���S���̍Đ�
            gameManager.EnemyAllDead = true; // �G���S�Ď��S������Ԃɐݒ�
            playerAbility.enabled = false; // �v���C���[�̔\�͂𖳌���
            Invoke("DamageDead", 1.5f); // 1.5�b���DamageDead���\�b�h���Ăяo��
        }
        if (other.gameObject.tag == "CakeItem")
        {
            // �P�[�L�A�C�e�����擾�����ꍇ
            Player.bulletNum = Player.MaxCakeNum; // �e�����ő�ɐݒ�
            SampleSoundManager.Instance.PlaySe(SeType.SE6); // �A�C�e���擾���̍Đ�
            Destroy(other.gameObject); // �A�C�e����j��
        }
        if (other.gameObject.tag == "SavePoint")
        {
            // �Z�[�u�|�C���g1�ɐG�ꂽ�ꍇ
            Vector3 respawnPosition = other.transform.position; // �Z�[�u�|�C���g�̈ʒu���擾
            respawnPosition.y += 18; // respawn�ʒu����Ɉړ�
            gameManager.RespawnPoint = respawnPosition; // respawn�|�C���g��ݒ�
            if (OnSave == false) // ���߂ĐG�ꂽ�ꍇ
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // �Z�[�u���̍Đ�
                SampleSoundManager.Instance.PlaySe(SeType.SE17); // �Z�[�u���̍Đ�
            }
            OnSave = true; // �Z�[�u��Ԃ��X�V
        }
        if (other.gameObject.tag == "SavePoint2")
        {
            // �Z�[�u�|�C���g2�ɐG�ꂽ�ꍇ
            Vector3 respawnPosition = other.transform.position; // �Z�[�u�|�C���g�̈ʒu���擾
            respawnPosition.y += 18; // respawn�ʒu����Ɉړ�
            gameManager.RespawnPoint = respawnPosition; // respawn�|�C���g��ݒ�
            if (OnSave2 == false) // ���߂ĐG�ꂽ�ꍇ
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16); // �Z�[�u���̍Đ�
                SampleSoundManager.Instance.PlaySe(SeType.SE17); // �Z�[�u���̍Đ�
            }
            OnSave2 = true; // �Z�[�u��Ԃ��X�V
        }
        if (other.gameObject.tag == "Goal")
        {
            // �S�[���ɐG�ꂽ�ꍇ
            Player.enabled = false; // �v���C���[�̑���𖳌���
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed); // �t�F�[�h���J�n
            SampleSoundManager.Instance.StopBgm(); // BGM���~
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O���m�F
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "EnemyBullet")
        {
            // �_���[�W��G�̒e�ƏՓ˂����ꍇ
            Instantiate(deathEffect, collision.transform.position, collision.transform.rotation); // ���S�G�t�F�N�g�̐���
            playerAbility.ability = PlayerAbility.Ability.nomal; // �v���C���[�̔\�͂��m�[�}���ɖ߂�
            playerAbility.YellowOffSwitch = true; // �C�G���[�\�͂̃I�t�X�C�b�`
            playerAbility.StartYellowAbilityCooldown(); // �C�G���[�\�͂̃N�[���_�E�����J�n
            playerAbility.lastTrueTime = Time.time; // �Ō�ɔ\�͂��L�����������Ԃ��L�^
            Destroy(collision.gameObject); // �Փ˂����I�u�W�F�N�g��j��
            playerAbility.YellowOn = false; // �C�G���[�\�͂��I�t�ɂ���
            playerAbility.nomalOn = true; // �m�[�}���\�͂��I���ɂ���
            player.ActivateInvincibility(); // �v���C���[�̖��G��Ԃ��A�N�e�B�u��
        }
        if (collision.gameObject.tag == "DamageObject")
        {
            // �_���[�W�I�u�W�F�N�g�ƏՓ˂����ꍇ
            playerAbility.YellowOffSwitch = true; // �C�G���[�\�͂̃I�t�X�C�b�`
            playerAbility.StartYellowAbilityCooldown(); // �C�G���[�\�͂̃N�[���_�E�����J�n
            playerAbility.lastTrueTime = Time.time; // �Ō�ɔ\�͂��L�����������Ԃ��L�^
            playerAbility.ability = PlayerAbility.Ability.nomal; // �v���C���[�̔\�͂��m�[�}���ɖ߂�
            player.ActivateInvincibility(); // �v���C���[�̖��G��Ԃ��A�N�e�B�u��
            playerAbility.nomalOn = true; // �m�[�}���\�͂��I���ɂ���
            playerAbility.YellowOn = false; // �C�G���[�\�͂��I�t�ɂ���
        }
    }
}
