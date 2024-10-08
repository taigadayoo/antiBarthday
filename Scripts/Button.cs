using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite Onrever;

    ReverDoor reverDoor;
    // Start is called before the first frame update
    void Start()
    {
        reverDoor = FindObjectOfType<ReverDoor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RedCake" || collision.gameObject.tag == "Cake")
        {
            reverDoor.enabled = true;
            spriteRenderer.sprite = Onrever;
            SampleSoundManager.Instance.PlaySe(SeType.SE20);
            SampleSoundManager.Instance.PlaySe(SeType.SE19);
        }
    }
}
