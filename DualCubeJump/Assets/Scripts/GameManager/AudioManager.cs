using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audios")]
    public AudioSource backgroundMusic;
    public AudioSource beep;
    public AudioSource menuMusic;

    [Header("MuteBoolValues")]
    public BoolValue musicMute;
    public BoolValue SFXMute;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        backgroundMusic = GetComponent<AudioSource>();
    }

    void Start()
    {
        musicMute.value = false;
        SFXMute.value = false;
    }

    public void PlayBackGroundMusic()
    {
        backgroundMusic.Play();
    }

    public void StopBackGroundMusic()
    {
        backgroundMusic.Stop();
    }
    
    public void PauseBackGroundMusic()
    {
        backgroundMusic.Pause();
    }

    public void PlayBeep()
    {
        beep.Play();
    }

    public void PlayMenuMusic()
    {
        menuMusic.Play();
    }
    
    public void StopMenuMusic()
    {
        menuMusic.Stop();
    }

    public void MuteUnmuteMusic()
    {
        backgroundMusic.mute = !backgroundMusic.mute;
        menuMusic.mute = !menuMusic.mute;
        musicMute.value = !musicMute.value;
    }
    
    public void MuteUnmuteSFX()
    {
        beep.mute = !beep.mute;
        SFXMute.value = !SFXMute.value;
    }

}
