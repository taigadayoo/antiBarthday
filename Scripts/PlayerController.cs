using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    public float Horizontal { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsGravityReversePressed { get; private set; }
    public bool IsPausePressed { get; set; }

    public bool IsNextPressed { get; set; }

    public bool IsRetryPressed { get; set; }

    public bool IsTitlePressed { get; set; }

    public bool IsSelectPressed { get; set; }

    public bool IsResetPressed { get; set; }

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction modeChange;
    private InputAction pauseAction;
    private InputAction nextAction;
    private InputAction titleAction;
    private InputAction Atack;
    private InputAction Retry;
    private InputAction Reset;

    private void Awake()
    {
        // アクションの参照を取得
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        modeChange = inputActions.FindAction("ModeChange");
        titleAction = inputActions.FindAction("Title");
        Atack = inputActions.FindAction("Atack");
        Retry = inputActions.FindAction("Retry");
        Reset = inputActions.FindAction("Reset");
    }

    private void OnEnable()
    {
        // アクションを有効化
        moveAction.Enable();
        jumpAction.Enable();
        modeChange.Enable();
        titleAction.Enable();
        Atack.Enable();
        Retry.Enable();
        Reset.Enable();
    }

    private void OnDisable()
    {
        // アクションを無効化
        moveAction.Disable();
        jumpAction.Disable();
        modeChange.Disable();
        titleAction.Disable();
        Atack.Disable();
        Retry.Disable();
        Reset.Disable();
    }

    private void Update()
    {
        // 他のアクションの状態を更新
        IsJumpPressed = jumpAction.triggered;
        IsGravityReversePressed = modeChange.triggered;
        IsTitlePressed = titleAction.triggered;
        IsSelectPressed = Atack.triggered;
        IsRetryPressed = Retry.triggered;
        IsResetPressed = Reset.triggered;

        // Vector2型の値として入力を読み取る
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Horizontal = moveInput.x; // X軸（水平方向）の値を取得
    }
}
