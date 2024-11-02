using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player player; // �v���C���[�ւ̎Q��
    public enum Charactor
    {
        Player, // �v���C���[�p�̒e
        Enemy   // �G�p�̒e
    }
    public float MoveSpeed = 20.0f; // �e�̈ړ����x

    private Camera mainCamera; // ���C���J�����ւ̎Q��
    private Charactor charactor; // �L�����N�^�[�̎�ށi�v���C���[���G���j
    private bool RightLeft = true; // �e���E�ɔ�Ԃ����ɔ�Ԃ��̃t���O
    private SpriteRenderer spriteRenderer; // �X�v���C�g�����_���[�̎Q��
    private bool _isRendered = false; // ��ʓ��ɕ`�悳��Ă��邩�ǂ����̃t���O

    private Vector3 initialDirection; // �e�̏�������

    // ����������
    void Start()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE11); // �e���ˉ����Đ�
        mainCamera = Camera.main; // ���C���J�������擾
        spriteRenderer = GetComponent<SpriteRenderer>(); // �X�v���C�g�����_���[���擾
        player = GameObject.Find("Player").GetComponent<Player>(); // �v���C���[�R���|�[�l���g���擾
        initialDirection = Vector3.right; // �����̈ړ��������E�ɐݒ�
    }

    // ���t���[���X�V����
    void Update()
    {
        BulletAttack(); // �e�̍U������
        _isRendered = false; // �`��t���O�����Z�b�g
        OffCamera(); // �J�����O�ɏo�����`�F�b�N
    }

    // �e�̍U������
    private void BulletAttack()
    {
        if (player != null)
        {
            // �v���C���[�̌����ɉ����Ēe�̌�����ݒ�
            if (player.spriteRenderer.flipX == true && RightLeft)
            {
                initialDirection = Vector3.left; // �v���C���[���������Ȃ�e������
                RightLeft = false;
            }
            else if (player.spriteRenderer.flipX == false && RightLeft)
            {
                initialDirection = Vector3.right; // �v���C���[���E�����Ȃ�e���E��
                RightLeft = false;
            }

            // �e��ݒ肵�������Ɉړ�
            transform.Translate(initialDirection * MoveSpeed * Time.deltaTime);

            // ��ʊO�ɏo����e��j��
            if (_isRendered == true)
            {
                Destroy(gameObject);
            }
        }
    }

    // �e����ʊO�ɏo���Ƃ��ɌĂ΂��
    private void OnBecameInvisible()
    {
        _isRendered = true; // �`��t���O���I���ɂ���
    }

    // �e���J�����O�ɏo�������`�F�b�N
    private void OffCamera()
    {
        Bounds spriteBounds = spriteRenderer.bounds; // �X�v���C�g�̋��E���擾

        // �r���[�|�[�g���W�ɕϊ����Ĕ͈͊O���ǂ����m�F
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true; // �͈͊O�Ȃ�`��t���O���I��
        }
    }

    // ���̃I�u�W�F�N�g�Ƃ̏Փˏ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����̃^�O�����I�u�W�F�N�g�ɏՓ˂�����e��j��
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "DamageObject" || collision.gameObject.CompareTag("Slorp") || collision.gameObject.CompareTag("ItemBox"))
        {
            Destroy(this.gameObject);
        }
    }
}
