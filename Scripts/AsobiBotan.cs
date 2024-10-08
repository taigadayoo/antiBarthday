using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsobiBotan : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string sceneNameClear;
    [SerializeField] private string sceneNameTitle;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    AudioSource audioSource;
    enum Scene
    {
        Asobi,
        Title
    }
    [SerializeField]
    Scene scene;

    PlayerController playerController;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsJumpPressed && scene == Scene.Asobi)
        {
            OnTitle();
        }
        if (playerController.IsResetPressed && scene == Scene.Title)
        {

            OnAsobi();
        }
    }

   public void OnAsobi()
    {
        audioSource.Play();
        Initiate.Fade(sceneNameClear, fadeColor, fadeSpeed);
       
    }
    public void OnTitle()
    {
        audioSource.Play();
        Initiate.Fade(sceneNameTitle, fadeColor, fadeSpeed);
       
    }
}
