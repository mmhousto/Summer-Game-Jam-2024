// Morgan Houston
using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    #region Fields

    public GameObject pauseMenu;
    public Button resumeButton, mainMenuButton;

    private StarterAssetsInputs inputs;
    private PlayerInput playerInput;
    private bool isGamePaused;
    private bool inMinigame;
    private Scene currentScene;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentScene = SceneLoader.GetActiveScene();
        inMinigame = (currentScene.name.Contains("Game") || currentScene.name.Contains("game"));
    }

    private void OnEnable()
    {
        resumeButton?.onClick.AddListener(ResumeGame);
        mainMenuButton?.onClick.AddListener(MainMenu);
    }

    private void OnDisable()
    {
        resumeButton?.onClick.RemoveListener(ResumeGame);
        mainMenuButton?.onClick.RemoveListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPause();
    }

    #endregion

    #region Methods

    private void CheckIfPause()
    {
        if (inputs != null && inputs.pause)
        {
            isGamePaused = !isGamePaused;
            inputs.pause = false;
        }

        if (pauseMenu != null)
            SetPauseMenu();

    }

    private void SetPauseMenu()
    {
        // PAUSED
        if (isGamePaused && !pauseMenu.activeInHierarchy)
        {
            Pause();
        }
        // NOT PAUSED
        else if (!isGamePaused && pauseMenu.activeInHierarchy)
        {
            Resume();
        }
    }

    private void Pause()
    {
        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.selectSFX);

        inputs.SetCursorState(false);
        playerInput.SwitchCurrentActionMap("UI");
        pauseMenu.SetActive(isGamePaused);
        if (resumeButton != null)
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        if (inMinigame) Time.timeScale = 0f;
    }

    private void Resume()
    {
        inputs.SetCursorState(true);
        playerInput.SwitchCurrentActionMap("Player");
        pauseMenu.SetActive(isGamePaused);
        if (inMinigame) Time.timeScale = 1f;
    }

    /// <summary>
    /// Restarts scene
    /// </summary>
    public void Restart()
    {
        SceneLoader.LoadLevel(currentScene.buildIndex);
    }

    public void ResumeGame()
    {
        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.selectSFX);
        isGamePaused = false;
    }

    public void MainMenu()
    {
        SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.selectSFX);
        if (inMinigame) Time.timeScale = 1f;
        SceneLoader.LoadLevel((int)SceneLoader.Levels.MainMenu);
    }

    public bool IsGamePaused() { return isGamePaused; }

    #endregion

}
