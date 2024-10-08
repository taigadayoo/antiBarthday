using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Player Player;
    public bool Down = false;
    public bool OneDamage = false;
    [SerializeField] private string sceneNameClear;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;


    private bool OnSave = false;
    private bool OnSave2 = false;


    public bool isHit = false;

    PlayerAbility playerAbility;

    private void Start()
    {
        playerAbility = FindObjectOfType<PlayerAbility>();
    }
    private void DamageDead()
    {
        gameManager.Dead();
        isHit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead"  && !isHit)
        {
            playerAbility.YellowOffSwitch = false;
             Player.enabled = false;
            SampleSoundManager.Instance.PlaySe(SeType.SE5);
            gameManager.EnemyAllDead = true;
            playerAbility.enabled = false;
            Invoke("DamageDead", 1.5f);
            this.enabled = false;
            isHit = true;
        }
        if (other.gameObject.tag == "CakeItem")
        {
            {
                Player.bulletNum = Player.MaxCakeNum;
                SampleSoundManager.Instance.PlaySe(SeType.SE6);

            }

            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "SavePoint")
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
        if (other.gameObject.tag == "Goal")
        {
            Player.enabled = false;
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
            SampleSoundManager.Instance.StopBgm();
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "DamageObject"  && !isHit)
        {
            isHit = true;
            this.enabled = false;
            SampleSoundManager.Instance.PlaySe(SeType.SE2);
            Down = true;
            Player.enabled = false;
            gameManager.EnemyAllDead = true;
            playerAbility.enabled = false;
            Player.animation.SetBool("Dead", true) ;
            if (OneDamage == false)
            {
                Player.OnDead();
                OneDamage = true;
                Invoke("DamageDead", 1.5f);
               
            }
        }
       
    }
   
}
