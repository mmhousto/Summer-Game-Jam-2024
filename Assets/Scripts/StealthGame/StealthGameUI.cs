using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthGameUI : MonoBehaviour
{
    #region Fields

    public GameObject gameOverUI;
    public GameObject gameWinUI;
    bool gameIsOver;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        Enemy.OnPlayerSpotted += ShowGameOverUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SceneManager.LoadScene(0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }
    }

    #endregion

    #region Methods

    void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }

    void ShowGameOverUI()
    {
        OnGameOver(gameOverUI);
    }

    public void OnGameOver(GameObject gameUI)
    {
        gameUI.SetActive(true);
        gameIsOver = true;
        Enemy.OnPlayerSpotted -= ShowGameOverUI;
    }

    #endregion
}
