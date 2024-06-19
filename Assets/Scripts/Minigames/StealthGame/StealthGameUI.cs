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

    public StealthGameManager sgm;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        Enemy.OnPlayerSpotted += ShowGameOverUI;
        sgm = GameObject.Find("StealthGameManager").GetComponent<StealthGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                sgm.playerInput.ActivateInput();
                SceneManager.LoadScene(2);
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
