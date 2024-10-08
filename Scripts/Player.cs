using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 30f; // �v���C���[�̈ړ����x
    public float bulletPower = 30.0f;
    public int bulletNum = 15;
    public int MaxCakeNum = 15;


    [SerializeField]
    GameManager gameManager;
    public new Animator animation;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    private Vector2 movementVector;
    public bool canJump = true;
    [SerializeField, Header("�W�����v��")]
    private int jumpPower;
    [SerializeField, Header("�ő�W�����v��")]
    public int maxJumpCount = 2;
    public int currentJumpCount = 0;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    GameObject redBullet;
    private Vector3 rightBulletPoint;
    private Vector3 leftBulletPoint;
    private int MaxBullet = 0;
    private bool JumpAni = false;
    [SerializeField] float flashInterval;
    public float invincibleTime = 2.0f;
    public bool isInvincible = false; // �v���C���[�����G���ǂ������Ǘ�����t���O
    private float invincibleStartTime; // ���G��Ԃ��J�n���ꂽ����
    private float nextFlashTime; // ���ɓ_�ł����鎞��
    [SerializeField]
    Damage damage;
    [SerializeField]
    PlayerAbility playerAbility;
   
    PlayerController _playerController;
   
    private void Awake()
    {
        Initialize();
    }
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        animation = gameObject.GetComponent<Animator>();
       
        rightBulletPoint = transform.Find("BulletPoint").localPosition;
        leftBulletPoint = transform.Find("LeftBulletPoint").localPosition;
        

    }

    private void Initialize()
    {
        currentJumpCount = maxJumpCount;
        var jumpC = gameObject.GetComponentInChildren<PlayerJumpController>();
        jumpC.JumpEvent += ResetJump;
    }
    void Update()
    {
        PlayerMovement();
       

        if (Input.GetKeyDown(KeyCode.Space) || _playerController.IsJumpPressed)
        {
            HandleJump();
        }
        if(playerAbility.ability == PlayerAbility.Ability.red)
        {
            if (Input.GetMouseButtonDown(0) || _playerController.IsSelectPressed)
            {
                if (bulletNum > MaxBullet && bulletNum >=3)
                {
                    if (spriteRenderer.flipX == false)
                    {
                        OnThrow();
                        SampleSoundManager.Instance.PlaySe(SeType.SE1);
                        Instantiate(redBullet, transform.position + rightBulletPoint, Quaternion.identity);

                        Invoke("OffThrow", 0.25f);
                    }
                    else
                    {
                        OnThrow();
                        SampleSoundManager.Instance.PlaySe(SeType.SE1);
                        Instantiate(redBullet, transform.position + leftBulletPoint, Quaternion.identity);

                        Invoke("OffThrow", 0.25f);
                    }
                    bulletNum -= 3;
                    //Debug.Log(bulletNum);
                }
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0) || _playerController.IsSelectPressed)
            {
                if (bulletNum > MaxBullet)
                {
                    if (spriteRenderer.flipX == false)
                    {
                        OnThrow();
                        SampleSoundManager.Instance.PlaySe(SeType.SE1);
                        Instantiate(Bullet, transform.position + rightBulletPoint, Quaternion.identity);

                        Invoke("OffThrow", 0.25f);
                    }
                    else
                    {
                        OnThrow();
                        SampleSoundManager.Instance.PlaySe(SeType.SE1);
                        Instantiate(Bullet, transform.position + leftBulletPoint, Quaternion.identity);

                        Invoke("OffThrow", 0.25f);
                    }
                    bulletNum -= 1;
                    //Debug.Log(bulletNum);
                }
            }
        }
        
    }
   
    private void PlayerMovement()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        movementVector = rb.velocity;
        movementVector.x = horizontalKey * moveSpeed;
        rb.velocity = movementVector;

       
        if (horizontalKey > 0)
        {
            //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (JumpAni == false)
            {
                animation.SetBool("Walk", true);
            }
            spriteRenderer.flipX = false;
        }
        if (horizontalKey < 0)
        {
            //rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (JumpAni == false)
            {
                animation.SetBool("Walk", true);
            }
            spriteRenderer.flipX = true;
        }
        if(horizontalKey == 0)
        {
            animation.SetBool("Walk", false);
        }
        
    }
    private void HandleJump()
    {
        if (canJump && currentJumpCount > 0)
        {
            Jump();
            currentJumpCount--;
        }
        if (currentJumpCount <= 0) canJump = false;
    }
    private void Jump()
    {
        SampleSoundManager.Instance.PlaySe(SeType.SE3);
        Vector2 jumpVector = new Vector2(0, jumpPower);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
        animation.SetBool("Jump", true);
        animation.SetBool("Walk", false);
        JumpAni = true;
    }
    private void ResetJump()
    {
        currentJumpCount = maxJumpCount;
        canJump = true;
        OffJump();
        JumpAni = false;
    }
   public void OnThrow()
    {
        animation.SetBool("throw", true);
        
    }
    public void OffThrow()
    {
        animation.SetBool("throw", false);
    }
    public void OnJump()
    {
        animation.SetBool("Jump", true);
    }
    public void OffJump()
    {
        animation.SetBool("Jump", false);
    }
    public void OnDead()
    {
        animation.SetBool("Dead", true);
        this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, 90);
        this.rb.constraints = RigidbodyConstraints2D.FreezePosition;
        this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void OffDead()
    {
        if (damage.Down == true)
        {
            animation.SetBool("Dead", false);
            this.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, -90);
            this.rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            this.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            damage.Down = false;
        }
    }
    void StartInvincibility()
    {
        isInvincible = true;
        invincibleStartTime = Time.time;
        nextFlashTime = Time.time;

     
        // ���Ԋu�œ_�ŏ��������s����
        InvokeRepeating("FlashPlayer", 0, flashInterval);
        // ���G���Ԃ��I�������疳�G��Ԃ���������
        Invoke("EndInvincibility", invincibleTime);
    }

    void FlashPlayer()
    {
        // �X�v���C�g�̕\����؂�ւ���
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    void EndInvincibility()
    {
        isInvincible = false;
        // �X�v���C�g��\������i���G��Ԃ��������ꂽ�Ƃ��ɕ\�������悤�ɂ���j
        spriteRenderer.enabled = true;
        // InvokeRepeating ���~����
        CancelInvoke("FlashPlayer");
    }
    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            StartInvincibility();
        }
    }
}
