using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private MainMenu mainMenu;

    private Image fadeScreen;
    void Start()
    {
        fadeScreen = GetComponent<Image>();

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
    
    IEnumerator FadeOutCoroutine(string sceneName, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            ChangeAlpha(t);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        float duration = 2f;
        float t = 0f;
        
        while (t < duration)
        {
            t += Time.deltaTime;
            ChangeAlpha(1f - (t / duration));
            yield return null;
        }

        FPController player = FindAnyObjectByType<FPController>();
        if (player != null) player.ResumePlayer();

        DialogueManager dialogueManager = FindAnyObjectByType<DialogueManager>();
        if (dialogueManager != null) dialogueManager.InitiateStartingDialogue();
    }

    private void ChangeAlpha(float a)
    {
        Color c = fadeScreen.color;
        c.a = a;
        fadeScreen.color = c;
    }

}
