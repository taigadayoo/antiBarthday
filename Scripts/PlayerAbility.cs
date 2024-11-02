using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    // �v���C���[�̔\�͂Ɋ֘A����Q�[���I�u�W�F�N�g
    [SerializeField]
    GameObject Red; // �Ԃ��\�͂̃Q�[���I�u�W�F�N�g
    [SerializeField]
    GameObject Blue; // ���\�͂̃Q�[���I�u�W�F�N�g
    [SerializeField]
    GameObject Yellow; // ���F���\�͂̃Q�[���I�u�W�F�N�g

    // ���\�͂Ɋ֘A����ݒ�
    [SerializeField]
    private int BlueJump = 2; // ���\�͂ł̍ő�W�����v��
    [SerializeField]
    private float BlueMove = 45f; // ���\�͂ł̈ړ����x
    [SerializeField]
    YellowCol yellowCol; // ���F���\�͂Ɋ֘A����R���W����
    public float YellowCoolTime = 5f; // ���F���\�͂̃N�[���^�C��

    private int NomalJump; // �ʏ펞�̃W�����v��
    public float lastTrueTime; // �Ō�ɗL������������
    private float NomalMove; // �ʏ펞�̈ړ����x
    public bool YellowOffSwitch = false; // ���F�\�͂̃I�t�X�C�b�`
    [SerializeField]
    AbilitySE abilitySE; // �\�͂Ɋ֘A����T�E���h�G�t�F�N�g
    [SerializeField]
    GameObject yellowTimer; // ���F�\�͂̃^�C�}�[�I�u�W�F�N�g
    [SerializeField]
    YellowTimer yellowTimerScript; // ���F�\�͂̃^�C�}�[�Ǘ��X�N���v�g
    [SerializeField]
    Player player; // �v���C���[�I�u�W�F�N�g
    [SerializeField]
    GameObject damage; // �_���[�W�������I�u�W�F�N�g
    [SerializeField]
    GameObject YellowCol; // ���F�\�͂̃R���W�����I�u�W�F�N�g
    [SerializeField]
    GameObject Mutekicol; // ���G��Ԃ̃R���W�����I�u�W�F�N�g
    PlayerController playerController; // �v���C���[�R���g���[���[

    public bool YellowOn = false; // ���F�\�͂��L�����ǂ���
    public bool nomalOn = true; // �ʏ탂�[�h���L�����ǂ���

    // �\�̗͂񋓌^
    public enum Ability
    {
        nomal, // �ʏ�\��
        red,   // �Ԃ��\��
        blue,  // ���\��
        yellow // ���F���\��
    }

    public Ability ability; // ���݂̔\��

    void Start()
    {
        // �����ݒ�
        playerController = GetComponent<PlayerController>();
        NomalJump = player.maxJumpCount; // �ʏ펞�̃W�����v�񐔂�ݒ�
        NomalMove = player.moveSpeed; // �ʏ펞�̈ړ����x��ݒ�
        NomalMode(); // �ʏ탂�[�h��������
        yellowTimer.SetActive(false); // �^�C�}�[���\���ɂ���
        lastTrueTime = Time.time; // ���݂̎������L�^
    }

    // Update is called once per frame
    void Update()
    {
        // �\�͂̏�Ԃ��X�V
        YellowSwitch(); // ���F�\�͂̏�Ԃ��X�V
        AbilityChange(); // �\�͂̕ύX����
        ColChange(); // �R���W�����̏�Ԃ��X�V
    }

    public void NomalMode()
    {
        // �ʏ탂�[�h�̐ݒ�
        BlueReset(); // ���\�͂̃��Z�b�g
        YellowReset(); // ���F���\�͂̃��Z�b�g
        ability = Ability.nomal; // �\�͂�ʏ�ɐݒ�
        Red.SetActive(false); // �Ԃ��\�͂��\��
        Blue.SetActive(false); // ���\�͂��\��
        Yellow.SetActive(false); // ���F���\�͂��\��
    }

    private void redAbility()
    {
        // �Ԃ��\�͂𔭓�
        abilitySE.SwitchToMode1(); // �T�E���h�G�t�F�N�g��ύX
        ability = Ability.red; // �\�͂�Ԃɐݒ�
        Red.SetActive(true); // �Ԃ��\�͂�\��
        Blue.SetActive(false); // ���\�͂��\��
        Yellow.SetActive(false); // ���F���\�͂��\��
        BlueReset(); // ���\�͂̃��Z�b�g
        YellowReset(); // ���F���\�͂̃��Z�b�g
    }

    private void blueAbility()
    {
        // ���\�͂𔭓�
        abilitySE.SwitchToMode2(); // �T�E���h�G�t�F�N�g��ύX
        player.maxJumpCount = BlueJump; // �ő�W�����v�񐔂�ݒ�
        player.moveSpeed = BlueMove; // �ړ����x��ݒ�
        player.currentJumpCount = BlueJump; // ���݂̃W�����v�񐔂�ݒ�
        ability = Ability.blue; // �\�͂�ɐݒ�
        Blue.SetActive(true); // ���\�͂�\��
        Red.SetActive(false); // �Ԃ��\�͂��\��
        Yellow.SetActive(false); // ���F���\�͂��\��
        YellowReset(); // ���F���\�͂̃��Z�b�g
    }

    private void yellowAbility()
    {
        // ���F���\�͂𔭓�
        damage.SetActive(false); // �_���[�W�𖳌��ɂ���
        YellowCol.SetActive(true); // ���F�R���W������L���ɂ���
        abilitySE.SwitchToMode3(); // �T�E���h�G�t�F�N�g��ύX
        ability = Ability.yellow; // �\�͂����F�ɐݒ�
        Yellow.SetActive(true); // ���F���\�͂�\��
        Red.SetActive(false); // �Ԃ��\�͂��\��
        Blue.SetActive(false); // ���\�͂��\��
        BlueReset(); // ���\�͂̃��Z�b�g
        YellowOn = true; // ���F�\�͂�L���ɂ���
        nomalOn = false; // �ʏ탂�[�h�𖳌��ɂ���
    }

    private void BlueReset()
    {
        // ���\�͂����Z�b�g
        player.maxJumpCount = NomalJump; // �ő�W�����v�񐔂�ʏ�ɐݒ�
        player.currentJumpCount = NomalJump; // ���݂̃W�����v�񐔂�ʏ�ɐݒ�
        player.moveSpeed = NomalMove; // �ړ����x��ʏ�ɐݒ�
    }

    private void YellowReset()
    {
        // ���F���\�͂����Z�b�g
        damage.SetActive(true); // �_���[�W��L���ɂ���
        YellowCol.SetActive(false); // ���F�R���W�����𖳌��ɂ���
        YellowOn = false; // ���F�\�͂𖳌��ɂ���
        nomalOn = true; // �ʏ탂�[�h��L���ɂ���
    }


private void AbilityChange()
    {
        // �v���C���[���d�͔��]�{�^���������Ă��邩�A�E�N���b�N�Œʏ탂�[�h�̏ꍇ
        if (playerController.IsGravityReversePressed && ability == Ability.nomal || Input.GetMouseButtonDown(1) && ability == Ability.nomal)
        {
            // �Ԃ��\�͂𔭓�
            redAbility();
        }
        // �v���C���[���d�͔��]�{�^���������Ă��邩�A�E�N���b�N�ŉ��F�\�͂̏ꍇ
        else if (playerController.IsGravityReversePressed && ability == Ability.yellow || Input.GetMouseButtonDown(1) && ability == Ability.yellow)
        {
            // �Ԃ��\�͂𔭓�
            redAbility();
        }
        // �v���C���[���d�͔��]�{�^���������Ă��邩�A�E�N���b�N�ŐԂ��\�͂̏ꍇ
        else if (playerController.IsGravityReversePressed && ability == Ability.red || Input.GetMouseButtonDown(1) && ability == Ability.red)
        {
            // ���\�͂𔭓�
            blueAbility();
        }
        // �v���C���[���d�͔��]�{�^���������Ă��邩�A�E�N���b�N�Ő��\�͂ŁA���F�I�t�X�C�b�`�������̏ꍇ
        else if (playerController.IsGravityReversePressed && ability == Ability.blue && YellowOffSwitch == false || Input.GetMouseButtonDown(1) && ability == Ability.blue && YellowOffSwitch == false)
        {
            // ���F���\�͂𔭓�
            yellowAbility();
        }
        // �v���C���[���d�͔��]�{�^���������Ă��邩�A�E�N���b�N�Ő��\�͂ŁA���F�I�t�X�C�b�`���L���̏ꍇ
        else if (playerController.IsGravityReversePressed && ability == Ability.blue && YellowOffSwitch == true || Input.GetMouseButtonDown(1) && ability == Ability.blue && YellowOffSwitch == true)
        {
            // �Ԃ��\�͂𔭓�
            redAbility();
        }
        // �ʏ�\�͂̏ꍇ
        else if (ability == Ability.nomal)
        {
            // �ʏ탂�[�h�𔭓�
            NomalMode();
        }
    }

    public void YellowSwitch()
    {
        // ���F�I�t�X�C�b�`���L���ȏꍇ
        if (YellowOffSwitch)
        {
            // ���F�̃^�C�}�[��\��
            yellowTimer.SetActive(true);

            // ���F�̃N�[���^�C�����o�߂����ꍇ
            if (Time.time - lastTrueTime >= YellowCoolTime)
            {
                // ���F�I�t�X�C�b�`�𖳌��ɂ��A�Ō�̃^�C�����X�V
                YellowOffSwitch = false;
                lastTrueTime = Time.time;
            }
        }
        else
        {
            // ���F�̃^�C�}�[���\��
            yellowTimer.SetActive(false);
        }
    }

    public void StartYellowAbilityCooldown()
    {
        // ���F�̃N�[���^�C�����J�n
        yellowTimerScript.StartTimer(YellowCoolTime);
    }

    private void ColChange()
    {
        // �ʏ탂�[�h�Ŗ��G�łȂ��A���F�\�͂������ȏꍇ
        if (nomalOn == true && player.isInvincible == false && YellowOn == false)
        {
            // �_���[�W�I�u�W�F�N�g��L���ɂ��A���𖳌��ɂ���
            damage.SetActive(true);
            YellowCol.SetActive(false);
            Mutekicol.SetActive(false);
        }
        // �ʏ탂�[�h�łȂ��A���G�łȂ��A���F�\�͂��L���ȏꍇ
        else if (!nomalOn && !player.isInvincible && YellowOn)
        {
            // �_���[�W�I�u�W�F�N�g�𖳌��ɂ��A���F�R���W������L���ɂ���
            damage.SetActive(false);
            YellowCol.SetActive(true);
            Mutekicol.SetActive(false);
        }
        // �ʏ탂�[�h�ł���A���G�ŁA���F�\�͂������ȏꍇ
        else if (nomalOn && player.isInvincible && !YellowOn)
        {
            // ���𖳌��ɂ��A���G�R���W������L���ɂ���
            damage.SetActive(false);
            YellowCol.SetActive(false);
            Mutekicol.SetActive(true);
        }
    }
}
