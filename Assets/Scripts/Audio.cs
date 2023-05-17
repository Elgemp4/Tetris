using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [SerializeField]
    AudioClip[] Musics;

    private Dictionary<ESoundEffects, AudioSource> _SoundEffects;

    private int PlayingMusicIndex = 0;

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

    void FixedUpdate()
    {
        foreach (AudioSource effect in _SoundEffects.Values)
        {
            effect.volume = DataTransferer.EffectVolume;
        }

        MusicPlayer.volume = DataTransferer.MusicVolume;
    }

    public void PlaySoundEffect(ESoundEffects eSound)
    {
        AudioSource effect = _SoundEffects[eSound];

        effect.Play();
    }   

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
