using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TransitionManager : MonoBehaviour
{
    private MainMenu mainMenu;

    [SerializeField] private Image fadeScreen;

    void Start()
    {
        ChangeAlpha(0f);

        mainMenu = FindAnyObjectByType<MainMenu>();

        if(SceneManager.GetActiveScene().name.StartsWith("Level")) // If you're not in a menu
        {
            FPController player = FindAnyObjectByType<FPController>();
            if (player != null) player.PausePlayer();

            StartCoroutine(FadeIn());
        }
    }

    public void FadeOut (string sceneName, float duration)
    {
        StartCoroutine(FadeOutCoroutine(sceneName, duration));
    }

    public void FadeOutMainMenu(float duration)
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        StartCoroutine(FadeOutCoroutine("Main Menu", duration));
    }

    public void FadeOutRestart(float duration)
    {
        if(Time.timeScale == 0) Time.timeScale = 1;
        FindAnyObjectByType<Player>().PauseGame();

        StartCoroutine(FadeOutCoroutine(SceneManager.GetActiveScene().name, duration));
    }

    public void FadeOutNextLevel(float duration)
    {
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
        {
            StartCoroutine(FadeOutCoroutine((SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex)).name, 3f));
        }
        else
        {
            //Bring to "Thank you for playing our game!" page
            FadeOut("Main Menu", duration);
        }
    }
    
    IEnumerator FadeOutCoroutine(string sceneName, float duration)
    {
        Debug.Log("FadeOut");
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            ChangeAlpha(t);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator FadeIn()
    {
        FPController player = FindAnyObjectByType<FPController>();
        float duration = 2f;
        float t = 0f;
        
        while (t < duration)
        {
            t += Time.deltaTime;
            ChangeAlpha(1f - (t / duration));
            yield return null;

            if (player != null) player.PausePlayer();
        }

        if (player != null) player.ResumePlayer();

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
        if (dialogueManager != null) dialogueManager.InitiateStartingDialogue();
    }

    private void ChangeAlpha(float a)
    {
        if (fadeScreen == null) return;

        Color c = fadeScreen.color;
        c.a = a;
        fadeScreen.color = c;
    }

}
