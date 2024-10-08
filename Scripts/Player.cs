using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 30f; // プレイヤーの移動速度
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
    [SerializeField, Header("ジャンプ力")]
    private int jumpPower;
    [SerializeField, Header("最大ジャンプ数")]
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
    public bool isInvincible = false; // プレイヤーが無敵かどうかを管理するフラグ
    private float invincibleStartTime; // 無敵状態が開始された時間
    private float nextFlashTime; // 次に点滅させる時間
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

     
        // 一定間隔で点滅処理を実行する
        InvokeRepeating("FlashPlayer", 0, flashInterval);
        // 無敵時間が終了したら無敵状態を解除する
        Invoke("EndInvincibility", invincibleTime);
    }

    void FlashPlayer()
    {
        // スプライトの表示を切り替える
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    void EndInvincibility()
    {
        isInvincible = false;
        // スプライトを表示する（無敵状態が解除されたときに表示されるようにする）
        spriteRenderer.enabled = true;
        // InvokeRepeating を停止する
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
