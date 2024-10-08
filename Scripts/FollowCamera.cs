using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    GameObject playerObj;
    Player player;
    Transform playerTransform;

    public float moveTime = 1.0f; // 移動にかかる時間
    public bool OnCamera = false;
    private Vector3 initialPosition; // カメラの初期位置
    private bool OneCamera = false;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        playerTransform = playerObj.transform;

        initialPosition = transform.position;

        // 移動先の目標位置をプレイヤーの位置に追従するように設定
        Vector3 targetPosition = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
        StartCoroutine(MoveCamera(targetPosition));
    }

    void LateUpdate()
    {
        if (player != null && OnCamera == true )
        {
            // 移動が完了した後もカメラを目標位置に維持
            MoveCamera();
           
        }
        if (player != null && OnCamera == true && !OneCamera)
        {
            player.enabled = true;
            OneCamera = true;
        }
            if (OnCamera == false)
        {
            player.enabled = false;
        }
       
    }

    void MoveCamera()
    {
        transform.position = new Vector3(playerTransform.position.x + 40, initialPosition.y, initialPosition.z);
    }

    IEnumerator MoveCamera(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        SampleSoundManager.Instance.PlaySe(SeType.SE13);

        yield return new WaitForSeconds(5.5f);

        SampleSoundManager.Instance.PlaySe(SeType.SE14);

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPosition, destination, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 移動が完了した後、カメラの位置を目標位置に設定する
        transform.position = destination;
        
        OnCamera = true;

       
    }
}