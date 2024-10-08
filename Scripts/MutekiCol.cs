using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutekiCol : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    // Start is called before the first frame update
    PlayerAbility playerAbility;

    Player player;

    private bool OnSave = false;
    private bool OnSave2 = false;

    private void Start()
    {
        this.gameObject.SetActive(false);
        playerAbility = FindObjectOfType<PlayerAbility>();
        player = FindObjectOfType<Player>();

    }
    private void DamageDead()
    {
        gameManager.Dead();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            player.enabled = false;
            SampleSoundManager.Instance.PlaySe(SeType.SE5);
            gameManager.EnemyAllDead = true;
            playerAbility.enabled = false;
            Invoke("DamageDead", 1.5f);
        }
        if (other.gameObject.tag == "CakeItem")
        {
            {
                player.bulletNum = player.MaxCakeNum;
                SampleSoundManager.Instance.PlaySe(SeType.SE6);
            }

            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "SavePoint")
        {
            Vector3 respawnPosition = other.transform.position;
            respawnPosition.y += 18;
            gameManager.RespawnPoint = respawnPosition;
            if (OnSave == false)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16);
                SampleSoundManager.Instance.PlaySe(SeType.SE17);
            }
            OnSave = true;
        }
        if (other.gameObject.tag == "SavePoint2")
        {
            Vector3 respawnPosition = other.transform.position;
            respawnPosition.y += 18;
            gameManager.RespawnPoint = respawnPosition;
            if (OnSave2 == false)
            {
                SampleSoundManager.Instance.PlaySe(SeType.SE16);
                SampleSoundManager.Instance.PlaySe(SeType.SE17);
            }
            OnSave2 = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
