using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    public enum Charactor
    {
        Player,
        Enemy
    }
    public float MoveSpeed = 20.0f;


    private Camera mainCamera;
    private Charactor charactor;
    private bool RightLeft = true;
    private SpriteRenderer spriteRenderer;
    private bool _isRendered = false;

    private Vector3 initialDirection;
    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        initialDirection = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        BulletAttack();
        _isRendered = false;
        OffCamera();
    }
    private void BulletAttack()
    {
        if (player != null)
        {

            if (player.spriteRenderer.flipX == true && RightLeft)
            {
                initialDirection = Vector3.left;
                RightLeft = false;
            }
            else if (player.spriteRenderer.flipX == false && RightLeft)
            {
                initialDirection = Vector3.right;
                RightLeft = false;
            }

            transform.Translate(initialDirection * MoveSpeed * Time.deltaTime);

            if (_isRendered == true)
            {
                Destroy(gameObject);
            }
        }

    }
    private void OnBecameInvisible()
    {
        _isRendered = true;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Wall"  || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "DamageObject" || collision.gameObject.CompareTag("Slorp") )
        {
            Destroy(this.gameObject);
        }
    }
}
