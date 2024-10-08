using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBadGood : MonoBehaviour
{
    public float speed = 20f; // ’e‚Ì‘¬“x
    private Camera mainCamera;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private bool _isRendered = false;
    private bool OneBullet = false;
   public Vector3 initialDirection;
    private GameManager gameManager;
    [SerializeField]
    GameObject deathEffect;
 
 
    // Start is called before the first frame update
    void Start()
    {
      gameManager = gameManager = FindObjectOfType<GameManager>();
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(initialDirection * speed * Time.deltaTime);

        GoodVector();
        _isRendered = false;
        OffCamera();
        if(_isRendered == true)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Slorp" || collision.gameObject.tag == "DamageObject" || collision.gameObject.CompareTag("ItemBox"))
        {
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

       
    }
    private void OffCamera()
    {
        Bounds spriteBounds = spriteRenderer.bounds;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(spriteBounds.center);
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            _isRendered = true;
        }
    }
    public void OnRight()
    {
        initialDirection = Vector3.right;
        spriteRenderer.flipX = true;
    }
    public void OnLeft()
    {
        initialDirection = Vector3.left;
        spriteRenderer.flipX = false;
    }
    private void GoodVector()
    {     
            if (OneBullet == false)
            {
                if (gameManager.OnLeft == false)
                {
                      
                          OnRight();
                    OneBullet = true;
                }
                if (gameManager.OnLeft == true)
                {
                    OnLeft();
                    OneBullet = true;
                  
                }
            }      
    }
}
