using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))] // このコンポーネントはCameraコンポーネントが必要であることを示す
public class FollowCamera : MonoBehaviour
{
    GameObject playerObj; // プレイヤーオブジェクトの参照
    Player player; // プレイヤーのスクリプトの参照
    Transform playerTransform; // プレイヤーのTransform

    public float moveTime = 1.0f; // カメラが移動するのにかかる時間
    public bool OnCamera = false; // カメラがプレイヤーに追従しているかどうかのフラグ
    private Vector3 initialPosition; // カメラの初期位置
    private bool OneCamera = false; // カメラの動作が一度だけ行われるかを判定するフラグ

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player"); // "Player"タグを持つオブジェクトを検索
        player = playerObj.GetComponent<Player>(); // プレイヤーコンポーネントを取得
        playerTransform = playerObj.transform; // プレイヤーのTransformを取得

        initialPosition = transform.position; // カメラの初期位置を保存

        // 移動先の目標位置をプレイヤーの位置に追従するように設定
        Vector3 targetPosition = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
        StartCoroutine(MoveCamera(targetPosition)); // カメラ移動のコルーチンを開始
    }

    void LateUpdate()
    {
        if (player != null && OnCamera == true)
        {
            // カメラを目標位置に維持
            MoveCamera();
        }
        if (player != null && OnCamera == true && !OneCamera)
        {
            player.enabled = true; // プレイヤーの動作を有効にする
            OneCamera = true; // 動作を一度だけ行うためのフラグを設定
        }
        if (OnCamera == false)
        {
            player.enabled = false; // プレイヤーの動作を無効にする
        }
    }

    void MoveCamera()
    {
        // プレイヤーの位置に応じてカメラの位置を更新
        transform.position = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
    }

    IEnumerator MoveCamera(Vector3 destination)
    {
        Vector3 startPosition = transform.position; // 現在のカメラの位置
        float elapsedTime = 0f; // 経過時間の初期化

        SampleSoundManager.Instance.PlaySe(SeType.SE13); // SE13を再生

        yield return new WaitForSeconds(5.5f); // 5.5秒待機

        SampleSoundManager.Instance.PlaySe(SeType.SE14); // SE14を再生

        // カメラを目的地まで移動させる
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, destination, (elapsedTime / moveTime)); // 線形補間
            elapsedTime += Time.deltaTime; // 経過時間を更新
            yield return null; // 次のフレームまで待機
        }

        // 移動が完了した後、カメラの位置を目標位置に設定する
        transform.position = destination;

        OnCamera = true; // カメラが移動したことを示すフラグを設定
    }
}
