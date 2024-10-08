using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCol : MonoBehaviour
{

    public bool moveRight = false; // ‰ŠúˆÚ“®•ûŒüi‰EŒü‚«j
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Damage" || other.gameObject.tag == "Wall" || other.gameObject.tag == "Ground" || other.gameObject.tag == "DamageObject" || other.gameObject.CompareTag("ItemBox"))
        {
            moveRight = !moveRight;
        }
    }
}
