// Morgan Houston
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    #region Fields
    /*[Tooltip("Player Sprite that follows loading bar.")]
    public GameObject player;
    [Tooltip("The end of the loading bar.")]
    public GameObject loadingBarEnd;
    [Tooltip("The loading bar.")]
    public Slider loadingBar;
    [Tooltip("Loading Bar image.")]
    public Image loadingImage;*/

    private float progress;
    private const float ALMOST_COMPLETE = 0.9999f;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncScene(SceneLoader.levelToLoad));
    }

    #endregion

    #region Methods

    IEnumerator LoadAsyncScene(int index)
    {
        yield return new WaitForSeconds(2f); // allows animation to play at least once

        AsyncOperation loading = SceneManager.LoadSceneAsync(index);

        while (!loading.isDone)
        {
            progress = Mathf.Clamp(loading.progress / .9f, 0f, ALMOST_COMPLETE);

            //loadingBar.value = progress * 100f;
            //loadingImage.fillAmount = progress * 100f;
            //player.transform.position = loadingBarEnd.transform.position; // animating them now
            yield return null;
        }
    }

    #endregion

}
