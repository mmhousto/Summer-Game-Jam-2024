// Morgan Houston
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

// Manager for the Main Menu
public class MainManager : MonoBehaviour
{

    #region Methods

    public void PlayGame()
    {
        // Get last saved scene then load scene
        // loading castle scene for now
        SceneLoader.LoadLevel(Player.Instance.lastLocation);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetMasterVolume(float newVolume)
    {
        SoundManagerScript.Instance.setFloat("MyExposedParam 4", newVolume);
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        SoundManagerScript.Instance.setFloat("MyExposedParam 2", newVolume);
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
    }

    public void SetSFXVolume(float newVolume)
    {
        SoundManagerScript.Instance.setFloat("MyExposedParam", newVolume);
        PlayerPrefs.SetFloat("SFXVolume", newVolume);
    }

    public void SetFootstepsVolume(float newVolume)
    {
        SoundManagerScript.Instance.setFloat("MyExposedParam 6", newVolume);
        PlayerPrefs.SetFloat("FSVolume", newVolume);
    }

    #endregion

}
