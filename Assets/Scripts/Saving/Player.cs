using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Fields

    private static Player instance;

    public static Player Instance { get { return instance; } }

    public bool scavengerRespect;
    public bool magiciansRespect;
    public bool thievesRespect;

    public int lastLocation;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    private void OnEnable()
    {
        LoadPlayerData();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region Methods

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex > 1)
            SetLastLocation(scene.buildIndex);

        SaveSystem.SavePlayer(this);
    }

    /// <summary>
    /// Loads the players local data and sets it to the player or assings default values.
    /// </summary>
    public void LoadPlayerData()
    {
        PlayerSaveData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            SetData(data);
        }
        else
        {
            scavengerRespect = false;
            magiciansRespect = false;
            thievesRespect = false;
            lastLocation = (int)SceneLoader.Levels.Castle;
        }

    }

    // Sets player data from local save
    public void SetData(PlayerSaveData data)
    {
        scavengerRespect = data.scavengerRespect;
        magiciansRespect = data.magiciansRespect;
        thievesRespect = data.thievesRespect;
        lastLocation = data.lastLocation;
    }

    /// <summary>
    /// Returns true if you have united the kingdom
    /// </summary>
    /// <returns></returns>
    public bool GetUnitedKingdon()
    {
        return (scavengerRespect && magiciansRespect && thievesRespect);
    }

    public void GainScavengersRespect()
    {
        scavengerRespect = true;
    }

    public void GainMagiciansRespect()
    {
        magiciansRespect = true;
    }

    public void GainThievesRespect()
    {
        thievesRespect = true;
    }

    public void SetLastLocation(int location)
    {
        lastLocation = location;
    }

    #endregion

}
