using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public enum GameState { MainMenu, MapScreen, StealthGame, CollectionGame, IllusionGame, LoadingScreen, Pause, FailScreen };

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
    }


    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.MainMenu;
    }

    public void setGameState(GameState gameState)
    {
        currentState = gameState;
        SoundManagerScript.Instance.StartBackgroundMusic(currentState);
    }
}
