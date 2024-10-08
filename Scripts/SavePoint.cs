using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite newSprite;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Body" || other.gameObject.tag == "YellowBody")
        {
            spriteRenderer.sprite = newSprite;
           
        }
    }
}
