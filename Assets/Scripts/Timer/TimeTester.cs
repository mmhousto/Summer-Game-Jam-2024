using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTester : MonoBehaviour
{
    #region Events
    [SerializeField] private TimerSO timerSo;
    public delegate void ClickAction(TimerSO timer);

    public static event ClickAction OnClicked;

    #endregion

    #region Methods

    public void StartTimer()
    {
        OnClicked?.Invoke(timerSo);
    }

    #endregion
}