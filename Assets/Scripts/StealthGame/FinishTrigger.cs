using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    #region Fields

    public StealthGameUI finishUI; 
    public StealthGameManager sgm;

    #endregion

    #region UnityMethods

    void OnTriggerEnter(Collider other)
    {
        if (sgm.playerHasItem)
        {
            sgm.playerInput.DeactivateInput();
            Time.timeScale = 0;
            finishUI.OnGameOver(finishUI.gameWinUI);
        }
    }

    #endregion
}
