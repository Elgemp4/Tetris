using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

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

    void Start()
    {
        Instance = this;
    }
}
