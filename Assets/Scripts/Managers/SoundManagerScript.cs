using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript Instance; // this is the singleton!

    public AudioClip titleTheme, collectingMinigameBGM, stealthMinigameBGM, illusionMinigameBGM; // drag clips into this 
    public AudioClip scavengersTheme, thievesTheme, magiciansTheme;

    public AudioClip illusionDeath, illusionWin;

    public AudioClip collectingDeath, collectingWin;

    public AudioClip wood, fork, hammer, potion, keychain, compass, book, dollhouse, goldbar;

    public AudioClip startGameSFX, backSFX, selectSFX, hoverSFX;

    //public enum BGM { CollectingMinigame, StealthMinigame, IllusionMinigame };
    //private AudioClip[] bgmArray;

    private Dictionary<GameManagerScript.GameState, AudioClip> bgmDict;

    public AudioSource backgroundSource;

    public AudioSource sfxSource;

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
            //bgmArray = new AudioClip[] { collectingMinigameBGM, stealthMinigameBGM, illusionMinigameBGM 
        }

        bgmDict = new Dictionary<GameManagerScript.GameState, AudioClip>() {
                { GameManagerScript.GameState.MainMenu, titleTheme},
                { GameManagerScript.GameState.ScavengersFaction, scavengersTheme},
                { GameManagerScript.GameState.ThievesFaction, thievesTheme},
                {GameManagerScript.GameState.MagiciansFaction, magiciansTheme },
                { GameManagerScript.GameState.CollectionGame, collectingMinigameBGM},
                { GameManagerScript.GameState.StealthGame, stealthMinigameBGM},
                { GameManagerScript.GameState.IllusionGame, illusionMinigameBGM} };
    }

    public void StartBackgroundMusic(GameManagerScript.GameState gamestate)
    {
        if (bgmDict.ContainsKey(gamestate))
        {
            AudioClip bgmClip = bgmDict[gamestate];
            backgroundSource.clip = bgmClip; // bgmArray[(int)bgm];
            backgroundSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundSource.Stop();
    }

    public void PlaySFXSound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void setFloat(string parameter, float value)
    {
        masterMixer.SetFloat(parameter, value);
    }
}
