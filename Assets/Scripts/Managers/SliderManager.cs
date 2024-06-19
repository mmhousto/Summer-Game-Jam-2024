using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets sliders value
/// </summary>
public class SliderManager : MonoBehaviour
{
    #region Fields

    [Tooltip("Name of Player Pref for slider.")]
    public string SliderPrefName;
    private Slider slider;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        if(slider == null) slider = GetComponent<Slider>();
        if(slider != null) slider.value = PlayerPrefs.GetFloat(SliderPrefName, 1f);
    }

    #endregion
}
