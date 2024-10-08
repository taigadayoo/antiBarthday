using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Red;
    [SerializeField]
    GameObject Blue;
    [SerializeField]
    GameObject Yellow;
   
  
    [SerializeField]
    private int BlueJump = 2;
    [SerializeField]
    private float BlueMove = 45f;
    [SerializeField]
    YellowCol yellowCol;
    public float YellowCoolTime = 5f;
 
    private int NomalJump;
    public float lastTrueTime;
    private float NomalMove;
    public bool YellowOffSwitch = false;
    [SerializeField]
    AbilitySE abilitySE;
    [SerializeField]
    GameObject yellowTimer;
    [SerializeField]
    YellowTimer yellowTimerScript;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject damage;
    [SerializeField]
    GameObject YellowCol;
    [SerializeField]
    GameObject Mutekicol;
    PlayerController playerController;

    public bool YellowOn = false;
    public bool nomalOn = true;
    public enum Ability
    {
        nomal,
        red,
        blue,
        yellow
    }

   
  public  Ability ability;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        NomalJump = player.maxJumpCount;
        NomalMove = player.moveSpeed;
        NomalMode();
        yellowTimer.SetActive(false);
        lastTrueTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        YellowSwitch();
        AbilityChange();
        ColChange();
    }

    public void NomalMode()
        {
        BlueReset();
        YellowReset();
        ability = Ability.nomal;
        Red.SetActive(false);
        Blue.SetActive(false);
        Yellow.SetActive(false);
         }
    private void redAbility()
    {
        abilitySE.SwitchToMode1();
        ability = Ability.red;
        Red.SetActive(true);
        Blue.SetActive(false);
        Yellow.SetActive(false);
        BlueReset();
        YellowReset();
    }
    private void blueAbility()
    {
        abilitySE.SwitchToMode2();
        player.maxJumpCount = BlueJump;
        player.moveSpeed = BlueMove;
        player.currentJumpCount = BlueJump;
        ability = Ability.blue;
        Blue.SetActive(true);
        Red.SetActive(false);
        Yellow.SetActive(false);
        YellowReset();
    }
    private void yellowAbility()
    {
        damage.SetActive(false);
        YellowCol.SetActive(true);
        abilitySE.SwitchToMode3();
        ability = Ability.yellow;
        Yellow.SetActive(true);
        Red.SetActive(false);
        Blue.SetActive(false);
        BlueReset();
        YellowOn = true;
        nomalOn = false;
    }
    private void BlueReset()
        {
             player.maxJumpCount = NomalJump;
        player.currentJumpCount = NomalJump;
            player.moveSpeed = NomalMove;
        }
    private void YellowReset()
    {
        damage.SetActive(true);
        YellowCol.SetActive(false);
        YellowOn = false;
        nomalOn = true;

    }

    private void AbilityChange()
    {
        if (playerController.IsGravityReversePressed && ability == Ability.nomal || Input.GetMouseButtonDown(1) && ability == Ability.nomal )
        {
            redAbility();
           

        }
        else if (playerController.IsGravityReversePressed && ability == Ability.yellow || Input.GetMouseButtonDown(1) && ability == Ability.yellow)
        {
            redAbility();
         

        }
        else if (playerController.IsGravityReversePressed && ability == Ability.red || Input.GetMouseButtonDown(1) && ability == Ability.red)
        {
            blueAbility();
            

        }
        else if (playerController.IsGravityReversePressed && ability == Ability.blue && YellowOffSwitch == false || Input.GetMouseButtonDown(1) && ability == Ability.blue && YellowOffSwitch == false)
        {
            yellowAbility();
         
        }
        else if (playerController.IsGravityReversePressed && ability == Ability.blue && YellowOffSwitch == true || Input.GetMouseButtonDown(1) && ability == Ability.blue && YellowOffSwitch == true)
        {
            redAbility();

        }
        else if(ability == Ability.nomal)
        {
            NomalMode();
        }
    }
    public void YellowSwitch()
    {
        if (YellowOffSwitch)
        {
            yellowTimer.SetActive(true);
           
            if (Time.time - lastTrueTime >= YellowCoolTime)
            {
              
                YellowOffSwitch = false;
                lastTrueTime = Time.time;
            }
        }else
        {
            yellowTimer.SetActive(false);
        }
    }
    public void StartYellowAbilityCooldown()
    {
        yellowTimerScript.StartTimer(YellowCoolTime);
    }
    private void ColChange()
    {
        if(nomalOn == true && player.isInvincible == false && YellowOn == false)
        {
            damage.SetActive(true);
            YellowCol.SetActive(false);
            Mutekicol.SetActive(false);
        }
        else if(!nomalOn && !player.isInvincible && YellowOn)
        {
            damage.SetActive(false);
            YellowCol.SetActive(true);
            Mutekicol.SetActive(false);
        }
        else if (nomalOn && player.isInvincible && !YellowOn)
        {
            damage.SetActive(false);
            YellowCol.SetActive(false);
            Mutekicol.SetActive(true);
        }
    }
    //void ChangeColor()
    //{

    //    switch (ability)
    //    {
    //        case Ability.nomal:
    //            redAbility();
    //            break;
    //        case Ability.red:
    //            blueAbility();
    //            break;
    //        case Ability.blue:
    //            yellowAbility();
    //            break;
    //        case Ability.yellow:
    //            redAbility();
    //            break;
    //    }

    //}
}
