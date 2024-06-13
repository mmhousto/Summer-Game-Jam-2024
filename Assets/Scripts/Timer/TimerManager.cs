using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    #region Fields

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
        TimeInvoker.OnStart += StartTimer;
        TimeInvoker.OnInterrupt += StopTimer;
    }


    private void OnDisable()
    {
        TimeInvoker.OnStart -= StartTimer;
        TimeInvoker.OnInterrupt -= StopTimer;
    }

    #endregion

    #region Methods

    private void StartTimer(TimerSO timer)
    {
        if (_activeTimer) return;
        _activeTimer = true;
        clock.StartTimerUI();
        _totaltime = timer.Minutes * MINUTE_TO_SECONDS + timer.seconds;
        _remainingtime = _totaltime;
        clock.TimerTextUpdate(_totaltime);
        clock.TimerFillImgUpdate(FILLED_IMG);
        Invoke(nameof(UpdateTimer), A_SECOND);
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

    public void StopTimer()
    {
        _remainingtime = TIME_ENDED;
        EndTimer();
    }

    #endregion
}