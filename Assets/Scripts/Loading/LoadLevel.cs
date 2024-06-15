// Morgan Houston
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    #region Fields
    [Tooltip("Player Sprite that follows loading bar.")]
    public GameObject player;
    [Tooltip("The end of the loading bar.")]
    public GameObject loadingBarEnd;
    [Tooltip("The loading bar.")]
    public Slider loadingBar;
    private float progress;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncScene(SceneLoader.levelToLoad));
    }

    #endregion

    #region Methods

    IEnumerator LoadAsyncScene(int index)
    {
        yield return null;

        AsyncOperation loading = SceneManager.LoadSceneAsync(index);

        while(!loading.isDone)
        {
            progress = Mathf.Clamp(loading.progress / .9f, 0f, 0.9999f);

            loadingBar.value = progress * 100f;
            player.transform.position = loadingBarEnd.transform.position;

            yield return null;
        }
    }

    #endregion

}
