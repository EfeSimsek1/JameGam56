using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (SFX_slider != null) SFX_slider.value = 1;
        if (BGM_slider != null) BGM_slider.value = 1;
        if (optionPanel != null) optionPanel.gameObject.SetActive(false);
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

    public void SetSFXVolume()
    {

        float value = Mathf.Clamp(SFX_slider.value, 0.001f, 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void SetBGMVolume()
    {
        float value = Mathf.Clamp(BGM_slider.value, 0.001f, 1f);
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);

    }
}
