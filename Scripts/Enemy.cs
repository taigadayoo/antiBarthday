using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyLife = 1; // �G�̃��C�t

    public float amplitude = 1f; // �c�����̓����̐U��
    public float moveSpeed = 20f; // �ړ����x
    public float moveSpeedVertical = 15f; // �c�ړ��̑��x
    public float maxY = 5f; // ������̈ړ��͈�
    public float minY = -5f; // �������̈ړ��͈�
    public float jumpForce = 5f; // �W�����v�̗�
    public float minJumpInterval = 1f; // �ŏ��W�����v�Ԋu
    public float maxJumpInterval = 3f; // �ő�W�����v�Ԋu
    public float changeDirectionInterval = 3f; // �����]���̊Ԋu

    [SerializeField]
    GameObject deathEffect; // �G�����S�����ۂ̃G�t�F�N�g
    [SerializeField]
    GameObject Ban1; // �o��1�i�X�v���C�g���j
    [SerializeField]
    GameObject Ban2; // �o��2�i�X�v���C�g���j

    [SerializeField]
    public GameObject bulletPrefab; // ���˂���e�̃v���n�u
    public Transform firePoint; // �e�̔��ˈʒu

    private float nextFireTime; // ���ɒe�𔭎˂��鎞��
    private float minFireInterval = 1f; // �ŏ����ˊԊu
    private float maxFireInterval = 3f; // �ő唭�ˊԊu

    private GameObject bulletInstance; // ���˂��ꂽ�e�̃C���X�^���X

    [SerializeField]
    public SpriteRenderer spriteRenderer; // �X�v���C�g�����_���[
    [SerializeField]
    EnemyCol enemyCol; // �G�̃R���W�����Ǘ�
    private Rigidbody2D rb; // ���W�b�h�{�f�B

    private Camera mainCamera; // ���C���J�����̎Q��

    private Vector2 initialPosition; // �����ʒu

    public bool _isRendered = false; // �`�悳��Ă��邩�̃t���O
    public bool _isRenderedThrow = false; // ������G���`�悳��Ă��邩�̃t���O
    public bool _isRenderedBird = false; // ���^�G���`�悳��Ă��邩�̃t���O
    private bool movingRight = true; // �E�����Ɉړ����Ă��邩�̃t���O
    private float timeSinceLastDirectionChange = 0f; // �Ō�ɕ�����ς��Ă���̎���
    GameManager gameManager; // �Q�[���}�l�[�W���[�̎Q��

    FollowCamera followCamera; // �J�����ǔ��p�̃X�N���v�g�Q��
    EnemyVoice enemyVoice; // �G�̉����Ǘ�

    RandomEnemyVoice randomEnemyVoice; // �����_���ȓG�̉������Ǘ�

    [SerializeField]
    EnemyVoiceMob voiceMob; // �G�{�C�X�̃��u�Ǘ�

    [SerializeField]
    anitiVoice AntiVoice; // �A���`�{�C�X�Ǘ�

    public enum EnemyType
    {
        nomal, // �ʏ�̓G
        side, // ���ړ�����G
        vertical, // �c�ړ�����G
        Throw // �e�𓊂���G
    }
    [SerializeField] EnemyType enemyType; // �G�̃^�C�v

    void Start()
    {
        followCamera = FindObjectOfType<FollowCamera>(); // FollowCamera�R���|�[�l���g��T���Ď擾
        randomEnemyVoice = GetComponent<RandomEnemyVoice>(); // ���g��RandomEnemyVoice�R���|�[�l���g���擾
        enemyVoice = FindObjectOfType<EnemyVoice>(); // �V�[������EnemyVoice�R���|�[�l���g��T���Ď擾
        initialPosition = transform.position; // �G�̏����ʒu��ۑ�
        mainCamera = Camera.main; // ���C���J�������擾
        spriteRenderer = GetComponent<SpriteRenderer>(); // ���g��SpriteRenderer�R���|�[�l���g���擾
        gameManager = FindObjectOfType<GameManager>(); // �V�[������GameManager�R���|�[�l���g��T���Ď擾
        rb = GetComponent<Rigidbody2D>(); // ���g��Rigidbody2D�R���|�[�l���g���擾
        if (enemyType == EnemyType.side)
        {
            StartCoroutine(RandomJump()); // �G�̃^�C�v���T�C�h�̏ꍇ�A�����_���ɃW�����v����R���[�`�����J�n
        }
        if (enemyType == EnemyType.Throw)
        {
            nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval); // �e�𔭎˂���^�C�~���O��ݒ�
        }
    }

    // Update�͖��t���[���Ăяo�����
    void Update()
    {
        EnemyDead(); // �G�̎��S����
        SideMove(); // ���ړ�����
        VerticalMove(); // �c�ړ�����
        Enemyvanish(); // �G����ʊO�ɏo���ꍇ�̏���
        ThrowEnemy(); // �e�𓊂��鏈��
        _isRendered = false; // �`��t���O�����Z�b�g
        _isRenderedBird = false; // ���^�G�̕`��t���O�����Z�b�g
        _isRenderedThrow = false; // ������G�̕`��t���O�����Z�b�g
        OffCamera(); // �J�����O�̓G�̏���
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Փ˂����I�u�W�F�N�g���P�[�L�n�̃^�O�̏ꍇ�A�G�̃��C�t�����炷
        if (collision.gameObject.tag == "Cake" || collision.gameObject.tag == "RedCake")
        {
            EnemyLife -= 1; // �G�̃��C�t��1����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �g���K�[�ɓ������I�u�W�F�N�g���uDead�v�^�O�̏ꍇ�A���S�������s��
        if (collision.gameObject.tag == "Dead")
        {
            Instantiate(deathEffect, this.transform.position, this.transform.rotation); // ���S�G�t�F�N�g�𐶐�
            Destroy(this.gameObject); // �G�I�u�W�F�N�g���폜
        }
    }

    private void EnemyDead()
    {
        // �G�̃��C�t��0�ȉ��ɂȂ����ꍇ�̏���
        if (EnemyLife <= 0)
        {
            // �G�̃^�C�v���c�̏ꍇ�A�������~���Ď��S�����Đ�
            if (enemyType == EnemyType.vertical && enemyVoice != null)
            {
                enemyVoice.audioSource.Stop(); // �������~
                enemyVoice.BirdDeath(); // ���S�����Đ�
            }

            // �G�̃^�C�v�����̏ꍇ�A�������~���ă����_���ȃT�E���h���Đ�
            if (enemyType == EnemyType.side)
            {
                voiceMob.audioSourceMattyo.Stop(); // �������~
                randomEnemyVoice.PlayRandomSoundExternal(); // �����_���ȃT�E���h���Đ�
            }

            // �G�̃^�C�v��������ꍇ�A�������~����SE���Đ�
            if (enemyType == EnemyType.Throw)
            {
                AntiVoice.audioSourceAnti.Stop(); // �������~
                SampleSoundManager.Instance.PlaySe(SeType.SE15); // SE���Đ�
            }

            // ���S�G�t�F�N�g�𐶐����A�G�I�u�W�F�N�g���폜
            Instantiate(deathEffect, this.transform.position, this.transform.rotation); // ���S�G�t�F�N�g�𐶐�
            Destroy(this.gameObject); // �G�I�u�W�F�N�g���폜
        }
    }

    private void ThrowEnemy()
    {
        // �G�̃^�C�v��������ꍇ�̏���
        if (enemyType == EnemyType.Throw)
        {
            // �G���`�悳��Ă��āA�J�����ɉf���Ă���ꍇ
            if (this._isRenderedThrow == true && followCamera.OnCamera == true)
            {
                AntiVoice.EnemyAntiVoiceOn(); // �G�̉������Đ�
            }

            // �G�̈ړ��������ݒ肳��Ă���ꍇ
            if (enemyCol != null)
            {
                // �E�Ɉړ�����ꍇ
                if (enemyCol.moveRight)
                {
                    transform.Translate(Vector2.right * moveSpeed * 3 * Time.deltaTime); // �E�Ɉړ�
                    spriteRenderer.flipX = true; // �X�v���C�g�𔽓]
                }
                // ���Ɉړ�����ꍇ
                else
                {
                    transform.Translate(Vector2.left * moveSpeed * 3 * Time.deltaTime); // ���Ɉړ�
                    spriteRenderer.flipX = false; // �X�v���C�g�𔽓]
                }
            }

            // �G���`�悳��Ă���ꍇ
            if (_isRenderedThrow == true)
            {
                // ���̔��˃^�C�~���O�������ꍇ
                if (Time.time >= nextFireTime)
                {
                    Shoot(); // �e�𔭎�
                    nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval); // ���̔��˃^�C�~���O��ݒ�
                }
            }
        }
    }

    private void SideMove()
    {
        // �G���`�悳��Ă��āA�����R���|�[�l���g�����݂��A�J�����ɉf���Ă���ꍇ
        if (this._isRendered == true && enemyVoice != null && followCamera.OnCamera == true)
        {
            voiceMob.EnemyNomalVoiceOn(); // �G�̒ʏ퉹�����Đ�
        }

        // �G�̃^�C�v�����ŁA�`�悳��Ă���ꍇ
        if (enemyType == EnemyType.side && _isRendered)
        {
            // �G�̈ړ��������ݒ肳��Ă���ꍇ
            if (enemyCol != null)
            {
                // �E�Ɉړ�����ꍇ
                if (enemyCol.moveRight)
                {
                    transform.Translate(Vector2.right * moveSpeed * 2 * Time.deltaTime); // �E�Ɉړ�
                    spriteRenderer.flipX = true; // �X�v���C�g�𔽓]
                }
                // ���Ɉړ�����ꍇ
                else
                {
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // ���Ɉړ�
                    spriteRenderer.flipX = false; // �X�v���C�g�𔽓]
                }
            }
        }
    }

    private void VerticalMove()
    {
        // �G�̃^�C�v���c�̏ꍇ
        if (enemyType == EnemyType.vertical)
        {
            // �G���`�悳��Ă��āA�����R���|�[�l���g�����݂��A�J�����ɉf���Ă���ꍇ
            if (this._isRenderedBird == true && enemyVoice != null && followCamera.OnCamera == true)
            {
                enemyVoice.BirdVoiceNomal(); // ���̒ʏ퉹�����Đ�
            }

            // �V����Y���W���v�Z�i�T�C���g�ɂ��㉺���j
            float newY = initialPosition.y + Mathf.Sin(Time.time * moveSpeedVertical) * amplitude;

            // �V����X���W���v�Z�i���E�ړ��j
            float newX = transform.position.x - (movingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(newX, newY, transform.position.z); // �V�����ʒu�Ɉړ�

            // �����]���̎��Ԃ��v��
            timeSinceLastDirectionChange += Time.deltaTime;
            if (timeSinceLastDirectionChange >= changeDirectionInterval)
            {
                movingRight = !movingRight; // �ړ������𔽓]
                timeSinceLastDirectionChange = 0f; // �^�C�}�[�����Z�b�g
            }

            // �ړ������ɉ����ăo���̕\����؂�ւ�
            if (movingRight)
            {
                if (Ban1 != null && Ban2 != null)
                {
                    Ban1.SetActive(true); // Ban1��\��
                    Ban2.SetActive(false); // Ban2���\��
                    spriteRenderer.flipX = false; // �X�v���C�g�𔽓]���Ȃ�
                }
            }
            else if (!movingRight)
            {
                if (Ban1 != null && Ban2 != null)
                {
                    Ban2.SetActive(true); // Ban2��\��
                    Ban1.SetActive(false); // Ban1���\��
                    spriteRenderer.flipX = true; // �X�v���C�g�𔽓]
                }
            }
        }
    }

    private void Enemyvanish()
    {
        // �Q�[�����̂��ׂĂ̓G�����S���Ă���ꍇ
        if (gameManager.EnemyAllDead == true)
        {
            Destroy(this.gameObject); // ���g�̃Q�[���I�u�W�F�N�g���폜
        }
    }

    IEnumerator RandomJump()
    {
        // �������[�v
        while (true)
        {
            // �����_���Ȏ��ԑҋ@
            yield return new WaitForSeconds(Random.Range(minJumpInterval, maxJumpInterval));
            Jump(); // �W�����v���\�b�h���Ăяo��
        }
    }

    void Jump()
    {
        // ������ɗ͂������ăW�����v������
        rb.velocity = Vector2.up * jumpForce;
    }

    private void Shoot()
    {
        // �e�𐶐�
        bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // �e�Ǝ����̃R���W�����𖳎�����
        Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OffCamera()
    {
        // �X�v���C�g�̃o�E���f�B���O�{�b�N�X���擾
        Bounds spriteBounds = spriteRenderer.bounds;

        // �X�v���C�g�̒��S���r���[�|�[�g���W�ɕϊ�
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);

        // �X�v���C�g���J�����̃r���[�|�[�g���ɂ���ꍇ
        if (viewportPosition.x > 0 && viewportPosition.x < 1)
        {
            // �G�̃^�C�v�ɉ����ĕ`���Ԃ��X�V
            if (enemyType == EnemyType.vertical)
            {
                _isRenderedBird = true; // ���^�G�̕`���Ԃ�ݒ�
            }
            if (enemyType == EnemyType.side)
            {
                _isRendered = true; // ���^�G�̕`���Ԃ�ݒ�
            }
            if (enemyType == EnemyType.Throw)
            {
                _isRenderedThrow = true; // �����^�G�̕`���Ԃ�ݒ�
            }
        }
    }
}
