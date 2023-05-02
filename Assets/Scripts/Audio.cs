using System.Linq;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [SerializeField]
    AudioClip[] Musics;

    private int PlayingMusicIndex = 0;


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

    void Start()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        MoveAudio.volume = DataTransferer.EffectVolume;

        RotateAudio.volume = DataTransferer.EffectVolume;

        HardDropAudio.volume = DataTransferer.EffectVolume;

        SingleLineClearedAudio.volume = DataTransferer.EffectVolume;

        DoubleLineClearedAudio.volume = DataTransferer.EffectVolume;

        TripleLineClearedAudio.volume = DataTransferer.EffectVolume;

        TetrisLineClearedAudio.volume = DataTransferer.EffectVolume;

        HoldAudio.volume = DataTransferer.EffectVolume;

        MusicPlayer.volume = DataTransferer.MusicVolume;
    }


    public void PlayMoveAudio()
    {
        MoveAudio.Play();
    }

    public void PlayRotateAudio()
    {
        RotateAudio.Play();
    }

    public void PlayHardDropAudio()
    {
        HardDropAudio.Play();
    }

    public void PlayHoldAudio()
    {
        HoldAudio.Play();
    }

    public void PlayLineClear(int numLines)
    {
        switch(numLines) 
        {
            case 1:
                SingleLineClearedAudio.Play();
                break;
            case 2:
                DoubleLineClearedAudio.Play();
                break;
            case 3:
                TripleLineClearedAudio.Play();
                break;
            case 4:
                TetrisLineClearedAudio.Play();
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
