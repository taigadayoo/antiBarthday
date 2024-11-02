using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    // �v���C���[�I�u�W�F�N�g�̎Q��
    private Player player;

    // �L�����N�^�[�̎�ނ��`����񋓌^
    public enum Charactor
    {
        Player, // �v���C���[
        Enemy   // �G
    }

    // �e�̈ړ����x
    public float MoveSpeed = 20.0f;

    // ���C���J�����̎Q��
    private Camera mainCamera;

    // �L�����N�^�[�̎��
    private Charactor charactor;

    // ���E�̕������Ǘ�����t���O
    private bool RightLeft = true;

    // �X�v���C�g�����_���[�̎Q��
    private SpriteRenderer spriteRenderer;

    // �e���J�����O�ɏo�����ǂ����̃t���O
    private bool _isRendered = false;

    // �e�̏�������
    private Vector3 initialDirection;

    void Start()
    {
        // ���C���J�����̎擾
        mainCamera = Camera.main;

        // �X�v���C�g�����_���[�̎擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �v���C���[�I�u�W�F�N�g�̎擾
        player = GameObject.Find("Player").GetComponent<Player>();

        // �����������E�ɐݒ�
        initialDirection = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        BulletAttack(); // �e�̍U������
        _isRendered = false; // �\����Ԃ����Z�b�g
        OffCamera(); // �J�����O�̃`�F�b�N
    }

    private void BulletAttack()
    {
        if (player != null) // �v���C���[�����݂���ꍇ
        {
            // �v���C���[�����������Ă���ꍇ
            if (player.spriteRenderer.flipX == true && RightLeft)
            {
                initialDirection = Vector3.left; // �������������ɐݒ�
                RightLeft = false; // �t���O���X�V
            }
            // �v���C���[���E�������Ă���ꍇ
            else if (player.spriteRenderer.flipX == false && RightLeft)
            {
                initialDirection = Vector3.right; // �����������E�ɐݒ�
                RightLeft = false; // �t���O���X�V
            }

            // �e�̈ړ�����
            transform.Translate(initialDirection * MoveSpeed * Time.deltaTime);

            // �\����Ԃ�true�̏ꍇ�A�e��j��
            if (_isRendered == true)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        // �e����ʊO�ɏo���ꍇ
        _isRendered = true;
    }

    private void OffCamera()
    {
        // �X�v���C�g�̃o�E���Y���擾
        Bounds spriteBounds = spriteRenderer.bounds;

        // ���[���h���W���r���[�|�[�g���W�ɕϊ�
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);

        // �r���[�|�[�g�̊O�ɏo�Ă���ꍇ
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true; // �\����Ԃ��X�V
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g�̃^�O���m�F
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground" ||
            collision.gameObject.tag == "DamageObject" || collision.gameObject.CompareTag("Slorp"))
        {
            // �ǂ�n�ʁA�_���[�W�I�u�W�F�N�g�A�܂���Slorp�^�O�̃I�u�W�F�N�g�ƏՓ˂����ꍇ�A�e��j��
            Destroy(this.gameObject);
        }
    }
}
