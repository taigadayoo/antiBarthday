using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeBer : MonoBehaviour
{
    [SerializeField] private Image _hpBarcurrent;
    [SerializeField] Player player;
   private float currentBullet;

    void Awake()
    {
        currentBullet = player.bulletNum;
    }
    void Update()
    {
        _hpBarcurrent.fillAmount = player.bulletNum / currentBullet;
    }
}
