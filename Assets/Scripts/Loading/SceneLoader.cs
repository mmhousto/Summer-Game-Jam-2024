// Morgan Houston
using System.ComponentModel;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    #region Fields

    public static int levelToLoad = 1;

    #endregion

    #region Methods

    public static Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static void LoadLevel(int level)
    {
        levelToLoad = level;
        SceneManager.LoadSceneAsync(0);
    }

    #endregion

}
