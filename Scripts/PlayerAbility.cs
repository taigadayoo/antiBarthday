using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    // プレイヤーの能力に関連するゲームオブジェクト
    [SerializeField]
    GameObject Red; // 赤い能力のゲームオブジェクト
    [SerializeField]
    GameObject Blue; // 青い能力のゲームオブジェクト
    [SerializeField]
    GameObject Yellow; // 黄色い能力のゲームオブジェクト

    // 青い能力に関連する設定
    [SerializeField]
    private int BlueJump = 2; // 青い能力での最大ジャンプ回数
    [SerializeField]
    private float BlueMove = 45f; // 青い能力での移動速度
    [SerializeField]
    YellowCol yellowCol; // 黄色い能力に関連するコリジョン
    public float YellowCoolTime = 5f; // 黄色い能力のクールタイム

    private int NomalJump; // 通常時のジャンプ回数
    public float lastTrueTime; // 最後に有効だった時刻
    private float NomalMove; // 通常時の移動速度
    public bool YellowOffSwitch = false; // 黄色能力のオフスイッチ
    [SerializeField]
    AbilitySE abilitySE; // 能力に関連するサウンドエフェクト
    [SerializeField]
    GameObject yellowTimer; // 黄色能力のタイマーオブジェクト
    [SerializeField]
    YellowTimer yellowTimerScript; // 黄色能力のタイマー管理スクリプト
    [SerializeField]
    Player player; // プレイヤーオブジェクト
    [SerializeField]
    GameObject damage; // ダメージを示すオブジェクト
    [SerializeField]
    GameObject YellowCol; // 黄色能力のコリジョンオブジェクト
    [SerializeField]
    GameObject Mutekicol; // 無敵状態のコリジョンオブジェクト
    PlayerController playerController; // プレイヤーコントローラー

    public bool YellowOn = false; // 黄色能力が有効かどうか
    public bool nomalOn = true; // 通常モードが有効かどうか

    // 能力の列挙型
    public enum Ability
    {
        nomal, // 通常能力
        red,   // 赤い能力
        blue,  // 青い能力
        yellow // 黄色い能力
    }

    public Ability ability; // 現在の能力

    void Start()
    {
        // 初期設定
        playerController = GetComponent<PlayerController>();
        NomalJump = player.maxJumpCount; // 通常時のジャンプ回数を設定
        NomalMove = player.moveSpeed; // 通常時の移動速度を設定
        NomalMode(); // 通常モードを初期化
        yellowTimer.SetActive(false); // タイマーを非表示にする
        lastTrueTime = Time.time; // 現在の時刻を記録
    }

    // Update is called once per frame
    void Update()
    {
        // 能力の状態を更新
        YellowSwitch(); // 黄色能力の状態を更新
        AbilityChange(); // 能力の変更処理
        ColChange(); // コリジョンの状態を更新
    }

    public void NomalMode()
    {
        // 通常モードの設定
        BlueReset(); // 青い能力のリセット
        YellowReset(); // 黄色い能力のリセット
        ability = Ability.nomal; // 能力を通常に設定
        Red.SetActive(false); // 赤い能力を非表示
        Blue.SetActive(false); // 青い能力を非表示
        Yellow.SetActive(false); // 黄色い能力を非表示
    }

    private void redAbility()
    {
        // 赤い能力を発動
        abilitySE.SwitchToMode1(); // サウンドエフェクトを変更
        ability = Ability.red; // 能力を赤に設定
        Red.SetActive(true); // 赤い能力を表示
        Blue.SetActive(false); // 青い能力を非表示
        Yellow.SetActive(false); // 黄色い能力を非表示
        BlueReset(); // 青い能力のリセット
        YellowReset(); // 黄色い能力のリセット
    }

    private void blueAbility()
    {
        // 青い能力を発動
        abilitySE.SwitchToMode2(); // サウンドエフェクトを変更
        player.maxJumpCount = BlueJump; // 最大ジャンプ回数を設定
        player.moveSpeed = BlueMove; // 移動速度を設定
        player.currentJumpCount = BlueJump; // 現在のジャンプ回数を設定
        ability = Ability.blue; // 能力を青に設定
        Blue.SetActive(true); // 青い能力を表示
        Red.SetActive(false); // 赤い能力を非表示
        Yellow.SetActive(false); // 黄色い能力を非表示
        YellowReset(); // 黄色い能力のリセット
    }

    private void yellowAbility()
    {
        // 黄色い能力を発動
        damage.SetActive(false); // ダメージを無効にする
        YellowCol.SetActive(true); // 黄色コリジョンを有効にする
        abilitySE.SwitchToMode3(); // サウンドエフェクトを変更
        ability = Ability.yellow; // 能力を黄色に設定
        Yellow.SetActive(true); // 黄色い能力を表示
        Red.SetActive(false); // 赤い能力を非表示
        Blue.SetActive(false); // 青い能力を非表示
        BlueReset(); // 青い能力のリセット
        YellowOn = true; // 黄色能力を有効にする
        nomalOn = false; // 通常モードを無効にする
    }

    private void BlueReset()
    {
        // 青い能力をリセット
        player.maxJumpCount = NomalJump; // 最大ジャンプ回数を通常に設定
        player.currentJumpCount = NomalJump; // 現在のジャンプ回数を通常に設定
        player.moveSpeed = NomalMove; // 移動速度を通常に設定
    }

    private void YellowReset()
    {
        // 黄色い能力をリセット
        damage.SetActive(true); // ダメージを有効にする
        YellowCol.SetActive(false); // 黄色コリジョンを無効にする
        YellowOn = false; // 黄色能力を無効にする
        nomalOn = true; // 通常モードを有効にする
    }


private void AbilityChange()
    {
        // プレイヤーが重力反転ボタンを押しているか、右クリックで通常モードの場合
        if (playerController.IsGravityReversePressed && ability == Ability.nomal || Input.GetMouseButtonDown(1) && ability == Ability.nomal)
        {
            // 赤い能力を発動
            redAbility();
        }
        // プレイヤーが重力反転ボタンを押しているか、右クリックで黄色能力の場合
        else if (playerController.IsGravityReversePressed && ability == Ability.yellow || Input.GetMouseButtonDown(1) && ability == Ability.yellow)
        {
            // 赤い能力を発動
            redAbility();
        }
        // プレイヤーが重力反転ボタンを押しているか、右クリックで赤い能力の場合
        else if (playerController.IsGravityReversePressed && ability == Ability.red || Input.GetMouseButtonDown(1) && ability == Ability.red)
        {
            // 青い能力を発動
            blueAbility();
        }
        // プレイヤーが重力反転ボタンを押しているか、右クリックで青い能力で、黄色オフスイッチが無効の場合
        else if (playerController.IsGravityReversePressed && ability == Ability.blue && YellowOffSwitch == false || Input.GetMouseButtonDown(1) && ability == Ability.blue && YellowOffSwitch == false)
        {
            // 黄色い能力を発動
            yellowAbility();
        }
        // プレイヤーが重力反転ボタンを押しているか、右クリックで青い能力で、黄色オフスイッチが有効の場合
        else if (playerController.IsGravityReversePressed && ability == Ability.blue && YellowOffSwitch == true || Input.GetMouseButtonDown(1) && ability == Ability.blue && YellowOffSwitch == true)
        {
            // 赤い能力を発動
            redAbility();
        }
        // 通常能力の場合
        else if (ability == Ability.nomal)
        {
            // 通常モードを発動
            NomalMode();
        }
    }

    public void YellowSwitch()
    {
        // 黄色オフスイッチが有効な場合
        if (YellowOffSwitch)
        {
            // 黄色のタイマーを表示
            yellowTimer.SetActive(true);

            // 黄色のクールタイムが経過した場合
            if (Time.time - lastTrueTime >= YellowCoolTime)
            {
                // 黄色オフスイッチを無効にし、最後のタイムを更新
                YellowOffSwitch = false;
                lastTrueTime = Time.time;
            }
        }
        else
        {
            // 黄色のタイマーを非表示
            yellowTimer.SetActive(false);
        }
    }

    public void StartYellowAbilityCooldown()
    {
        // 黄色のクールタイムを開始
        yellowTimerScript.StartTimer(YellowCoolTime);
    }

    private void ColChange()
    {
        // 通常モードで無敵でなく、黄色能力が無効な場合
        if (nomalOn == true && player.isInvincible == false && YellowOn == false)
        {
            // ダメージオブジェクトを有効にし、他を無効にする
            damage.SetActive(true);
            YellowCol.SetActive(false);
            Mutekicol.SetActive(false);
        }
        // 通常モードでなく、無敵でなく、黄色能力が有効な場合
        else if (!nomalOn && !player.isInvincible && YellowOn)
        {
            // ダメージオブジェクトを無効にし、黄色コリジョンを有効にする
            damage.SetActive(false);
            YellowCol.SetActive(true);
            Mutekicol.SetActive(false);
        }
        // 通常モードであり、無敵で、黄色能力が無効な場合
        else if (nomalOn && player.isInvincible && !YellowOn)
        {
            // 他を無効にし、無敵コリジョンを有効にする
            damage.SetActive(false);
            YellowCol.SetActive(false);
            Mutekicol.SetActive(true);
        }
    }
}
