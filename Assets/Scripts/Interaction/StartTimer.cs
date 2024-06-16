// Morgan Houston
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : Interactable
{
    #region field

    [SerializeField]private TimerScriptableObject timerSo;

    #endregion
    
    #region Unity Methods

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log($"Start timer script");
        canInteract = true;
    }
    private void OnEnable()
    {
        TimerManager.OnTimeOver += RestartInteraction;
        TimerManager.OnTimeStopped += RestartInteraction;
    }


    private void OnDisable()
    {
        TimerManager.OnTimeOver -= RestartInteraction;
        TimerManager.OnTimeStopped -= RestartInteraction;
    }
    #endregion

    #region Methods

    public override void Interact(Transform interactedTarget)
    {
        if (!canInteract) return;
        canInteract = false;
        timerSo.StartTimer();
    }

    private void RestartInteraction()
    {
        canInteract = true;
    }
    
    #endregion
}
