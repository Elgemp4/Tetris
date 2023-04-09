using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [SerializeField]
    AudioSource moveAudio;

    [SerializeField]
    AudioSource rotateAudio;

    [SerializeField]
    AudioSource hardDropAudio;

    [SerializeField]
    AudioSource singleLineClearedAudio;

    [SerializeField]
    AudioSource doubleLineClearedAudio;

    [SerializeField]
    AudioSource tripleLineClearedAudio;

    [SerializeField]
    AudioSource tetrisLineClearedAudio;


    public void PlayMoveAudio()
    {
        moveAudio.Play();
    }

    public void PlayRotateAudio()
    {
        rotateAudio.Play();
    }

    public void PlayHardDropAudio()
    {
        hardDropAudio.Play();
    }

    public void PlayLineClear(int numLines)
    {
        switch(numLines) 
        {
            case 1:
                singleLineClearedAudio.Play();
                break;
            case 2:
                doubleLineClearedAudio.Play();
                break;
            case 3:
                tripleLineClearedAudio.Play();
                break;
            case 4:
                tetrisLineClearedAudio.Play();
                break;
        }
    }

    void Start()
    {
        Instance = this;
    }
}
