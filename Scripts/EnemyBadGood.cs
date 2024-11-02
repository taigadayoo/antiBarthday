using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBadGood : MonoBehaviour
{
    public float speed = 20f; // �e�̑��x
    private Camera mainCamera; // ���C���J����
    [SerializeField]
    private SpriteRenderer spriteRenderer; // �X�v���C�g�����_���[
    private bool _isRendered = false; // �I�t�X�N���[�����
    private bool OneBullet = false; // ��x�����e�𔭎˂���t���O
    public Vector3 initialDirection; // �����̈ړ�����
    private GameManager gameManager; // �Q�[���}�l�[�W���[
    [SerializeField]
    GameObject deathEffect; // �G�����񂾂Ƃ��̃G�t�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // �Q�[���}�l�[�W���[���擾
        mainCamera = Camera.main; // ���C���J�������擾
        spriteRenderer = GetComponent<SpriteRenderer>(); // �X�v���C�g�����_���[���擾
    }

    // Update is called once per frame
    void Update()
    {
        // ���������Ɋ�Â��Ĉړ�
        transform.Translate(initialDirection * speed * Time.deltaTime);

        GoodVector(); // �ړ�����������
        _isRendered = false; // �����_�����O��Ԃ����Z�b�g
        OffCamera(); // �J�����O����
        if (_isRendered == true)
        {
            Destroy(this.gameObject); // �I�t�X�N���[���̏ꍇ�͎������폜
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g�ɂ���Ď��S����
        if (collision.gameObject.tag == "Damage" ||
            collision.gameObject.tag == "Wall" ||
            collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "Ground" ||
            collision.gameObject.tag == "Slorp" ||
            collision.gameObject.tag == "DamageObject" ||
            collision.gameObject.CompareTag("ItemBox"))
        {
            // ���S�G�t�F�N�g�𐶐�
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject); // �������폜
        }
    }

    private void OffCamera()
    {
        // �X�v���C�g�̃o�E���f�B���O�{�b�N�X���擾
        Bounds spriteBounds = spriteRenderer.bounds;

        // �X�v���C�g�̒��S���r���[�|�[�g���W�ɕϊ�
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);
        // �r���[�|�[�g�O�ɏo���ꍇ
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true; // �I�t�X�N���[����Ԃ�ݒ�
        }
    }

    public void OnRight()
    {
        // �E�����Ɉړ�����ݒ�
        initialDirection = Vector3.right;
        spriteRenderer.flipX = true; // �X�v���C�g�𔽓]
    }

    public void OnLeft()
    {
        // �������Ɉړ�����ݒ�
        initialDirection = Vector3.left;
        spriteRenderer.flipX = false; // �X�v���C�g�𔽓]
    }

    private void GoodVector()
    {
        // ��x�����ړ�������ݒ�
        if (OneBullet == false)
        {
            if (gameManager.OnLeft == false)
            {
                OnRight(); // �E������I��
                OneBullet = true; // �t���O�𗧂Ă�
            }
            if (gameManager.OnLeft == true)
            {
                OnLeft(); // ��������I��
                OneBullet = true; // �t���O�𗧂Ă�
            }
        }
    }
}
