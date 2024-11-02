using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public GameObject backgroundPrefab; // 生成する背景のプレハブ
    public float backgroundWidth; // 背景の幅
    public int numberOfBackgrounds = 3; // 表示する背景の数
    public Transform cameraTransform; // カメラのトランスフォーム

    private GameObject[] backgrounds; // 背景の配列
    private float backgroundStartX; // 背景の開始X座標
    private float backgroundEndX; // 背景の終了X座標

    // 初期化処理
    void Start()
    {
        backgrounds = new GameObject[numberOfBackgrounds]; // 背景配列の初期化
        backgroundStartX = cameraTransform.position.x - (backgroundWidth * numberOfBackgrounds / 2f); // 背景の開始位置を計算
        backgroundEndX = cameraTransform.position.x + (backgroundWidth * numberOfBackgrounds / 2f); // 背景の終了位置を計算

        // 背景オブジェクトを生成して配置
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            GameObject background = Instantiate(backgroundPrefab, new Vector3(backgroundStartX + i * backgroundWidth, transform.position.y, transform.position.z), Quaternion.identity);
            backgrounds[i] = background;
        }
    }

    // 毎フレーム更新処理
    void Update()
    {
        // カメラの位置が背景の終了位置より右に行ったら、最も左にある背景を右端に移動させる
        if (cameraTransform.position.x > backgroundEndX)
        {
            MoveBackgroundRight();
        }

        // カメラの位置が背景の開始位置より左に行ったら、最も右にある背景を左端に移動させる
        if (cameraTransform.position.x < backgroundStartX)
        {
            MoveBackgroundLeft();
        }
    }

    // 背景を右にループ移動させる
    void MoveBackgroundRight()
    {
        // 最も左の背景を右端に移動
        backgrounds[0].transform.position = new Vector3(backgrounds[backgrounds.Length - 1].transform.position.x + backgroundWidth, backgrounds[0].transform.position.y, backgrounds[0].transform.position.z);

        // 配列を更新して背景の順序を維持
        GameObject temp = backgrounds[0];
        for (int i = 1; i < backgrounds.Length; i++)
        {
            backgrounds[i - 1] = backgrounds[i];
        }
        backgrounds[backgrounds.Length - 1] = temp;

        // 背景の開始位置と終了位置を更新
        backgroundStartX += backgroundWidth;
        backgroundEndX += backgroundWidth;
    }

    // 背景を左にループ移動させる
    void MoveBackgroundLeft()
    {
        // 最も右の背景を左端に移動
        backgrounds[backgrounds.Length - 1].transform.position = new Vector3(backgrounds[0].transform.position.x - backgroundWidth, backgrounds[backgrounds.Length - 1].transform.position.y, backgrounds[backgrounds.Length - 1].transform.position.z);

        // 配列を更新して背景の順序を維持
        GameObject temp = backgrounds[backgrounds.Length - 1];
        for (int i = backgrounds.Length - 1; i > 0; i--)
        {
            backgrounds[i] = backgrounds[i - 1];
        }
        backgrounds[0] = temp;

        // 背景の開始位置と終了位置を更新
        backgroundStartX -= backgroundWidth;
        backgroundEndX -= backgroundWidth;
    }
}
