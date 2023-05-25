using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    public static OptionsMenu Instance { get; private set; }

    [SerializeField]
    private Slider MusicVolumeSlider;

    [SerializeField]
    private Slider EffectVolumeSlider;

    protected override void PostStart()
    {
        Instance = this;

        MusicVolumeSlider.value = Audio.MusicVolume;
        EffectVolumeSlider.value = Audio.EffectVolume;
    }

    /// <summary>
    /// Modifie le volume de la musique
    /// </summary>
    /// <param name="volume">Nouveau volume de la musique entre 0 et 1</param>
    public void ModifyMusicVolume(float volume)
    {
        Audio.MusicVolume = volume;
    }

    /// <summary>
    /// Modifie le volume des effets sonores
    /// </summary>
    /// <param name="volume">Nouveau volume des effets sonores entre 0 et 1</param>
    public void ModifyEffectVolume(float volume)
    {
        Audio.EffectVolume = volume;
    }
}
