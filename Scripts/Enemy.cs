using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyLife = 1;
   
    public float amplitude = 1f;
    public float moveSpeed = 20f; // 移動速度
    public float moveSpeedVertical = 15f; // 移動速度
    public float maxY = 5f; // 上方向の移動範囲
    public float minY = -5f; // 下方向の移動範囲
    public float jumpForce = 5f; // ジャンプの力
    public float minJumpInterval = 1f; // 最小ジャンプ間隔
    public float maxJumpInterval = 3f; // 最大ジャンプ間隔
    public float changeDirectionInterval = 3f;
    [SerializeField]
    GameObject deathEffect;
    [SerializeField]
    GameObject Ban1;
    [SerializeField]
    GameObject Ban2;

    [SerializeField]
    public GameObject bulletPrefab; // 発射する弾のプレハブ
    public Transform firePoint; // 弾の発射位置

    private float nextFireTime;
    private float minFireInterval = 1f; // 最小発射間隔
    private float maxFireInterval = 3f; // 最大発射間隔
    
    private GameObject bulletInstance;

    [SerializeField]
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    EnemyCol enemyCol;
    private Rigidbody2D rb;

    private Camera mainCamera;

    private Vector2 initialPosition;

    public bool _isRendered = false;
    public bool _isRenderedThrow = false;
    public bool _isRenderedBird = false;
    private bool movingRight = true; // 右方向に移動しているかどうか
    private float timeSinceLastDirectionChange = 0f;
    GameManager gameManager;

    FollowCamera followCamera;
    EnemyVoice enemyVoice;

    RandomEnemyVoice randomEnemyVoice;

    [SerializeField]
    EnemyVoiceMob voiceMob;

    [SerializeField]
    anitiVoice AntiVoice;
    public enum EnemyType
    {
        nomal,
        side,
        vertical,
        Throw
    }
    [SerializeField] EnemyType enemyType;
    // Start is called before the first frame update
    void Start()
    {
        followCamera = FindObjectOfType<FollowCamera>();
        randomEnemyVoice = GetComponent<RandomEnemyVoice>();
        enemyVoice = FindObjectOfType<EnemyVoice>();
        initialPosition = transform.position;
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        if (enemyType == EnemyType.side)
        {
            StartCoroutine(RandomJump());
        }
        if(enemyType == EnemyType.Throw)
        {
            nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDead();
        SideMove();
        VerticalMove();
        Enemyvanish();
        ThrowEnemy();
        _isRendered = false;
        _isRenderedBird = false;
        _isRenderedThrow = false;
        OffCamera();
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cake" || collision.gameObject.tag == "RedCake")
        {
            EnemyLife -= 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Dead")
        {
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    private void EnemyDead()
    {
        if (EnemyLife <= 0)
        {
            if (enemyType == EnemyType.vertical && enemyVoice != null)
            {
                
                enemyVoice.audioSource.Stop();
                enemyVoice.BirdDeath();
            }
            if(enemyType == EnemyType.side)
            {
                voiceMob.audioSourceMattyo.Stop();
                randomEnemyVoice.PlayRandomSoundExternal();
                
            }
            if(enemyType == EnemyType.Throw)
            {
                AntiVoice.audioSourceAnti.Stop();
                SampleSoundManager.Instance.PlaySe(SeType.SE15);
            }
                Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
         
        }
    }
    private void ThrowEnemy()
    {

        if (enemyType == EnemyType.Throw )
            {
                if (this._isRenderedThrow== true && followCamera.OnCamera == true)
                    {
                AntiVoice.EnemyAntiVoiceOn();
                    }
                if (enemyCol != null)
                {
                    if (enemyCol.moveRight)
                    {
                        transform.Translate(Vector2.right * moveSpeed * 3 * Time.deltaTime);
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        transform.Translate(Vector2.left * moveSpeed *3 * Time.deltaTime);
                        spriteRenderer.flipX = false;
                    }
                }
                if (_isRenderedThrow == true)
                {
                    if (Time.time >= nextFireTime)
                     {
                            Shoot();

                         nextFireTime = Time.time + Random.Range(minFireInterval, maxFireInterval);
                     }
                }
             }
    }
    private void SideMove()
    {

        if (this._isRendered == true && enemyVoice != null && followCamera.OnCamera == true)
        {

            voiceMob.EnemyNomalVoiceOn();

        }
        if (enemyType == EnemyType.side && _isRendered)
        {
            if (enemyCol != null)
            {
                if (enemyCol.moveRight)
                {
                    transform.Translate(Vector2.right * moveSpeed*2 * Time.deltaTime);
                    spriteRenderer.flipX = true;
                }
                else
                {
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                    spriteRenderer.flipX = false;
                }
            }
        }
    }
    private  void VerticalMove()
    {
        if(enemyType == EnemyType.vertical)
        {
            if (this._isRenderedBird == true && enemyVoice != null && followCamera.OnCamera == true)
            {

                enemyVoice.BirdVoiceNomal();

            }
           
            float newY = initialPosition.y + Mathf.Sin(Time.time * moveSpeedVertical) * amplitude;

                float newX = transform.position.x - (movingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
                transform.position = new Vector3(newX, newY, transform.position.z);

                timeSinceLastDirectionChange += Time.deltaTime;
                if (timeSinceLastDirectionChange >= changeDirectionInterval)
                {

                    movingRight = !movingRight;
                    timeSinceLastDirectionChange = 0f;
                }
                if (movingRight)
                {
                    if (Ban1 != null && Ban2 != null)
                    {
                        Ban1.SetActive(true);
                        Ban2.SetActive(false);
                        spriteRenderer.flipX = false;
                    }
                }
                else if (!movingRight)
                {
                    if (Ban1 != null && Ban2 != null)
                    {
                        Ban2.SetActive(true);
                        Ban1.SetActive(false);
                        spriteRenderer.flipX = true;
                    }
                }
         }
        
        
    }
    private void Enemyvanish()
    {
        if(gameManager.EnemyAllDead == true)
        {
           
            Destroy(this.gameObject);
           
        }
       
    }
    IEnumerator RandomJump()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minJumpInterval, maxJumpInterval));
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }
   private void Shoot()
    {              
            bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);          
            Physics2D.IgnoreCollision(bulletInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());    
    }
    private void OffCamera()
    {
        Bounds spriteBounds = spriteRenderer.bounds;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);
        if (viewportPosition.x > 0 && viewportPosition.x < 1)
        {
            
            if(enemyType == EnemyType.vertical)
            {
                _isRenderedBird = true;
            }
            if (enemyType == EnemyType.side)
            {
                _isRendered = true;
            }
            if (enemyType == EnemyType.Throw)
            {
                _isRenderedThrow = true;
            }
        }
    }
}
