using System.Collections;
using UnityEngine;

public class StartTimer : Interactable
{
    #region field

    [SerializeField]private TimerScriptableObject timerSo;

    #endregion
    
    #region Constants

    private const float RESTART_TIME = 3;

    #endregion
    
    #region Unity Methods

    private void Start()
    {
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
        StartCoroutine(WaitAndRestart());
    }
    
    private IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(RESTART_TIME);
        canInteract = true;
    }
    
    #endregion
}
