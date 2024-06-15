// Morgan Houston
using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    #region Fields

    public GameObject pauseMenu;
    public Button resumeButton, mainMenuButton;

    private StarterAssetsInputs inputs;
    private PlayerInput playerInput;
    [SerializeField]
    private bool isGamePaused;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        inputs.SetCursorState(false);
        playerInput.SwitchCurrentActionMap("UI");
        pauseMenu.SetActive(isGamePaused);
        if(resumeButton != null)
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

    private void Resume()
    {
        inputs.SetCursorState(true);
        playerInput.SwitchCurrentActionMap("Player");
        pauseMenu.SetActive(isGamePaused);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
    }

    public void MainMenu()
    {
        SceneLoader.LoadLevel((int)SceneLoader.Levels.MainMenu);
    }

    public bool IsGamePaused() { return isGamePaused; }

    #endregion

}
