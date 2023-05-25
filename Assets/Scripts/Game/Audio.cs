using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La classe <c>Audio</c> gère les effets sonores et la musique du jeu.
/// </summary>
public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [SerializeField]
    AudioClip[] Musics;

    private Dictionary<ESoundEffects, AudioSource> _SoundEffects;

    private int PlayingMusicIndex = 0;

    public static float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
        }

    }

    public static float EffectVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("EffectVolume", 1f);
        }
        set
        {
            PlayerPrefs.SetFloat("EffectVolume", value);
        }
    }

    #region Audios
    [SerializeField]
    AudioSource MoveAudio;

    [SerializeField]
    AudioSource RotateAudio;

    [SerializeField]
    AudioSource HardDropAudio;

    [SerializeField]
    AudioSource SingleLineClearedAudio;

    [SerializeField]
    AudioSource DoubleLineClearedAudio;

    [SerializeField]
    AudioSource TripleLineClearedAudio;

    [SerializeField]
    AudioSource TetrisLineClearedAudio;

    [SerializeField]
    AudioSource HoldAudio;

    [SerializeField]
    AudioSource MusicPlayer;
    #endregion

    void Start()
    {
        Instance = this;

        _SoundEffects = new Dictionary<ESoundEffects, AudioSource>()
        {
            {ESoundEffects.Rotate, RotateAudio},
            {ESoundEffects.Move, MoveAudio},
            {ESoundEffects.HardDrop, HardDropAudio},
            {ESoundEffects.Hold, HoldAudio},
            {ESoundEffects.SingleLineCleared, SingleLineClearedAudio},
            {ESoundEffects.DoubleLineCleared, DoubleLineClearedAudio},
            {ESoundEffects.TripleLineCleared, TripleLineClearedAudio},
            {ESoundEffects.TetrisLineCleared, TetrisLineClearedAudio},
        };
    }

    /// <summary>
    /// Vérfie si le volume n'a pas changé dans les paramètres
    /// </summary>
    void FixedUpdate()
    {
        foreach (AudioSource effect in _SoundEffects.Values)
        {
            effect.volume = EffectVolume;
        }

        MusicPlayer.volume = MusicVolume;
    }

    /// <summary>
    /// Joue l'effet sonore indiqué
    /// </summary>
    /// <param name="eSound">Effet sonore devant être joué</param>
    public void PlaySoundEffect(ESoundEffects eSound)
    {
        AudioSource effect = _SoundEffects[eSound];

        effect.Play();
    }   

    /// <summary>
    /// Joue l'effet sonore correspondant au nombre de lignes supprimées
    /// </summary>
    /// <param name="numLines">Les nombre de lignes supprimées</param>
    public void PlayLineClear(int numLines)
    {
        switch(numLines) 
        {
            case 1:
                PlaySoundEffect(ESoundEffects.SingleLineCleared);
                break;
            case 2:
                PlaySoundEffect(ESoundEffects.DoubleLineCleared);
                break;
            case 3:
                PlaySoundEffect(ESoundEffects.TripleLineCleared);
                break;
            case 4:
                PlaySoundEffect(ESoundEffects.TetrisLineCleared);
                break;
        }
    }

    /// <summary>
    /// Vérifie si la musique est coupée, si oui il en relance une autre
    /// </summary>
    private void Update()
    {
        if (PlayingMusicIndex == 0)
        {
            ShuffleMusics();
        }

        if (!MusicPlayer.isPlaying)
        {
            PlayingMusicIndex = (PlayingMusicIndex + 1) % Musics.Length;
            MusicPlayer.clip = Musics[PlayingMusicIndex];
            MusicPlayer.Play();
        }
    }

    /// <summary>
    /// Mélange la playlist de musiques
    /// </summary>
    private void ShuffleMusics()
    {
        for (int i = 0; i < Musics.Length; i++)
        {
            int randomIndex = Random.Range(0, Musics.Length - 1);

            AudioClip temp = Musics[i];
            Musics[i] = Musics[randomIndex];
            Musics[randomIndex] = temp;
        }
    }
}
