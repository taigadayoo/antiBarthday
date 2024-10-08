using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public int PlayerLife = 3;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    public Vector3 RespawnPoint;
    [SerializeField] private string sceneName;
    [SerializeField] private string sceneNameTitle;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;
    [SerializeField] Player player;
    [SerializeField]
    Damage damage;
    [SerializeField]
    SpawnManager spawnManager;
    public bool EnemyAllDead = false;

    private Transform playerTransform;
    private Transform enemyTransform;
    public Vector3 playerPosition;
    public Vector3 enemyPosition;
    public bool OnLeft = false;

    private bool OnBGM = false;
    PlayerAbility playerAbility;

    [SerializeField]
    PlayerController playerController;

    FollowCamera followCamera;

    private void Awake()
    {
        followCamera = FindObjectOfType<FollowCamera>();
    }
    void Start()
    {
        if (followCamera.OnCamera)
        {
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = GameObject.Find("EnemyThrow").transform;
        playerAbility = FindObjectOfType<PlayerAbility>();
    }


    void Update()
    {

        if (followCamera.OnCamera && OnBGM == false)
        {
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
            OnBGM = true;
        }

        GameOver();
        if (playerTransform != null)
        {
            playerPosition = playerTransform.position;
        }
        if (enemyTransform != null)
        {
            enemyPosition = enemyTransform.position;
        }
        LeftorRight();

        if (Input.GetKeyDown(KeyCode.Escape) || playerController.IsResetPressed)
        {
            Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed);
            SampleSoundManager.Instance.StopBgm();
        }
    }
    public void Dead()
    {
        PlayerLife -= 1;
        if (PlayerLife != 0)
        {
            damage.enabled = true;
           playerAbility.YellowOffSwitch = false;
           playerAbility.lastTrueTime = Time.time;
            player.OffDead();
            player.enabled = true;
            Player.transform.position = RespawnPoint;
            player.bulletNum = player.MaxCakeNum;
            player.OffJump();
            player.OffThrow();
            damage.OneDamage = false;
            EnemyAllDead = false;
            spawnManager.RespawnAll();
            playerAbility.enabled = true;
            playerAbility.NomalMode();
        }
        else if(PlayerLife == 0)
        {
            Destroy(Player);
        }
    }
    private void GameOver()
    {
        if (PlayerLife == 0)
        {
            Initiate.Fade(sceneName, fadeColor, fadeSpeed);
            SampleSoundManager.Instance.StopBgm();
        }
    }
    private void LeftorRight()
    {
        if(enemyPosition.x  > playerPosition.x)
        {
           
            OnLeft = true;
        }
        if (enemyPosition.x < playerPosition.x)
        {
            OnLeft = false;
          
        }
    }
}
