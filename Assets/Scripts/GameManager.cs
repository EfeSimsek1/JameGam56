using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource SFX_AudioSource;
    public AudioSource BGM_AudioSource;

    public Slider SFX_slider;
    public Slider BGM_slider;


    public GameObject optionPanel;

    [Header("Audio Clips")]
    public AudioClip Grab;
    public AudioClip CanDrop;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
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
        if (SFX_AudioSource != null)
        {
            SFX_AudioSource.PlayOneShot(CanDrop);
        }
    }
    public void AudioPlayGrab()
    {
        if (SFX_AudioSource != null)
        {
            SFX_AudioSource.PlayOneShot(Grab);
        }
    }

    public void SetSFXVolume ()
    {
        float value = SFX_slider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void SetBGMVolume()
    {
        float value = BGM_slider.value;
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        
    }

}
