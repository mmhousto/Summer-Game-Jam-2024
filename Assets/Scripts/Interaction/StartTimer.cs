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

    #endregion

    #region Methods

    public override void Interact(Transform interactedTarget)
    {
        Debug.Log($"Start timer interaction{canInteract}");
        if (!canInteract) return;
        canInteract = false;
        Debug.Log($"Start timer interacted");
        timerSo.StartTimer();
    }
    
    #endregion
}
