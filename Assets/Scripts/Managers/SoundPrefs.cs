using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets audio mixer volumes
/// </summary>
public class SoundPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManagerScript.Instance.setFloat("MyExposedParam 4", PlayerPrefs.GetFloat("MasterVolume", 1f));
        SoundManagerScript.Instance.setFloat("MyExposedParam 2", PlayerPrefs.GetFloat("MusicVolume", -20f));
        SoundManagerScript.Instance.setFloat("MyExposedParam", PlayerPrefs.GetFloat("SFXVolume", 1f));
        SoundManagerScript.Instance.setFloat("MyExposedParam 6", PlayerPrefs.GetFloat("FSVolume", 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
