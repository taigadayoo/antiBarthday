using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerLife = 3; // �v���C���[�̃��C�t
    [SerializeField]
    GameObject Player; // �v���C���[�̃Q�[���I�u�W�F�N�g
    [SerializeField]
    public Vector3 RespawnPoint; // ���X�|�[���|�C���g
    [SerializeField] private string sceneName; // �Q�[���I�[�o�[���ɑJ�ڂ���V�[����
    [SerializeField] private string sceneNameTitle; // �^�C�g���V�[����
    [SerializeField] private Color fadeColor; // �t�F�[�h�̐F
    [SerializeField] private float fadeSpeed; // �t�F�[�h�̑��x
    [SerializeField] Player player; // �v���C���[�̃X�N���v�g
    [SerializeField]
    Damage damage; // �_���[�W�����p�̃X�N���v�g
    [SerializeField]
    SpawnManager spawnManager; // �X�|�[���}�l�[�W���[�̎Q��
    public bool EnemyAllDead = false; // �G���S�Ď���ł��邩�ǂ����̃t���O

    private Transform playerTransform; // �v���C���[��Transform
    private Transform enemyTransform; // �G��Transform
    public Vector3 playerPosition; // �v���C���[�̈ʒu
    public Vector3 enemyPosition; // �G�̈ʒu
    public bool OnLeft = false; // �v���C���[�ƓG�̈ʒu�֌W�𔻒肷��t���O

    private bool OnBGM = false; // BGM���Đ������ǂ����̃t���O
    PlayerAbility playerAbility; // �v���C���[�̔\�͂��Ǘ�����X�N���v�g

    [SerializeField]
    PlayerController playerController; // �v���C���[�̑�����Ǘ�����X�N���v�g

    FollowCamera followCamera; // �J�����̒Ǐ]���Ǘ�����X�N���v�g

    private void Awake()
    {
        followCamera = FindObjectOfType<FollowCamera>(); // FollowCamera�X�N���v�g��T���Ď擾
    }

    void Start()
    {
        // �J�������Ǐ]���Ă���ꍇ��BGM���Đ�
        if (followCamera.OnCamera)
        {
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // �v���C���[��Transform���擾
        enemyTransform = GameObject.Find("EnemyThrow").transform; // �G��Transform���擾
        playerAbility = FindObjectOfType<PlayerAbility>(); // PlayerAbility�X�N���v�g���擾
    }

    void Update()
    {
        // �J�������Ǐ]���Ă��āABGM���Đ����łȂ��ꍇ��BGM���Đ�
        if (followCamera.OnCamera && OnBGM == false)
        {
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
            OnBGM = true; // BGM���Đ����̃t���O��ݒ�
        }

        GameOver(); // �Q�[���I�[�o�[�̔���
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position; // �v���C���[�̈ʒu���X�V
        }
        if (enemyTransform != null)
        {
            enemyPosition = enemyTransform.position; // �G�̈ʒu���X�V
        }
        LeftorRight(); // �v���C���[�ƓG�̈ʒu�֌W�𔻒�

        // Escape�L�[�܂��̓��Z�b�g�{�^���������ꂽ�ꍇ�Ƀ^�C�g���V�[���ɖ߂�
        if (Input.GetKeyDown(KeyCode.Escape) || playerController.IsResetPressed)
        {
            Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed); // �t�F�[�h���J�n
            SampleSoundManager.Instance.StopBgm(); // BGM���~
        }
    }

    public void Dead()
    {
        PlayerLife -= 1; // �v���C���[�̃��C�t������
        if (PlayerLife != 0)
        {
            damage.enabled = true; // �_���[�W������L���ɂ���
            playerAbility.YellowOffSwitch = false; // �v���C���[�̔\�͂����Z�b�g
            playerAbility.lastTrueTime = Time.time; // �Ō�̐��������Ԃ��L�^
            player.OffDead(); // �v���C���[�̎��Ɋւ��鏈��
            player.enabled = true; // �v���C���[���ēx�L���ɂ���
            Player.transform.position = RespawnPoint; // �v���C���[�����X�|�[���|�C���g�Ɉړ�
            player.bulletNum = player.MaxCakeNum; // �e�̐����ő�ɐݒ�
            player.OffJump(); // �W�����v�𖳌��ɂ���
            player.OffThrow(); // �����𖳌��ɂ���
            damage.OneDamage = false; // ��x�̃_���[�W�����Z�b�g
            EnemyAllDead = false; // �G�S�ł̃t���O�����Z�b�g
            spawnManager.RespawnAll(); // �G��S�ă��X�|�[��������
            playerAbility.enabled = true; // �v���C���[�̔\�͂�L���ɂ���
            playerAbility.NomalMode(); // �ʏ탂�[�h�ɐݒ�
        }
        else if (PlayerLife == 0)
        {
            Destroy(Player); // �v���C���[�����S�����ꍇ�̓I�u�W�F�N�g���폜
        }
    }

    private void GameOver()
    {
        if (PlayerLife == 0)
        {
            Initiate.Fade(sceneName, fadeColor, fadeSpeed); // �Q�[���I�[�o�[���Ƀt�F�[�h���J�n
            SampleSoundManager.Instance.StopBgm(); // BGM���~
        }
    }

    private void LeftorRight()
    {
        // �G�̈ʒu���v���C���[�̈ʒu���E�ɂ���ꍇ
        if (enemyPosition.x > playerPosition.x)
        {
            OnLeft = true; // �v���C���[�������ɂ���
        }
        // �G�̈ʒu���v���C���[�̈ʒu��荶�ɂ���ꍇ
        if (enemyPosition.x < playerPosition.x)
        {
            OnLeft = false; // �v���C���[���E���ɂ���
        }
    }
}
