using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixerGroup MusicMixer;
    public AudioMixerGroup SoundMixer;

    public float MusicVolume { get; set; }

    public float SoundVolume { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            var save = PlayGame.Load();
            if (save != null)
            {
                var musicVolume = float.Parse(save["MusicVolume"]);
                var soundVolume = float.Parse(save["SoundVolume"]);
                SetMusicVolume(musicVolume);
                SetSoundVolume(soundVolume);
                Debug.Log($"Loaded: {musicVolume} {soundVolume}");
            }
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
        MusicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        SoundMixer.audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
        SoundVolume = volume;
    }
}