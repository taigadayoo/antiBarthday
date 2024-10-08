using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverAnime : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject gameOver;
    [SerializeField]
    GameObject Japa;
    [SerializeField]
    GameObject RetryMoji;

    [SerializeField]
    public SpriteRenderer spriteRenderer; // アルファを変更したいSpriteRendererを参照
    public float fadeInDuration = 1f; // フェードインにかかる時間

    void Start()
    {
        StartCoroutine(JapaAnim());
        RetryMoji.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator JapaAnim()
    {
        yield return new WaitForSeconds(0.3f);

        gameOver.transform.DOMove(new Vector3(0.55f, -1.49f, 0), 2.0f);

        yield return new WaitForSeconds(1.0f);

        spriteRenderer.DOFade(0.6f, fadeInDuration);

        yield return new WaitForSeconds(0.8f);
        RetryMoji.SetActive(true);
    }
}
