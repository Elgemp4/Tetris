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

    /// <summary>
    /// Modifie le volume de la musique
    /// </summary>
    /// <param name="volume">Nouveau volume de la musique entre 0 et 1</param>
    public void ModifyMusicVolume(float volume)
    {
        DataTransferer.SetMusicVolume(volume);
    }

    /// <summary>
    /// Modifie le volume des effets sonores
    /// </summary>
    /// <param name="volume">Nouveau volume des effets sonores entre 0 et 1</param>
    public void ModifyEffectVolume(float volume)
    {
        DataTransferer.SetEffectVolume(volume);
    }
}
