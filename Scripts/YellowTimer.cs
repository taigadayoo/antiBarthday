using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField]
    PlayerAbility playerAbility;

    private float currentTime = 0f;

    void Start()
    {
        
    }
    private void UpdateTimerUI()
    {
        if (uiFill != null)
        {
            float elapsedTime = playerAbility.YellowCoolTime - currentTime; // 経過時間
            float fillAmount = elapsedTime / playerAbility.YellowCoolTime; // タイマーの塗りつぶし割合
            uiFill.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }
    public void StartTimer(float cooldown)
    {
       
        playerAbility.YellowCoolTime = cooldown;
        currentTime = playerAbility.YellowCoolTime;
        UpdateTimerUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (playerAbility.YellowOffSwitch)
        {
            
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
               
            }
            UpdateTimerUI();
        }
    }
}
