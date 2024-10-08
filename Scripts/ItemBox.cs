using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField]
    GameObject breakEffect;
    [SerializeField]
    GameObject CakeItem;
    [SerializeField]
    GameObject enemy;

    GameManager gameManager;

    private enum BoxName
    {
        WoodBox,
        NomalBox,
        WoodNomalBox,
        EnemyBox
    }

    [SerializeField]
    BoxName boxName;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemyvanish();
    }

    private void Enemyvanish()
    {
        if (gameManager.EnemyAllDead == true && (boxName == BoxName.NomalBox) || gameManager.EnemyAllDead == true && (boxName == BoxName.EnemyBox))
        {

            Destroy(this.gameObject);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxName == BoxName.NomalBox)
        {
            if (collision.gameObject.tag == "RedCake" || collision.gameObject.tag == "Cake")
            {

                Instantiate(breakEffect, this.transform.position, this.transform.rotation);
                Instantiate(CakeItem, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
        if (boxName == BoxName.WoodBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18);
                Instantiate(breakEffect, this.transform.position, this.transform.rotation);
                Instantiate(CakeItem, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);

            }
        }
        if (boxName == BoxName.WoodNomalBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18);
                Instantiate(breakEffect, this.transform.position, this.transform.rotation);
              
                Destroy(this.gameObject);
            }
        }
        if (boxName == BoxName.EnemyBox)
        {
            if (collision.gameObject.tag == "RedCake")
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE18);
                Instantiate(breakEffect, this.transform.position, this.transform.rotation);
                Instantiate(enemy, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
