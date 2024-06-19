using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public enum GameState { MainMenu, MapScreen, ThievesFaction, StealthGame, ScavengersFaction, CollectionGame, MagiciansFaction, IllusionGame, LoadingScreen, Pause, FailScreen };

    public static GameManagerScript Instance;
    GameState currentState;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        currentState = GameState.MapScreen;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setGameState(GameState gameState)
    {
        if (currentState == gameState) { return; }
        currentState = gameState;
        SoundManagerScript.Instance.StartBackgroundMusic(currentState);
    }
}
