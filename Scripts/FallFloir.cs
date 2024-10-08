using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloir : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody" || collision.gameObject.tag == "Player")
        {
            SampleSoundManager.Instance.PlaySe(SeType.SE22);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
