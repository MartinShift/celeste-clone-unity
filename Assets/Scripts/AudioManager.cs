using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixerGroup MusicMixer;
    public AudioMixerGroup SoundMixer;

    public float MusicVolume => Mathf.Pow(10,MusicMixer.audioMixer.GetFloat("MusicVolume", out float volume) ? volume : 0);

    public float SoundVolume => Mathf.Pow(10, SoundMixer.audioMixer.GetFloat("MusicVolume", out float volume) ? volume : 0);

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicVolume(float volume)
    {
        MusicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundVolume(float volume)
    {
        SoundMixer.audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
    }
}