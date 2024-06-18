using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript Instance; // this is the singleton!

    public AudioClip collectingMinigameBGM, stealthMinigameBGM, illusionMinigameBGM; // drag clips into this 
    public enum BGM { CollectingMinigame, StealthMinigame, IllusionMinigame };
    private AudioClip[] bgmArray;

    public AudioSource backgroundSource;

    public AudioMixer masterMixer;


    void Awake()
    {
        // make sure there's only one singleton at all times
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
            bgmArray = new AudioClip[] { collectingMinigameBGM, stealthMinigameBGM, illusionMinigameBGM };
        }
    }

    private void Start()
    {
        StartBackgroundMusic(BGM.StealthMinigame);
        Debug.Log("Testing w/ stealthminigame bgm");

    }

    public void StartBackgroundMusic(BGM bgm)
    {
        backgroundSource.clip = bgmArray[(int)bgm];
        backgroundSource.Play();
    }

    public void StopBackgroundMusic(AudioClip bgmClip)
    {
        backgroundSource.Stop();
    }

    public void setFloat(string parameter, float value)
    {
        masterMixer.SetFloat(parameter, value);
    }
}
