using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image emptyImg;
    [SerializeField] private Image fullImg;

    #endregion

    #region UnityMethods

    private void Reset()
    {
        var images = GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (fullImg == null && emptyImg != null)
            {
                fullImg = image;
            }

            if (emptyImg == null)
            {
                emptyImg = image;
            }
        }
    }

    #endregion

    #region Methods

    public void Damaged()
    {
        emptyImg.enabled = true;
        fullImg.enabled = false;
    }

    public void Healed()
    {
        fullImg.enabled = true;
        emptyImg.enabled = false;
    }

    #endregion
}