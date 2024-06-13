using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInvoker : MonoBehaviour
{
    #region Events
    [SerializeField] private TimerSO timerSo;
    public delegate void StartAction(TimerSO timer);
    public delegate void EndAction();

    public static event StartAction OnStart;
    public static event EndAction OnInterrupt;
    #endregion

    #region Methods

    public void StartTimer()
    {
        OnStart?.Invoke(timerSo);
    }
    
    public void EndTimer()
    {
        OnInterrupt?.Invoke();
    }

    #endregion
}