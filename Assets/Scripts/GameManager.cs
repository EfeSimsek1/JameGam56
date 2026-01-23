using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource SFX_AudioSource;
    public AudioSource BGM_AudioSource;

    public Slider SFX_slider;
    public Slider BGM_slider;

    public GameObject optionCanvas;
    public GameObject optionPanel;

    [Header("Audio Clips")]
    public AudioClip Grab;
    public AudioClip CanDrop;

    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SFX_slider.value = 1;
        BGM_slider.value = 1;
        optionPanel.gameObject.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void AudioPlayCanDrop()
    {
        if (SFX_AudioSource != null && CanDrop != null)
        {
            SFX_AudioSource.PlayOneShot(CanDrop);
        }
    }
    public void AudioPlayGrab()
    {
        if (SFX_AudioSource != null && Grab != null)
        {
            SFX_AudioSource.PlayOneShot(Grab);
        }
    }

    public void SetSFXVolume ()
    {

        float value = Mathf.Clamp(SFX_slider.value, 0.001f, 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void SetBGMVolume()
    {
        float value = Mathf.Clamp(BGM_slider.value, 0.001f, 1f);
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        
    }

    /*public void FadeAndLoad(string sceneName, float duration)
    {
        StartCoroutine(Fader(sceneName, duration));
    }

    IEnumerator Fader(string sceneName, float duration)
    {
        float t = 0;
        Color c = faderImage.color;
        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = t / duration;
            faderImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeOut()
    {
        Debug.Log("coroutine started");

        float duration = 2f;
        float t = 0;
        Color c = faderImage.color;
        while (t < duration)
        {
            t += Time.deltaTime;
            c.a = 1f - (t / duration);
            faderImage.color = c;
            yield return null;
        }

        FindAnyObjectByType<FPController>().ResumePlayer();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Entered scene: " + scene.name);

        // Scene-specific setup here
        if (SceneManager.GetActiveScene().name.StartsWith("Level"))
        {
            StartCoroutine(FadeOut());
            FindAnyObjectByType<FPController>().PausePlayer();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }*/



}
