using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    TitleSound1 titleSound;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        titleSound = FindObjectOfType<TitleSound1>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || playerController.IsTitlePressed)
        {
            titleSound.audioSource.Stop();
            Initiate.Fade(sceneName, fadeColor, fadeSpeed);
        }
    }
}
