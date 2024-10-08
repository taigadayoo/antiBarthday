using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField]
    private Text heartText = null;
    private int oldHeartNum = 0;
    [SerializeField]
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //heartText = GetComponent<Text>();

        heartText.text = "Å~" + gameManager.PlayerLife;

    }

    // Update is called once per frame
    void Update()
    {
        if (oldHeartNum != gameManager.PlayerLife)
        {
            heartText.text = "Å~" + gameManager.PlayerLife;
            oldHeartNum = gameManager.PlayerLife;
        }
    }
}
