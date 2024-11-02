using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 100f;
    [SerializeField]
    GameObject deathEffect;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "RedCake")
        {
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);//回復アイテムに触れた際に残弾を回復
            Destroy(this.gameObject);
        }
    }
}
