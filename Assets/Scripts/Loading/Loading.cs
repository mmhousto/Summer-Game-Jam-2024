// Morgan Houston
using UnityEngine;

public class Loading : MonoBehaviour
{
    [Tooltip("The level to be loaded.")]
    //public int levelToLoad;
    public SceneLoader.Levels levelToLoad;

    /// <summary>
    /// Once player enters trigger area load loading scene and level to load.
    /// </summary>
    /// <param name="other">The object that triggered</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneLoader.LoadLevel((int)levelToLoad);
    }

}
