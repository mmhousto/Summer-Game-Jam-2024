// Morgan Houston
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    #region Fields

    public static int levelToLoad = 1;
    public enum Levels { Loading, MainMenu, Castle, Scavenger, ScavengerMinigame, Magician, MagicianMinigame, Thieves, ThievesMinigame };

    #endregion

    #region Methods

    public static Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static void LoadLevel(int level)
    {
        levelToLoad = level;
        SceneManager.LoadSceneAsync((int)Levels.Loading);
    }

    #endregion

}
