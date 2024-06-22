using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StealthGameUI : MonoBehaviour
{
    #region Fields

    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject buttonToSelectOver, buttonToSelectWin;
    bool gameIsOver;

    public StealthGameManager sgm;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        Enemy.OnPlayerSpotted += ShowGameOverUI;
        sgm = GameObject.Find("StealthGameManager").GetComponent<StealthGameManager>();
    }

    private void OnEnable()
    {
        TimerManager.OnTimeOver += EndGame;
    }

    private void OnDisable()
    {
        TimerManager.OnTimeOver -= EndGame;
    }

    #endregion

    #region Methods

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void GoToCastle()
    {
        Time.timeScale = 1;
        sgm.playerInput.ActivateInput();
        SceneLoader.LoadLevel((int)SceneLoader.Levels.Castle);
    }

    void ShowGameWinUI()
    {
        EventSystem.current.SetSelectedGameObject(buttonToSelectWin);
        OnGameOver(gameWinUI);
    }

    void ShowGameOverUI()
    {
        EventSystem.current.SetSelectedGameObject(buttonToSelectOver);
        OnGameOver(gameOverUI);
    }

    void EndGame()
    {
        EventSystem.current.SetSelectedGameObject(buttonToSelectOver);
        sgm.playerInput.DeactivateInput();
        Time.timeScale = 0;
        OnGameOver(gameOverUI);
    }

    public void OnGameOver(GameObject gameUI)
    {
        if (gameUI.name.Contains("Win"))
        {
            EventSystem.current.SetSelectedGameObject(buttonToSelectWin);
        }
        Cursor.lockState = CursorLockMode.None;
        gameUI.SetActive(true);
        gameIsOver = true;
        Enemy.OnPlayerSpotted -= ShowGameOverUI;
    }

    #endregion
}
