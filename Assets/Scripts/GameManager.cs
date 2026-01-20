using UnityEngine;

public class GameManager : MonoBehaviour
{

    public AudioSource SFX_AudioSource;
    public AudioSource BGM_AudioSource;

    [Header("Audio Clips")]
    public AudioClip Grab;
    public AudioClip CanDrop;
    
    void Start()
    {
        
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

}
