using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    GameObject itemEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody")
        {

            Instantiate(itemEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            SampleSoundManager.Instance.PlaySe(SeType.SE10);
        }
    }
}
