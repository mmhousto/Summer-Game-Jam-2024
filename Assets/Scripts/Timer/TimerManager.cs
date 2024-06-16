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
    private bool _interrupted;

    #endregion

    #region Constants

    private const int MINUTE_TO_SECONDS = 60;
    private const float A_SECOND = 1f;
    private const float FILLED_IMG = 1f;
    private const int TIME_ENDED = 0;

    #endregion

    #region Events

    public delegate void FinishedAction();
    public delegate void StartingAction();

    public static event FinishedAction OnTimeOver;
    public static event FinishedAction OnTimeStopped;
    public static event StartingAction OnTimeStart;

    #endregion

    #region UnityMethods

    private void Awake()
    {
        _activeTimer = false;
    }

    private void OnEnable()
    {
        TimerScriptableObject.OnStart += StartTimer;
        TimerScriptableObject.OnInterrupt += StopTimer;
        CollectGameManager.OnFinished += StopTimer;
    }


    private void OnDisable()
    {
        TimerScriptableObject.OnStart -= StartTimer;
        TimerScriptableObject.OnInterrupt -= StopTimer;
        CollectGameManager.OnFinished -= StopTimer;
    }

    #endregion

    #region Methods

    private void StartTimer(TimerScriptableObject timer)
    {
        if (_activeTimer) return;
        _activeTimer = true;
        _interrupted = false;
        clock.StartTimerUI();
        _totaltime = timer.Minutes * MINUTE_TO_SECONDS + timer.seconds;
        _remainingtime = _totaltime;
        clock.TimerTextUpdate(_totaltime);
        clock.TimerFillImgUpdate(FILLED_IMG);
        Invoke(nameof(UpdateTimer), A_SECOND);
        OnTimeStart?.Invoke();
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
        if(_interrupted) return;
        OnTimeOver?.Invoke();
    }

    public void StopTimer()
    {
        _interrupted = true;
        _remainingtime = TIME_ENDED;
        OnTimeStopped?.Invoke();
        EndTimer();
    }

    #endregion
}