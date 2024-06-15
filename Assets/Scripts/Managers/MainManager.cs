// Morgan Houston
using UnityEditor;
using UnityEngine;

// Manager for the Main Menu
public class MainManager : MonoBehaviour
{

    #region Methods

    public void PlayGame()
    {
        // Get last saved scene then load scene
        // loading castle scene for now
        SceneLoader.LoadLevel((int)SceneLoader.Levels.Castle);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion

}
