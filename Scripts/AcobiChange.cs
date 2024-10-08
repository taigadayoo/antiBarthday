using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcobiChange : MonoBehaviour
{
    public GameObject[] objectsToSwitch; // 切り替えるオブジェクトの配列
    private int currentIndex = 0; // 現在のオブジェクトのインデックス

    PlayerController PlayerController;

    void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        // 最初のオブジェクトのみを表示する
        ShowObject(currentIndex);
    }

    void Update()
    {
        // マウスの左クリックが押されたら次のオブジェクトを表示する
        if (Input.GetKeyDown(KeyCode.Space)|| PlayerController.IsGravityReversePressed)
        {
            currentIndex = (currentIndex + 1) % objectsToSwitch.Length;
            ShowObject(currentIndex);
        }
    }

    void ShowObject(int index)
    {
        // すべてのオブジェクトを非表示にする
        foreach (GameObject obj in objectsToSwitch)
        {
            obj.SetActive(false);
        }

        // 指定されたインデックスのオブジェクトを表示する
        objectsToSwitch[index].SetActive(true);
    }
}
