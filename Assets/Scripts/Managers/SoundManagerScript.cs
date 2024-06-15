using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    private static SoundManagerScript S; // this is the singleton!

    public AudioClip stealthMinigameBGM; // drag clips into this field
    public AudioSource backgroundSource;

    void Awake()
    {
        // make sure there's only one singleton at all times
        if (S)
        {
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }

    private void Start()
    {
        StartBackgroundMusic(stealthMinigameBGM);
        Debug.Log("Testing w/ stealthminigame bgm");
    }

    public void StartBackgroundMusic(AudioClip bgmClip)
    {
        backgroundSource.clip = bgmClip;
        backgroundSource.Play();
    }

    public void StopBackgroundMusic(AudioClip bgmClip)
    {
        backgroundSource.Stop();
    }
}
