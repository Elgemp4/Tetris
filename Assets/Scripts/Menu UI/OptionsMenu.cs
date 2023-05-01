using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Showable
{
    public static OptionsMenu Instance { get; private set; }

    [SerializeField]
    private Slider MusicVolumeSlider;

    [SerializeField]
    private Slider EffectVolumeSlider;

    protected override void PostStart()
    {
        Instance = this;

        DataTransferer.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        DataTransferer.SetEffectVolume(PlayerPrefs.GetFloat("EffectVolume"));

        MusicVolumeSlider.value = DataTransferer.MusicVolume;
        EffectVolumeSlider.value = DataTransferer.EffectVolume;
    }

    public void ModifyMusicVolume(float volume)
    {
        DataTransferer.SetMusicVolume(volume);
    }

    public void ModifyEffectVolume(float volume)
    {
        DataTransferer.SetEffectVolume(volume);
    }
}
