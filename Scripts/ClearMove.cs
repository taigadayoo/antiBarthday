using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClearMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject migiMattyo;
    [SerializeField]
    GameObject leftMattyo;
    [SerializeField]
    GameObject migiMegane;
    [SerializeField]
    GameObject leftMegane;
    [SerializeField]
    GameObject migiBan;
    [SerializeField]
    GameObject leftBan;
    [SerializeField]
    GameObject Cake;
    [SerializeField]
    SpriteRenderer ClearMoji;
    [SerializeField]
    GameObject Title;
    void Start()
    {
        Title.SetActive(false);
        StartCoroutine(ClearAnime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ClearAnime()
    {
        yield return new WaitForSeconds(0.5f);

        Cake.transform.DOMove(new Vector3(0f, -3.58f, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);

        migiMattyo.transform.DOMove(new Vector3(7.57f, -6.0f, 0), 1.5f);
        leftMattyo.transform.DOMove(new Vector3(-7.57f, -6.0f, 0), 1.5f);

        yield return new WaitForSeconds(0.5f);
        migiMegane.transform.DOMove(new Vector3(8.32f, -0.74f, 0), 1.5f);
        leftMegane.transform.DOMove(new Vector3(-8.32f, -0.74f, 0), 1.5f);

        yield return new WaitForSeconds(0.5f);
        migiBan.transform.DOMove(new Vector3(3.12f, 2.49f, 0), 1.5f);
        leftBan.transform.DOMove(new Vector3(-3.12f, 2.49f, 0), 1.5f);

        yield return new WaitForSeconds(0.3f);
        ClearMoji.DOFade(1.0f, 1.0f);

        yield return new WaitForSeconds(1.0f);
        Title.SetActive(true);
    }
}
