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

    void Start()
    {
        Instance = this;
    }
}
