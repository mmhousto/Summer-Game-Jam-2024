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
        canInteract = true;
    }

    #endregion

    #region Methods

    public override void Interact(Transform transform)
    {
        if (!canInteract) return;
        canInteract = false;
        timerSo.StartTimer();
    }
    
    #endregion
}
