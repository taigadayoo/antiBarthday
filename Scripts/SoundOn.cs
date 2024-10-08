using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SampleSoundManager.Instance.PlayBgm(BgmType.BGM1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
