using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCol : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject deathEffect;
    [SerializeField]
    Player Player;
    public bool Down = false;
    public bool OneDamage = false;


    [SerializeField] private string sceneNameClear;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    private bool OnSave = false;
    private bool OnSave2 = false;


    PlayerAbility playerAbility;
    Player player;

    private void Start()
    {
      
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
            Player.enabled = false;
            SampleSoundManager.Instance.PlaySe(SeType.SE5);
            gameManager.EnemyAllDead = true;
            playerAbility.enabled = false;
            Invoke("DamageDead", 1.5f);
        }
        if (other.gameObject.tag == "CakeItem")
        {
            {
                Player.bulletNum = Player.MaxCakeNum;
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
        if (other.gameObject.tag == "Goal")
        {
            Player.enabled = false;
            Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
            SampleSoundManager.Instance.StopBgm();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage" || collision.gameObject.tag == "EnemyBullet")
        {
            Instantiate(deathEffect, collision.transform.position, collision.transform.rotation);
            playerAbility.ability = PlayerAbility.Ability.nomal;
            playerAbility.YellowOffSwitch = true;
            playerAbility.StartYellowAbilityCooldown();
            playerAbility.lastTrueTime = Time.time;
            Destroy(collision.gameObject);
            playerAbility.YellowOn = false;
            playerAbility.nomalOn = true;
            player.ActivateInvincibility();
        }
        if(collision.gameObject.tag == "DamageObject")
        {
            playerAbility. YellowOffSwitch = true;
            playerAbility.StartYellowAbilityCooldown();
            playerAbility.lastTrueTime = Time.time;
            playerAbility.ability = PlayerAbility.Ability.nomal;
            player.ActivateInvincibility();
            playerAbility.nomalOn = true;
            playerAbility.YellowOn = false;
        }
    }
    
}
