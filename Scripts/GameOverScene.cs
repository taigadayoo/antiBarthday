using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour
{
    SampleSoundManager sampleSoundManager;
    // Start is called before the first frame update
    void Start()
    {
       
            SampleSoundManager.Instance.PlaySe(SeType.SE4);
            SampleSoundManager.Instance.PlayBgm(BgmType.BGM2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
