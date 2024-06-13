using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI timertext;
    [SerializeField] private Image fillclockImg;
    [SerializeField] private Color inactiveTimerColor;
    [SerializeField] private Color activeTimerColor;

    #endregion
    
    #region Constants

    private const int MINUTE_TO_SECONDS = 60;
    private const float FILLED_IMG = 1f;
    private const int END_TIME = 0;

    #endregion

    #region Methods

    public void TimerTextUpdate(int seconds)
    {
        var minutes = END_TIME;
        if (seconds >= MINUTE_TO_SECONDS)
        {
            minutes = Mathf.FloorToInt(((float)seconds / MINUTE_TO_SECONDS));
            seconds %= MINUTE_TO_SECONDS;
        }

        timertext.text = $"{minutes:00}:{seconds:00}";
    }

    public void TimerFillImgUpdate(float percentage)
    {
        fillclockImg.fillAmount = percentage;
    }

    public void EndTimerUI()
    {
        fillclockImg.color = inactiveTimerColor;
        fillclockImg.fillAmount = FILLED_IMG;
        TimerTextUpdate(END_TIME);
    }

    public void StartTimerUI()
    {
        fillclockImg.color = activeTimerColor;
        fillclockImg.fillAmount = FILLED_IMG;
    }

    #endregion
}