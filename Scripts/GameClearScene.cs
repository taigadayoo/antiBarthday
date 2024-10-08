using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearScene : MonoBehaviour
{
   
    [SerializeField] private string sceneNameTitle;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    public AudioSource audioSource;
    public AudioSource audioSourceBGM;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource.Play();
        audioSourceBGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.T) || playerController.IsTitlePressed)
        {
            audioSource.Stop();
            audioSourceBGM.Stop();
            Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed);
        }

    }

}
