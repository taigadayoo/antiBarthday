using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeItem : MonoBehaviour
{
    private Player player;

    public float floatStrength = 400f; // �����̋��x
    public float floatSpeed = 1f; // �����̑��x
    [SerializeField]
    GameObject itemEffect;

    GameManager gameManager;

    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
        GameObject playerObject = GameObject.Find("Player");
        gameManager = FindObjectOfType<GameManager>();
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player�I�u�W�F�N�g��PlayerScript���A�^�b�`����Ă��܂���I");
            }
        }
        else
        {
            Debug.LogError("Player�I�u�W�F�N�g��������܂���ł����I");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Enemyvanish();
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatStrength, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Body" || collision.gameObject.tag == "YellowBody")
        {

            Instantiate(itemEffect, this.transform.position, this.transform.rotation);

        }
    }
    private void Enemyvanish()
    {
        if (gameManager.EnemyAllDead == true)
        {

            Destroy(this.gameObject);

        }

    }
}
