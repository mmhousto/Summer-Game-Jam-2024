using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTester : MonoBehaviour
{
    #region Events

    public delegate void ClickAction();

    public static event ClickAction OnClicked;

    #endregion

    #region Methods

    public void StartTimer()
    {
        OnClicked?.Invoke();
    }

    #endregion
}