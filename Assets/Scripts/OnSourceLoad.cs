using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSourceLoad : MonoBehaviour
{
    public AudioSource source;

    void Start()
    {
        source.volume = AudioManager.Instance.MusicVolume;
    }
}
