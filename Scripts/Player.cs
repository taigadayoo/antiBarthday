using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 30f; // �v���C���[�̈ړ����x
    public float bulletPower = 30.0f; // �e�̃p���[
    public int bulletNum = 15; // �������Ă���e�̐�
    public int MaxCakeNum = 15; // �ő�P�[�L��

    [SerializeField]
    GameManager gameManager; // �Q�[���}�l�[�W���[�̎Q��
    public new Animator animation; // �v���C���[�̃A�j���[�^�[
    [SerializeField]
    private Rigidbody2D rb; // �v���C���[��Rigidbody2D
    [SerializeField]
    public SpriteRenderer spriteRenderer; // �v���C���[�̃X�v���C�g�����_���[
    private Vector2 movementVector; // �v���C���[�̈ړ��x�N�g��
    public bool canJump = true; // �W�����v�\���ǂ����̃t���O
    [SerializeField, Header("�W�����v��")]
    private int jumpPower; // �W�����v��
    [SerializeField, Header("�ő�W�����v��")]
    public int maxJumpCount = 2; // �ő�W�����v��
    public int currentJumpCount = 0; // ���݂̃W�����v��
    [SerializeField]
    private GameObject Bullet; // �ʏ�̒e�̃v���n�u
    [SerializeField]
    GameObject redBullet; // �Ԃ��e�̃v���n�u
    private Vector3 rightBulletPoint; // �E������̒e�̔��ˈʒu
    private Vector3 leftBulletPoint; // ��������̒e�̔��ˈʒu
    private int MaxBullet = 0; // �ő�e��
    private bool JumpAni = false; // �W�����v�A�j���[�V���������ǂ����̃t���O
    [SerializeField] float flashInterval; // �_�ŊԊu
    public float invincibleTime = 2.0f; // ���G����
    public bool isInvincible = false; // �v���C���[�����G���ǂ������Ǘ�����t���O
    private float invincibleStartTime; // ���G��Ԃ��J�n���ꂽ����
    private float nextFlashTime; // ���ɓ_�ł����鎞��
    [SerializeField]
    Damage damage; // �_���[�W�Ǘ�
    [SerializeField]
    PlayerAbility playerAbility; // �v���C���[�̃A�r���e�B�Ǘ�

    PlayerController _playerController; // �v���C���[�R���g���[���[�̎Q��

    private void Awake()
    {
        Initialize(); // �������������Ăяo��
    }

    void Start()
    {
        _playerController = GetComponent<PlayerController>(); // PlayerController���擾
        animation = gameObject.GetComponent<Animator>(); // Animator���擾

        // �e�̔��ˈʒu��ݒ�
        rightBulletPoint = transform.Find("BulletPoint").localPosition;
        leftBulletPoint = transform.Find("LeftBulletPoint").localPosition;
    }

    // ����������
    private void Initialize()
    {
        currentJumpCount = maxJumpCount; // ���݂̃W�����v�����ő�ɐݒ�
        var jumpC = gameObject.GetComponentInChildren<PlayerJumpController>(); // �q�I�u�W�F�N�g��PlayerJumpController���擾
        jumpC.JumpEvent += ResetJump; // �W�����v�C�x���g��ResetJump���\�b�h��o�^
    }

    void Update()
    {
        PlayerMovement(); // �v���C���[�̈ړ��������Ăяo��

        // �X�y�[�X�L�[�܂��̓W�����v�{�^���������ꂽ���̏���
        if (Input.GetKeyDown(KeyCode.Space) || _playerController.IsJumpPressed)
        {
            HandleJump(); // �W�����v�������Ăяo��
        }

        // �v���C���[�̃A�r���e�B�ɂ��e�̔��ˏ���
        if (playerAbility.ability == PlayerAbility.Ability.red)
        {
            // �}�E�X�{�^���������ꂽ���̏���
            if (Input.GetMouseButtonDown(0) || _playerController.IsSelectPressed)
            {
                // �ő�e���𒴂��A�e��3�ȏ゠��ꍇ
                if (bulletNum > MaxBullet && bulletNum >= 3)
                {
                    // �E������̔��ˏ���
                    if (spriteRenderer.flipX == false)
                    {
                        OnThrow(); // ������A�j���[�V�������J�n
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // �T�E���h�Đ�
                        Instantiate(redBullet, transform.position + rightBulletPoint, Quaternion.identity); // �e�̐���
                        Invoke("OffThrow", 0.25f); // ������A�j���[�V�������I�t�ɂ���
                    }
                    // ��������̔��ˏ���
                    else
                    {
                        OnThrow(); // ������A�j���[�V�������J�n
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // �T�E���h�Đ�
                        Instantiate(redBullet, transform.position + leftBulletPoint, Quaternion.identity); // �e�̐���
                        Invoke("OffThrow", 0.25f); // ������A�j���[�V�������I�t�ɂ���
                    }
                    bulletNum -= 3; // �e�̐�������
                }
            }
        }
        // �ʏ�̒e�̔��ˏ���
        else
        {
            // �}�E�X�{�^���������ꂽ���̏���
            if (Input.GetMouseButtonDown(0) || _playerController.IsSelectPressed)
            {
                // �ő�e���𒴂��Ă���ꍇ
                if (bulletNum > MaxBullet)
                {
                    // �E������̔��ˏ���
                    if (spriteRenderer.flipX == false)
                    {
                        OnThrow(); // ������A�j���[�V�������J�n
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // �T�E���h�Đ�
                        Instantiate(Bullet, transform.position + rightBulletPoint, Quaternion.identity); // �e�̐���
                        Invoke("OffThrow", 0.25f); // ������A�j���[�V�������I�t�ɂ���
                    }
                    // ��������̔��ˏ���
                    else
                    {
                        OnThrow(); // ������A�j���[�V�������J�n
                        SampleSoundManager.Instance.PlaySe(SeType.SE1); // �T�E���h�Đ�
                        Instantiate(Bullet, transform.position + leftBulletPoint, Quaternion.identity); // �e�̐���
                        Invoke("OffThrow", 0.25f); // ������A�j���[�V�������I�t�ɂ���
                    }
                    bulletNum -= 1; // �e�̐�������
                }
            }
        }
    }

    // �v���C���[�̈ړ�����
    private void PlayerMovement()
    {
        float horizontalKey = Input.GetAxis("Horizontal"); // �������͂��擾
        movementVector = rb.velocity; // ���݂̑��x���擾
        movementVector.x = horizontalKey * moveSpeed; // �����ړ����x��ݒ�
        rb.velocity = movementVector; // Rigidbody�̑��x���X�V

        // �E�����ւ̈ړ�����
        if (horizontalKey > 0)
        {
            if (JumpAni == false)
            {
                animation.SetBool("Walk", true); // ���s�A�j���[�V������L���ɂ���
            }
            spriteRenderer.flipX = false; // �X�v���C�g���E�����ɂ���
        }
        // �������ւ̈ړ�����
        if (horizontalKey < 0)
        {
            if (JumpAni == false)
            {
                animation.SetBool("Walk", true); // ���s�A�j���[�V������L���ɂ���
            }
            spriteRenderer.flipX = true; // �X�v���C�g���������ɂ���
        }
        // ��~���̏���
        if (horizontalKey == 0)
        {
            animation.SetBool("Walk", false); // ���s�A�j���[�V�����𖳌��ɂ���
        }
    }

    // �W�����v����
    private void HandleJump()
    {
        // �W�����v�\���W�����v�����c���Ă���ꍇ
        if (canJump && currentJumpCount > 0)
        {
            Jump(); // �W�����v�������Ăяo��
            currentJumpCount--; // �W�����v��������
        }
        // �W�����v����0�ɂȂ�����W�����v�s��
        if (currentJumpCount <= 0) canJump = false;
    }
    private void Jump()
    {
        // �W�����v�����Đ�
        SampleSoundManager.Instance.PlaySe(SeType.SE3);

        // �W�����v�̃x�N�g�����쐬
        Vector2 jumpVector = new Vector2(0, jumpPower);

        // ���݂̑��x��Y���������Z�b�g�i�������̑��x��0�ɂ���j
        rb.velocity = new Vector2(rb.velocity.x, 0);

        // �W�����v�̗͂�������i�C���p���X�����Łj
        rb.AddForce(jumpVector, ForceMode2D.Impulse);

        // �W�����v�A�j���[�V������L���ɂ��A���s�A�j���[�V�����𖳌��ɂ���
        animation.SetBool("Jump", true);
        animation.SetBool("Walk", false);

        // �W�����v�A�j���[�V�������t���O���Z�b�g
        JumpAni = true;
    }

    private void ResetJump()
    {
        // �W�����v�J�E���g�����Z�b�g
        currentJumpCount = maxJumpCount;

        // �W�����v�\��Ԃɐݒ�
        canJump = true;

        // �W�����v�A�j���[�V�������I�t�ɂ���
        OffJump();

        // �W�����v�A�j���[�V�������t���O�����Z�b�g
        JumpAni = false;
    }

    public void OnThrow()
    {
        // ������A�j���[�V������L���ɂ���
        animation.SetBool("throw", true);
    }

    public void OffThrow()
    {
        // ������A�j���[�V�����𖳌��ɂ���
        animation.SetBool("throw", false);
    }

    public void OnJump()
    {
        // �W�����v�A�j���[�V������L���ɂ���
        animation.SetBool("Jump", true);
    }

    public void OffJump()
    {
        // �W�����v�A�j���[�V�����𖳌��ɂ���
        animation.SetBool("Jump", false);
    }

    public void OnDead()
    {
        // ���S�A�j���[�V������L���ɂ���
        animation.SetBool("Dead", true);

        // �I�u�W�F�N�g��90�x��]������i���S���̉��o�j
        this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, 90);

        // Rigidbody�̈ʒu���Œ肵�āA�����Ȃ��悤�ɂ���
        this.rb.constraints = RigidbodyConstraints2D.FreezePosition;
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void OffDead()
    {
        // �_���[�W���󂯂Ă���ꍇ
        if (damage.Down == true)
        {
            // ���S�A�j���[�V�����𖳌��ɂ���
            animation.SetBool("Dead", false);

            // �I�u�W�F�N�g�����̉�]�ɖ߂�
            this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, -90);

            // Rigidbody�̈ʒu������������āA��������悤�ɂ���
            this.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            // �_���[�W�t���O�����Z�b�g
            damage.Down = false;
        }
    }

    void StartInvincibility()
    {
        // ���G��Ԃ��J�n
        isInvincible = true;
        invincibleStartTime = Time.time; // ���G�J�n�������L�^
        nextFlashTime = Time.time; // �_�ŊJ�n�������L�^

        // ���Ԋu�œ_�ŏ��������s����
        InvokeRepeating("FlashPlayer", 0, flashInterval);

        // ���G���Ԃ��I�������疳�G��Ԃ���������
        Invoke("EndInvincibility", invincibleTime);
    }

    void FlashPlayer()
    {
        // �X�v���C�g�̕\����؂�ւ���i�_�Ō��ʁj
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    void EndInvincibility()
    {
        // ���G��Ԃ�����
        isInvincible = false;

        // �X�v���C�g��\������i���G��Ԃ��������ꂽ�Ƃ��ɕ\�������悤�ɂ���j
        spriteRenderer.enabled = true;

        // InvokeRepeating ���~����
        CancelInvoke("FlashPlayer");
    }

    public void ActivateInvincibility()
    {
        // ���G�łȂ��ꍇ�ɖ��G���J�n
        if (!isInvincible)
        {
            StartInvincibility();
        }
    }
}