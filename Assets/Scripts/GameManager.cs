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
    public AudioMixer SFX_mixer;
    public AudioMixer BGM_mixer;


    public Slider SFX_slider;
    public Slider BGM_slider;

    public GameObject optionCanvas;
    public GameObject optionPanel;

    [Header("Audio Clips")]
    public AudioClip Eat;           //done
    public AudioClip Grab;          //done
    public AudioClip CanDrop;       
    public AudioClip CoinDrop;
    public AudioClip KeyDrop;
    public AudioClip GravelDrop;
    public AudioClip SplatDrop;

    public AudioClip BGM1;
    public AudioClip BGM2;


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
        StartCoroutine(PlayLoop());
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

    #region AudioPlay
    
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

    public void AudioPlayEat()
    {
        if (SFX_AudioSource != null && Eat != null)
        {
            SFX_AudioSource.PlayOneShot(Eat);
        }
    }

    public void AudioPlayCandrop()
    {
        if (SFX_AudioSource != null && CanDrop != null)
        {
            SFX_AudioSource.PlayOneShot(CanDrop);
        }
    }
    public void AudioPlayCoindrop()
    {
        if (SFX_AudioSource != null && CoinDrop != null)
        {
            SFX_AudioSource.PlayOneShot(CoinDrop);
        }
    }
    public void AudioPlayKeydrop()
    {
        if (SFX_AudioSource != null && KeyDrop != null)
        {
            SFX_AudioSource.PlayOneShot(KeyDrop);
        }
    }
    public void AudioPlayGraveldrop()
    {
        if (SFX_AudioSource != null && GravelDrop != null)
        {
            SFX_AudioSource.PlayOneShot(GravelDrop);
        }
    }
    public void AudioPlaySplatdrop()
    {
        if (SFX_AudioSource != null && SplatDrop != null)
        {
            SFX_AudioSource.PlayOneShot(SplatDrop);
        }
    }
   #endregion
    
    public void AudioPlayBGM()
    {

    }
    
    public void SetSFXVolume()
    {

        float value = Mathf.Clamp(SFX_slider.value, 0.001f, 1f);
        SFX_mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void SetBGMVolume()
    {
        float value = Mathf.Clamp(BGM_slider.value, 0.001f, 1f);
        BGM_mixer.SetFloat("BGM", Mathf.Log10(value) * 20);

    }

    IEnumerator PlayLoop()
    {
        while (true)
        {
            BGM_AudioSource.clip = BGM1;
            BGM_AudioSource.Play();
            yield return new WaitForSeconds(BGM1.length);

            BGM_AudioSource.clip = BGM2;
            BGM_AudioSource.Play();
            yield return new WaitForSeconds(BGM2.length);
        }
    }
}
