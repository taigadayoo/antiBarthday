using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    GameManager gameManager;
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
        if (gameManager.EnemyAllDead == true)
        {

            Destroy(this.gameObject);

        }

    }
}
