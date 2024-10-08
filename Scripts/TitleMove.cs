using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleMove : MonoBehaviour
{
    [SerializeField]
    GameObject AntiLogo;
    [SerializeField]
    GameObject ButtoLogo;
    [SerializeField]
    GameObject Push;
    [SerializeField]
    GameObject Japa;
    private Animator animator;

    private Color initialColor;
    // Start is called before the first frame update
    void Start()
    {
      
        animator = Push.GetComponent<Animator>();
        StartCoroutine(LogoAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LogoAnim()
    {
        yield return new WaitForSeconds(1.0f);
   
        ButtoLogo.transform.DOMove(new Vector3(-2.3f, 0.29f, 0), 2.0f);

        yield return new WaitForSeconds(0.9f);

        AntiLogo.transform.DOMove(new Vector3(-2.3f, 0.43f, 0), 2.0f);

        yield return new WaitForSeconds(1f);

        Japa.transform.DOMove(new Vector3(4.9f, -2.2f, 0), 0.5f);

        yield return new WaitForSeconds(0.2f);

        animator.SetBool("FadeOn",true);

        Push.transform.DOScale(new Vector3(1.02f, 1.02f, 1.02f), 2f) // オブジェクトを2倍に拡大
         .SetLoops(-1, LoopType.Yoyo); //
    }
}
