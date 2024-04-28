using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    public Slider VolumeSlider;

    public TextMeshProUGUI VolumeStat;

    void Start()
    {
        VolumeSlider.value = AudioManager.Instance.MusicVolume;
        VolumeSlider.onValueChanged.AddListener(HandleSliderUpdate);
        VolumeStat.text = Math.Floor(VolumeSlider.value * 100).ToString();
    }

    private void HandleSliderUpdate(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        VolumeStat.text = Math.Floor(VolumeSlider.value*100).ToString();
    }
}
