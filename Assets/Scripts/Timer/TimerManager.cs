using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private TimerSO timerSo;

    [SerializeField] private Clock clock;

    private int _totaltime;
    private int _remainingtime;
    private bool _activeTimer;

    #endregion

    #region Constants

    private const int MINUTE_TO_SECONDS = 60;
    private const float A_SECOND = 1f;
    private const float FILLED_IMG = 1f;
    private const int TIME_ENDED = 0;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        _activeTimer = false;
    }

    private void OnEnable()
    {
        TimeTester.OnClicked += StartTimer;
    }


    private void OnDisable()
    {
        TimeTester.OnClicked -= StartTimer;
    }

    #endregion

    #region Methods

    private void StartTimer()
    {
        if (_activeTimer) return;
        _activeTimer = true;
        clock.StartTimerUI();
        clock.TimerTextUpdate(timerSo.Seconds);
        clock.TimerFillImgUpdate(FILLED_IMG);
        _totaltime = timerSo.Minutes * MINUTE_TO_SECONDS + timerSo.seconds;
        _remainingtime = _totaltime;
        Invoke(nameof(UpdateTimer), A_SECOND);
        Debug.Log("Time started");
    }

    private void UpdateTimer()
    {
        _remainingtime--;
        if (_remainingtime > TIME_ENDED)
        {
            clock.TimerTextUpdate(_remainingtime);
            var percentageTime = (float)_remainingtime / _totaltime;
            clock.TimerFillImgUpdate(percentageTime);
            Invoke(nameof(UpdateTimer), A_SECOND);
        }
        else
        {
            EndTimer();
        }
    }

    private void EndTimer()
    {
        clock.EndTimerUI();
        _activeTimer = false;
    }

    #endregion
}