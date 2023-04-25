using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransferer : MonoBehaviour
{
    public static float MusicVolume = 1f;

    public static float EffectVolume = 1f;

    public static void SetMusicVolume(float volumeLevel)
    { 
        MusicVolume = volumeLevel;
    }

    public static void SetEffectVolume(float volumeLevel) 
    { 
        EffectVolume = volumeLevel;
    }
}
